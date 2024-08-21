using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public enum CameraState
    {
        Base, //기본 상태
        ZoomInObject, //카메라 줌인 상태
        GameCamera //게임 카메라
    }

    InGameScene gameScene;
    CinemachineBrain cinemachineBranin;
    CinemachineBlendDefinition blendDef;
    CinemachineBlendDefinition blendDef2;

    public CinemachineVirtualCamera virBaseCamera;
    public CinemachineVirtualCamera virZoomInCamera;
    public CinemachineVirtualCamera virGameCamera;


    private CameraState currentCam;
    public CameraState CurrentCamState
    {
        get { return currentCam; }
        set
        {
            currentCam = value;
            switch (currentCam)
            {
                case CameraState.Base:
                    cinemachineBranin.m_DefaultBlend = blendDef;
                    gameScene.SetBackButton(false);
                    virBaseCamera.Priority = 100;
                    virZoomInCamera.Priority = 10;
                    virGameCamera.Priority = 10;
                    break;
                case CameraState.ZoomInObject:
                    gameScene.SetBackButton(true);
                    virBaseCamera.Priority = 10;
                    virZoomInCamera.Priority = 100;
                    virGameCamera.Priority = 10;
                    break;
                case CameraState.GameCamera:
                    cinemachineBranin.m_DefaultBlend = blendDef2;
                    gameScene.SetButtons(false);
                    virBaseCamera.Priority = 10;
                    virZoomInCamera.Priority = 10;
                    virGameCamera.Priority = 100;
                    break;
            }
        }
    }

    private void OnPostRender()
    {
        //ScreenShot_Manager.Inst.IsReadyCap = true;
    }

    private void Awake()
    {
        gameScene = Manager.Scene.GetCurScene<InGameScene>();
        cinemachineBranin = GetComponent<CinemachineBrain>();

        blendDef = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1f);
        blendDef2 = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut,0.5f);
        cinemachineBranin.m_DefaultBlend = blendDef;

        Manager.Game.Camera = this;
    }

    private void Start()
    {
        Manager.Game.Camera.CurrentCamState = CameraState.Base;
    }

    public void SetZoomInCameraPos(Vector2 position)
    {
        virZoomInCamera.transform.position = new Vector3(position.x, position.y, virZoomInCamera.transform.position.z); ;
    }

    public Vector2 GetGameCamPosition()
    {
        return virGameCamera.gameObject.transform.position;
    }
}
