using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour
{
    [SerializeField] EnemyWaves[] enemyWaves;   // 적 웨이브 배열

    [SerializeField] GameObject powerUp;
    [SerializeField] float timeForNewPowerup;    // 생성 시간 간격
    [SerializeField] GameObject[] planets;       // 행성 오브젝트 배열
    [SerializeField] float timeBetweenPlanets;   // 행성 생성 시간 간격
    [SerializeField] float planetsSpeed;         // 행성 이동 속도
    [SerializeField] PooledObject enemyBullet;   // 적 총알
    [SerializeField] PooledObject enemyEffect;   // 적 죽을때 VFX
    [SerializeField] PooledObject bulletEffect;  // 총알 VFX
    [SerializeField] PooledObject missilePrefab; // 보스 미사일


    [SerializeField] GameObject bossPrefab;     // 보스 프리팹
    [SerializeField] float bossSpawnDelay;      // 보스 생성 지연 시간
    [SerializeField] AudioSource appearanceSound;
    [SerializeField] AudioSource bGM;

List<GameObject> planetsList = new List<GameObject>();
    Camera mainCamera;   

    private void Start()
    {
        mainCamera = Camera.main;

        Manager.Pool.CreatePool(enemyBullet, 40, 100);
        Manager.Pool.CreatePool(enemyEffect, 10, 50);
        Manager.Pool.CreatePool(bulletEffect, 10, 50);
        Manager.Pool.CreatePool(missilePrefab, 40, 400);

        // 웨이브 순차적 생성 
        for (int i = 0; i<enemyWaves.Length; i++) 
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }

        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
        StartCoroutine(SpawnBossEnemy());
    }


    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (ShootPlayerController.instance != null)
            Instantiate(Wave);
    }

   
    IEnumerator PowerupBonusCreation() 
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForNewPowerup);

            // 오브젝트 랜덤 생성
            Instantiate( powerUp, new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                         mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), Quaternion.identity);
        }
    }

    IEnumerator PlanetsCreation()
    {
       
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);

        while (true)
        {
            // 랜덤으로 행성 선택 및 생성
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);

            // 행성 리스트가 비었을 경우 다시 초기화
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;    // 행성 이동 속도 설정

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }

    IEnumerator SpawnBossEnemy()
    {
        // 보스 생성 지연 시간 대기
        yield return new WaitForSeconds(bossSpawnDelay);

        bossPrefab.SetActive(true);
        appearanceSound.Play();
        bGM.volume = 0.4f;
    }
}
