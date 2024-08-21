using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] Transform spawnPoint;   // 스폰 위치
    [SerializeField] float repeatTime;       // 생성 주기   
    [SerializeField] float randomRange;      // 생성 범위
    [SerializeField] float fallSpeed;   // 내려가는 속도

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
            Vector3 random = Vector3.right * Random.Range(-randomRange, randomRange);
            // 프리팹 스폰
            GameObject newPlatform = Instantiate(platformPrefab, spawnPoint.position + random, spawnPoint.rotation);
            // PlatformMover 컴포넌트를 추가하고 설정
            PlatformMover mover = newPlatform.AddComponent<PlatformMover>();
            mover.fallSpeed = fallSpeed;
        }
    }
}
