using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{

    public float fallSpeed = 1f; // 내려가는 속도
    private float destroyTime = 5f; // 5초 후 파괴

    void Start()
    {
        // destroyTime 이후에 파괴
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // 아래로 내려가기
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

}
