using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : PooledObject
{
    [SerializeField] ShootPlayerController player;
    [SerializeField] int hp;
    [SerializeField] int maxHp;
    [SerializeField] Slider hpBar;
    [SerializeField] GameObject clearUI;
    [SerializeField] GameObject destructionVFX;
    [SerializeField] Transform targetPoint;
    [SerializeField] GameObject bossAppearsUI;
    [SerializeField] AudioSource bGM;
    [SerializeField] SpriteRenderer renderSprite;

    [SerializeField] float moveSpeed = 5f; // 보스 이동 속도
    [SerializeField] float horizontalMoveDistance; // 좌우 이동 거리
    [SerializeField] float verticalMoveDistance; // 위아래 이동 거리
    [SerializeField] float moveInterval; // 이동 간격

    [SerializeField] PooledObject missilePrefab; // 미사일 프리팹
    [SerializeField] Transform missileSpawnPoint; // 미사일 발사 위치
    [SerializeField] float missileFireInterval; // 미사일 발사 간격
    [SerializeField] float missileSpeed; // 미사일 속도
    [SerializeField] Vector2 randomScaleRange = new Vector2(0.5f, 2.0f); // 미사일 스케일 범위

    private Vector3 startPos;
    private Vector3 leftBound;
    private Vector3 rightBound;
    private Vector3 topBound;
    private Vector3 bottomBound;

    private int fireCount = 3; // 처음 발사 패턴 수
    private bool invulnerable = true; // 무적 상태 초기화

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<ShootPlayerController>();

        bossAppearsUI.SetActive(true);

        hpBar.maxValue = maxHp;
        hpBar.value = hp;

        StartCoroutine(MoveToTarget());
        StartCoroutine(FireMissiles());
    }

    private void Update()
    {
        hpBar.value = hp;
    }

    private void GetDamage(int damage)
    {
        if (invulnerable) return; // 무적 상태에서는 데미지를 받지 않음

        hp -= damage;
        if (hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        player.Invincibility();
        renderSprite.enabled = false;
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Debug.Log("보스 죽음");
        StartCoroutine(Clear());
    }

    private IEnumerator Clear()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        clearUI.SetActive(true);    
        Time.timeScale = 0;
    }

    private IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(4.0f);
        bossAppearsUI.SetActive(false);

        bGM.volume = 1f;

        while (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        invulnerable = false;

        startPos = transform.position;
        leftBound = startPos - new Vector3(horizontalMoveDistance, 0, 0);
        rightBound = startPos + new Vector3(horizontalMoveDistance, 0, 0);
        topBound = startPos + new Vector3(0, verticalMoveDistance, 0);
        bottomBound = startPos - new Vector3(0, verticalMoveDistance, 0);

        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            Vector3 randomTarget = new Vector3(Random.Range(leftBound.x, rightBound.x), Random.Range(bottomBound.y, topBound.y), transform.position.z);

            while (Vector3.Distance(transform.position, randomTarget) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, randomTarget, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }

    private IEnumerator FireMissiles()
    {
        yield return new WaitForSeconds(5.2f);

        while (true)
        {
            yield return new WaitForSeconds(missileFireInterval);

            StartCoroutine(FireExplosionPattern(fireCount));
            fireCount++;
            if (fireCount > 10)
            {
                fireCount = 10;
            }

            yield return new WaitForSeconds(1.5f); // 패턴 사이의 간격을 1.5초로 설정
        }
    }

    private IEnumerator FireExplosionPattern(int count)
    {
        for (int i = 0; i < count; i++)
        {
            FireExplosion();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void FireExplosion()
    {
        int missileCount = 12;
        float explosionRadius = 1f;

        for (int i = 0; i < missileCount; i++)
        {
            float angle = 360f / missileCount * i;
            Quaternion rotation = missileSpawnPoint.rotation * Quaternion.Euler(0, 0, angle);
            Vector3 spawnPosition = missileSpawnPoint.position + rotation * Vector3.right * explosionRadius;
            StartCoroutine(FireMissile(spawnPosition, rotation));
        }
    }

    private IEnumerator FireMissile(Vector3 position, Quaternion rotation)
    {
        PooledObject newMissile = Manager.Pool.GetPool(missilePrefab, position, rotation);

        float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
        newMissile.transform.localScale = Vector3.one * randomScale;

        SpriteRenderer spriteRenderer = newMissile.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }

        Rigidbody2D rb = newMissile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = rotation * Vector2.down * missileSpeed;
        }

        Collider2D missileCollider = newMissile.GetComponent<Collider2D>();
        if (missileCollider != null)
        {
            Physics2D.IgnoreCollision(missileCollider, GetComponent<Collider2D>());
        }

        yield return new WaitForSeconds(5f);
        if (newMissile != null)
        {
            newMissile.Release();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShootPlayerController.instance.GetDamage(1);
        }
        else if (collision.tag == "Projectile")
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
            {
                GetDamage(projectile.damage);
                projectile.Release(); // 발사체 파괴
                Debug.Log("Projectile Released"); // 디버깅 메시지 추가
            }
        }
    }
}
