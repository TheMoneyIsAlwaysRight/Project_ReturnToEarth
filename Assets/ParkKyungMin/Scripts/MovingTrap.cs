using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] Transform targetPosition; // 이동할 목표 위치
    [SerializeField] float speed; // 이동 속도


    void Update()
    {
        MoveTrap();
    }

    void MoveTrap()
    {
        if (Vector2.Distance(targetPosition.position, transform.position) > 0.1f)
        {
            // 목적지까지의 방향 벡터 계산
            Vector2 direction = ((Vector2)targetPosition.position - (Vector2)transform.position).normalized;

            // 타일을 목적지 방향으로 일정 속도로 이동
            transform.position += (Vector3)(direction * Time.deltaTime * speed);
        }
        else
        {
            transform.position = targetPosition.position;
        }
    }
}
