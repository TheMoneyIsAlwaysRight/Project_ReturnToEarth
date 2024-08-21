using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpCameraController : MonoBehaviour
{
    [SerializeField] Transform player; // Player Transform.
    [SerializeField] JumpPlayerController playerController;
    [SerializeField] float upperBound; // 카메라가 따라갈 수 있는 최대 y 위치
    [SerializeField] float lowerBound; // 카메라가 따라갈 수 있는 최소 y 위치
    [SerializeField] float smoothTime; // 부드럽게 이동하는 시간

    private Vector3 offset; // 초기 카메라와 플레이어 간의 오프셋

    private Vector3 velocity = Vector3.zero; // 현재 속도

    void Start()
    {
        // 초기 오프셋계산
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 새로운 카메라 위치를 계산
        Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);

        // 새로운 위치가 상한과 하한 내에 있는지 확인
        if (targetPosition.y > upperBound)
        {
            targetPosition.y = upperBound;
        }
        else if (targetPosition.y < lowerBound)
        {
            targetPosition.y = lowerBound;
        }

        // 카메라 위치 업데이트
        if (!playerController.isJumping)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        
    }
}



