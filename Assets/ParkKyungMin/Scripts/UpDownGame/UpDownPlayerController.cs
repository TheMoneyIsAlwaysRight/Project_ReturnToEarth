using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking.Types;

public class UpDownPlayerController : MonoBehaviour
{
    [SerializeField] GameObject gameoverUI;

    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] PlayerInput input;
    [SerializeField] new Collider2D collider;
    [SerializeField] Animator playerAnimator;

    [SerializeField] float jumpSpeed;

    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource dieSound;
    [SerializeField] AudioSource bgm;

    private void Awake()
    {
         playerAnimator.SetBool("isDie", false);
    }

    private void Update()
    {
        if (rigidbody.velocity.y <= 0)
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }

    public void Ready()
    {
        input.enabled = false;
        rigidbody.simulated = false;
    }

    public void Fly()
    {
        input.enabled = true;
        rigidbody.simulated = true;
        bgm.Play();
    }

    public void Jump()
    {
        rigidbody.velocity = Vector2.up * jumpSpeed;
        jumpSound.Play();
        playerAnimator.SetBool("isJumping", true);
    }

    private IEnumerator Die()
    {
        input.enabled = false;
        dieSound.Play();
        playerAnimator.SetBool("isDie", true);

        yield return new WaitForSeconds(0.1f);
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

    private void OnJump(InputValue value)
    {
        Jump();
    }
}