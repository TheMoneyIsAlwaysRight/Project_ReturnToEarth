using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX;
}

public class PlayerShooting : MonoBehaviour
{

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public PooledObject playerBullet;


    [HideInInspector] public float nextFire;    // 다음 발사 시간


    [Tooltip("current weapon power")]
    [Range(1, 4)]
    public int weaponPower = 1;     // 무기 파워 설정

    public Guns guns;
    bool shootingIsActive = true;   // 발사 활성화 여부
    [HideInInspector] public int maxweaponPower = 4;    // 최대 무기 파워
    public static PlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        // 풀 생성
        Manager.Pool.CreatePool(playerBullet, 40, 100);

        // 각 총의 발사 효과 가져오기
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // 발사가 활성화되어 있을 때
        if (shootingIsActive)
        {
            // 현재 시간이 다음 발사 시간보다 클 때
            if (Time.time > nextFire)
            {
                MakeAShot(); // 발사 메서드 호출
                nextFire = Time.time + 1 / fireRate; // 다음 발사 시간 설정
            }
        }


        void MakeAShot()
        {
            switch (weaponPower)
            {
                case 1: // 파워가 1일 때
                    CreateLazerShot(playerBullet, guns.centralGun.transform.position, Vector3.zero);
                    guns.centralGunVFX.Play();
                    break;
                case 2: // 파워가 2일 때
                    CreateLazerShot(playerBullet, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    guns.leftGunVFX.Play();
                    CreateLazerShot(playerBullet, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                    guns.rightGunVFX.Play();
                    break;
                case 3: // 파워가 3일 때
                    CreateLazerShot(playerBullet, guns.centralGun.transform.position, Vector3.zero);
                    CreateLazerShot(playerBullet, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    guns.leftGunVFX.Play();
                    CreateLazerShot(playerBullet, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                    guns.rightGunVFX.Play();
                    break;
                case 4: // 파워가 4일 때
                    CreateLazerShot(playerBullet, guns.centralGun.transform.position, Vector3.zero);
                    CreateLazerShot(playerBullet, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    guns.leftGunVFX.Play();
                    CreateLazerShot(playerBullet, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                    guns.rightGunVFX.Play();
                    CreateLazerShot(playerBullet, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                    CreateLazerShot(playerBullet, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                    break;
            }
        }

        // 찬규 : 발사체의 인스턴스를 생성하는데 이걸 모두 생성해서 쓰면 박살이 나기 때문에
        // 풀링을 해서 쓰도록 해보자
        void CreateLazerShot(PooledObject lazer, Vector3 pos, Vector3 rot)
        {
            Manager.Pool.GetPool(lazer, pos, Quaternion.Euler(rot));     // 발사체 인스턴스 생성
        }
    }
}
