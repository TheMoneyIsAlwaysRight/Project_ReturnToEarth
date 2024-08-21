using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    [SerializeField] Animator wormAnimator;

    private void Start()
    {
        Debug.Log("모기 등장");
        Destroy(gameObject, 2.6f);
    }

    public void Die()
    {
        wormAnimator.Play("WormDie");
    }
}
   