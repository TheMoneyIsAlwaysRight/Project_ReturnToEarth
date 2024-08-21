using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WallMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed;  // 이동 속도

    // 이동 범위(최대값, 최소값)
    [SerializeField] Vector2 moveRange = new Vector2(-2.0f, 2.0f);

    private float targetY;  // 목표 Y 위치
    private float direction = 1f;  // 이동 방향

    private void Start()
    {
        // 초기 목표 Y 위치를 설정
        targetY = transform.position.y;
    }

    private void Update()
    {
        // 목표 위치에 도달했는지 확인
        if (Mathf.Abs(transform.position.y - targetY) < 0.1f)
        {
            // 새로운 목표 Y 위치를 랜덤하게 설정
            targetY = Random.Range(moveRange.x, moveRange.y);

            // (targetY > transform.position.y) true이면 1f false이면 -1f 
            direction = (targetY > transform.position.y) ? 1f : -1f;
        }

        // 목표 위치를 향해 이동
        transform.Translate(Vector2.up * moveSpeed * direction * Time.deltaTime);

        // 이동 방향을 목표 위치에 따라 조정
        if ((direction > 0 && transform.position.y > targetY) || (direction < 0 && transform.position.y < targetY))
        {
            transform.position = new Vector2(transform.position.x, targetY);
        }
    }
}