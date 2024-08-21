using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform spawnPoint;   // 스폰위치
    [SerializeField] float repeatTime;       // 생성 주기   
    [SerializeField] float randomRange;      // 생성 범위

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
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoint.position + random, spawnPoint.rotation);
        }
    }
}
