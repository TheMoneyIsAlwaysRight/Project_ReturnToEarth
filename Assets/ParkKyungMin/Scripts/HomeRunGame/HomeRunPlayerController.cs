using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HomeRunPlayerController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject batCollider;
    [SerializeField] AudioSource dieSound;
    [SerializeField] int damaged;           // Player 충돌 확인 변수 

    [SerializeField] GameObject gameOverUI;

    private bool playerInvincible = true;
    private bool isTakingDamage = false; // 데미지 처리 체크


    private void Start()
    {
        StartCoroutine(DeactivateInvincibility());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIElement() == false)
            {
                playerAnimator.SetTrigger("Swing");
            }
        }
    }

    private bool IsPointerOverUIElement()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();
#elif UNITY_IOS || UNITY_ANDROID
        return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
    }

    public IEnumerator DeactivateInvincibility()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("무적 해제");
        playerInvincible = false;
    }

    private IEnumerator Die()
    {
        dieSound.Play();
        gameOverUI.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        Time.timeScale = 0;
    }
    private IEnumerator TakeDamage()
    {
        isTakingDamage = true;
        damaged++;

        if (damaged >= 3)
        {
            StartCoroutine(Die());
        }

        // 데미지 후 일정 시간 동안 추가 충돌 무시
        yield return new WaitForSeconds(0.5f);
        isTakingDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerInvincible || isTakingDamage)
        {
            Debug.Log("무적이지롱");
            return; 
        }

        StartCoroutine(TakeDamage());
    }

}