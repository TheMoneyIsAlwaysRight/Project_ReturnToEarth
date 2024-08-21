using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    [SerializeField] private Animator wormAnimator;
    [SerializeField] private GameObject wormPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float genTime;

    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(genTime);

            // 프리팹 생성
            Instantiate(wormPrefab, transform.position, transform.rotation);
        }
    }
}
