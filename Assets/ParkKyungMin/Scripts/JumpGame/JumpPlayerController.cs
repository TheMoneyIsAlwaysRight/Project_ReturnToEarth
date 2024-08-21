using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPlayerController : MonoBehaviour
{
    [SerializeField] GameObject gameoverUI;

    public Rigidbody2D playerRigidbody;
    [SerializeField] PlayerInput input;

    [SerializeField] float jumpSpeed;     // 점프
    [SerializeField] float moveSpeed;     // 이동 속도

    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource dieSound;
    [SerializeField] AudioSource bgm;

    private Vector2 moveInput;
    public bool isGrounded = true;
    public bool isJumping = false;  // 점프 상태를 나타내는 변수


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Ready()
    {
        input.enabled = false;
        playerRigidbody.simulated = false;
    }

    public void Go()
    {
        input.enabled = true;
        playerRigidbody.simulated = true;
        bgm.Play();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.velocity.y);

        // 스프라이트를 반전
        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void RightMove()
    {
        moveInput = Vector2.right * moveSpeed;

    }

    public void LeftMove()
    {
        moveInput = Vector2.left * moveSpeed;
    }

    public void DontMove()
    {
        moveInput = Vector2.zero;
    }

    public void MoveUP()
    {
        if (isGrounded)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            jumpSound.Play();
            isGrounded = false;
            Debug.Log(" 점프중 ");
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    private void OnJump(InputValue value)
    {
        MoveUP();
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
