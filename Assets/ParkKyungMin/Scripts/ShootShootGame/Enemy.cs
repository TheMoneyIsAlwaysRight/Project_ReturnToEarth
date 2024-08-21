using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : PooledObject
{
    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public PooledObject projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public PooledObject destructionVFX;
    public PooledObject hitEffect;
    
    [HideInInspector] public int shotChance;    // 경로를 따라가는 동안 적의 발사 확률
    [HideInInspector] public float shotTimeMin, shotTimeMax;    // 경로를 시작한 후 발사까지의 최소 및 최대 시간

    private ShootPlayerController playerController;

    #endregion

    private void Start()
    {
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));

        playerController = ShootPlayerController.instance;
    }

    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                            
        {
            Manager.Pool.GetPool(projectile, gameObject.transform.position, Quaternion.identity);        
        }
    }

    public void GetDamage(int damage) 
    {
        health -= damage;
        if (health <= 0)
            Destruction();
        else
            Manager.Pool.GetPool(hitEffect, transform.position, Quaternion.identity);
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (Projectile.GetComponent<Projectile>() != null)
            //    ShootPlayerController.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            //else
                ShootPlayerController.instance.GetDamage(1);
        }
    }
  

    void Destruction()                           
    {        
        playerController.point++;
        Manager.Pool.GetPool(destructionVFX, transform.position, Quaternion.identity);
        Release();
    }
}
