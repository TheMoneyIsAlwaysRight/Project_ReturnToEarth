using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShootPlayerController : MonoBehaviour
{
    [SerializeField] GameObject destructionFX;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] AudioSource dieSound;

    public float point;

    public static ShootPlayerController instance;

    private bool invulnerable = false; // 무적 상태 초기화

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    public void GetDamage(int damage)   
    {
        if (invulnerable == true)
            return;

        Destruction();
        gameOverUI.SetActive(true);

        Time.timeScale = 0;
    }    
    
    // 찬규 : 이건 생성이 되는거긴 한데 플레이어의 기체가 파괴되면 한번 나오는거 같으니까
    // 굳이 풀링까지는 안해도 될 듯 싶음
    void Destruction()
    {
        dieSound.Play();
        Instantiate(destructionFX, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }

    public void Invincibility()
    {
        invulnerable = true;
    }
}
