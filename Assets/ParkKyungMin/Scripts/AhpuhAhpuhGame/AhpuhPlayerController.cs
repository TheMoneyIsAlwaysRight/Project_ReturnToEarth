using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class AhpuhPlayerController : MonoBehaviour
{
    [SerializeField] float moveDistance;  // 한 번에 이동할 거리
    [SerializeField] float moveSpeed;     // 이동 속도
    public float starPoint;     // 먹은 별 갯수

    [SerializeField] GameObject star;   // 별 오브젝트
    [SerializeField] PlayerInput input;
    [SerializeField] AudioSource pointSound;
    [SerializeField] AudioSource dieSound;

    [SerializeField] GameObject gameoverUI;
    [SerializeField] StarScoreUI starScoreUI;

    private bool isMove = false;  // 이동 여부
    private Vector3 destination;  // 목표 위치
    private bool moveUp = true;   // 이동 방향 (true면 위로, false면 아래로)


    void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 이동 방향 전환
            moveUp = !moveUp;

            // 목표 위치 설정
            if (moveUp)
            {
                destination = new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z);
            }
            else
            {
                destination = new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z);
            }

            // 이동 시작
            isMove = true;
        }

        // 이동 처리
        if (isMove)
        {
            Move();
        }
    }

    private void Move()
    {
        // 목표 위치와 현재 위치의 거리가 0.1 이하인 경우 이동 종료
        if (Vector3.Distance(destination, transform.position) <= 0.1f)
        {
            isMove = false;
            transform.position = destination;  // 목표 위치로 정확히 설정
        }
        else
        {
            // 목표 위치를 향해 이동
            Vector3 direction = destination - transform.position; // 이동 방향
            transform.position += direction.normalized * moveSpeed * Time.deltaTime; // 이동
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Debug.Log(" 옴뇸뇸 ");
            pointSound.Play();
            Destroy(collision.gameObject);  // 충돌한 오브젝트 제거
            starPoint++;

            // 점수 업데이트 호출
            starScoreUI.UpdateScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(" 넌 죽었다 ");
        Die();
    }

    private void Die()
    {
        input.enabled = false;
        dieSound.Play();

        // 사운드 제외하고 일시정지 시키기
        Time.timeScale = 0;

        gameoverUI.SetActive(true);
    }
}
