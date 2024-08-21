using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustSpawner : MonoBehaviour
{
    [SerializeField] GameObject dustPrefab;             // 먼지 프리팹
    [SerializeField] Transform spawnPoint;              // 스폰 위치
    [SerializeField] Transform[] arrivalPoints;         // 먼지 도착 지점
    [SerializeField] float repeatTime;                  // 생성 주기
    [SerializeField] float scaleUpDuration = 5f;
    [SerializeField] float moveSpeed = 2f;              // 장애물이 다가오는 속도

    private Coroutine spawnCoroutine;
    private Coroutine stopSpawnCoroutine;

    private void OnEnable()
    {

        spawnCoroutine = StartCoroutine(SpawnRoutine());
        stopSpawnCoroutine = StartCoroutine(StopSpawningAfterDuration());
    }

    private void OnDisable()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        if (stopSpawnCoroutine != null)
        {
            StopCoroutine(stopSpawnCoroutine);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatTime);

            // 랜덤한 인덱스 선택
            int randomIndex = Random.Range(0, arrivalPoints.Length);

            // 도착 지점 설정
            Transform arrivalPoint = arrivalPoints[randomIndex];

            // 프리팹 스폰
            GameObject newObstacle = Instantiate(dustPrefab, spawnPoint.position, spawnPoint.rotation);

            // 초기 스케일 설정
            newObstacle.transform.localScale = Vector3.one * 0.1f;

            StartCoroutine(ScaleUpRoutine(newObstacle.transform));

            StartCoroutine(MoveTowardsPoint(newObstacle.transform, arrivalPoint));
        }
    }

    IEnumerator ScaleUpRoutine(Transform objectTransform)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = objectTransform.localScale;
        Vector3 targetScale = Vector3.one;

        while (elapsedTime < scaleUpDuration)
        {
            if (objectTransform == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / scaleUpDuration;
            objectTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        if (objectTransform != null)
        {
            objectTransform.localScale = targetScale;
        }
    }

    IEnumerator MoveTowardsPoint(Transform objectTransform, Transform targetPoint)
    {
        while (objectTransform != null)
        {
            // 장애물이 목표 지점으로 움직이는 방향 계산
            Vector3 direction = (targetPoint.position - objectTransform.position).normalized;

            // 일정 속도로 목표 지점을 향해 이동
            objectTransform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator StopSpawningAfterDuration()
    {
        yield return new WaitForSeconds(60f);

        // 스폰 코루틴 중지
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null; 
        }
    }
}
