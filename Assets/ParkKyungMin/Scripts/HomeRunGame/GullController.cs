using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullController : MonoBehaviour 
{
    [SerializeField] Animator gullAnimator;

    private void Start()
    {
        Debug.Log("갈매기 등장");
        Destroy(gameObject, 2);
    }

    public void Die()
    {
        gullAnimator.Play("GullDie");
    }
}
