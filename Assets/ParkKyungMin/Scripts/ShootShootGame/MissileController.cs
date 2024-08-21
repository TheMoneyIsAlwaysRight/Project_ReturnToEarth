using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : PooledObject
{
    [SerializeField] int hp;
    [SerializeField] PooledObject destructionVFX;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShootPlayerController.instance.GetDamage(1);
        }
        else if (collision.tag == "Projectile") // 플레이어의 발사체와 충돌 시
        {
            // 발사체의 데미지를 가져와 미사일에 적용
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
            {
                GetDamage(projectile.damage);
                // Release();   // 발사체 파괴
            }
        }
    }

    private void GetDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        Manager.Pool.GetPool(destructionVFX, transform.position, Quaternion.identity);
        Release();
    }
}
