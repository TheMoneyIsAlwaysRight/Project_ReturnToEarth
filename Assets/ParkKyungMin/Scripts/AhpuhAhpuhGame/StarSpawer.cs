using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawer : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] Transform spawnPoint;   // 스폰위치
    [SerializeField] float repeatTime;       // 생성 주기   
    [SerializeField] float randomRange;      // 생성 범위

    [SerializeField] float minScale;  // 최소 크기
    [SerializeField] float maxScale;  // 최대 크기

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
            GameObject newStar = Instantiate(starPrefab, spawnPoint.position + random, spawnPoint.rotation);

            // 랜덤 색상 생성
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // 스폰된 프리팹의 색상 설정
            Renderer starRenderer = newStar.GetComponent<Renderer>();
            if (starRenderer != null)
            {
                starRenderer.material.color = randomColor;
            }

            // 랜덤 크기 생성
            float randomScale = Random.Range(minScale, maxScale);
            newStar.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
    
}
