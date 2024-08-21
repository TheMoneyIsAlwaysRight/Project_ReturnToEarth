using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    JumpPlayerController player;

    private void Awake()
    {
        player = GetComponentInParent<JumpPlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundBlock gb = collision.gameObject.GetComponent<GroundBlock>();
        Debug.Log(gb);
        if (gb != null)
        {
            player.isGrounded = true;
            gb.PlayDisappear();
        }
    }
}
