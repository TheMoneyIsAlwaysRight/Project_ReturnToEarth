using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : PooledObject
{

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyBullet && collision.tag == "Player")
        {
            ShootPlayerController.instance.GetDamage(damage);
            Debug.Log("플레이어 충돌");

            Destruction();
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            Debug.Log("에너미 충돌");
   
            Destruction();
        }
    }

    void Destruction()
    {
        Release();
    }

}


