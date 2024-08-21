using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] GameObject planetPrefab;
    [SerializeField] Transform spawnPoint;      // 행성 스폰위치
    [SerializeField] float repeatTime;          // 생성 주기   
    [SerializeField] float randomRange;         // 생성 범위
    [SerializeField] float minSpeed;            // 최소 속도
    [SerializeField] float maxSpeed;            // 최대 속도

    // 스케일 범위(최대값, 최소값)
    [SerializeField] Vector2 randomScaleRange = new Vector2(0.5f, 2.0f);

    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(SpawnRoutine());                                                                                          
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatTime);
            // 랜덤 위치 생성
            Vector3 random = Vector3.up * Random.Range(-randomRange, randomRange);

            // 프리팹 스폰
            GameObject newPlanet = Instantiate(planetPrefab, spawnPoint.position + random, spawnPoint.rotation);

            // 랜덤 스케일을 생성, 설정
            float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
            newPlanet.transform.localScale = Vector3.one * randomScale;

            // 랜덤 속도를 생성, 설정
            Rigidbody2D rb = newPlanet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randomSpeed = Random.Range(minSpeed, maxSpeed);
                rb.velocity = Vector2.left * randomSpeed;  
            }
        }
    }
}
