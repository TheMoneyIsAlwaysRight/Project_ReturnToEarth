using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] AudioSource batSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("맞췄다");

        batSFX.Play();

        FishController fishController = collision.GetComponent<FishController>();
        if (fishController != null)
        {
            fishController.Die();
        }

        WormController wormController = collision.GetComponent<WormController>();
        if (wormController != null)
        {
            wormController.Die();
        }

        GullController gullController = collision.GetComponent<GullController>();
        if (gullController != null)
        {
            gullController.Die();
        }
    }
}
