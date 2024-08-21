using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLayerInteract : CameraInteractController
{
    [SerializeField] Collider2D firstCollider;
    [SerializeField] Collider2D secondCollider;

    private bool isInteract = false;

    protected override void Awake()
    {
        base.Awake();
        firstCollider.enabled = true;
        secondCollider.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();
        firstCollider.enabled = false;
        secondCollider.enabled = true;
    }

    public override void PuzzleOnInteract()
    {
        if (isInteract)
            return;

        isInteract = true;
        InteractObjectItem();
    }

    public override void Load()
    {
        isInteract = true;
        InteractObjectItem(true);
    }

    public override void Close()
    {
        base.Close();
        firstCollider.enabled = true;
        secondCollider.enabled = false;
    }

}
