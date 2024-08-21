using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class RunPlayerController : MonoBehaviour
{
    [SerializeField] GameObject gameoverUI;

    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float moveSpeed;
    private Vector2 moveInput;

    [SerializeField] AudioSource dieSound;
    [SerializeField] AudioSource bgm;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        Move();
        playerAnimator.SetFloat("Run", Mathf.Abs(rigidbody.velocity.x));
    }

    private void Move()
    {
        rigidbody.velocity = new Vector2(moveInput.x * moveSpeed, rigidbody.velocity.y);
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

    private IEnumerator Die()
    {

        dieSound.Play();

        yield return new WaitForSeconds(0.2f);
        GameOver();
    }

    private void GameOver()
    {
        // 사운드 제외하고 일시정지 시키기
        Time.timeScale = 0;

        gameoverUI.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Die());
    }

}
