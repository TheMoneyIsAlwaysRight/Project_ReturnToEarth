using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class Targets
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("prefab")]
    public GameObject wave;
}
#endregion

public class TargetController : MonoBehaviour
{
    [SerializeField] Targets[] targets;   // 타겟 배열

    private void Start()
    {
        // 타겟 순차적 생성
        for (int i = 0; i < targets.Length; i++)
        {
            StartCoroutine(CreateTarget(targets[i].timeToStart, targets[i].wave));
        }
    }

    IEnumerator CreateTarget(float delay, GameObject targetPrefab)
    {
        // 지연 시간 동안 대기
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        // 타겟 생성
        Instantiate(targetPrefab);
    }
}
