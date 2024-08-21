using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] Animator fishAnimator;

    private void Start()
    {
        Debug.Log("물고기 등장");
        Destroy(gameObject, 3);
    }

    public void Die()
    {
        fishAnimator.Play("fishDie");
    }
}
 