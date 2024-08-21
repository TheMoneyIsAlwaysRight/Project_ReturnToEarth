using System.Collections;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private Animator fishAnimator;
    [SerializeField] private GameObject fishPrefab;
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
            Instantiate(fishPrefab, transform.position, transform.rotation);
        }
    }
}
