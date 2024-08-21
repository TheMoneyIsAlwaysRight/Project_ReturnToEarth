using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minRotationSpeed;  // 최소 회전 속도
    [SerializeField] float maxRotationSpeed;  // 최대 회전 속도

    float rotationSpeed;


    private void Start()
    {
        Destroy(gameObject, 5);  // 5초뒤에 삭제

        // 최소 회전속도와 최대 회전속도에서 랜덤
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private void Update()
    {
        // 직선 이동
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);

        // 회전
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
