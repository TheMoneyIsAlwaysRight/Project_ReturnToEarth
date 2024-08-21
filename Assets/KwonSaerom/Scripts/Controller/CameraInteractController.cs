using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 카메라 줌인이 있는 오브젝트는 해당 추상클래스를 상속받아 구현.
public abstract class CameraInteractController : InteractObject
{
    [Header("카메라 확대해도 주변 오브젝트들도 터치할 수 있나여부.(디폴트는 본인만 터치가능)")]
    [SerializeField] public bool OnInteractAround;

    private Vector2 zoominTransform;
    protected CameraController cameraController;

    protected override void Start()
    {
        base.Start();
        cameraController = Manager.Game.Camera;
        zoominTransform = GetComponentInChildren<Collider2D>().offset;
    }

    public override void Interact()
    {
        if(cameraController.CurrentCamState == CameraController.CameraState.ZoomInObject)
        {
            PuzzleOnInteract();
            return;
        }

        cameraController.SetZoomInCameraPos(zoominTransform);
        cameraController.CurrentCamState = CameraController.CameraState.ZoomInObject;
    }

    public override void Load()
    {
        gameObject.SetActive(false);
    }

    public abstract void PuzzleOnInteract();
}
