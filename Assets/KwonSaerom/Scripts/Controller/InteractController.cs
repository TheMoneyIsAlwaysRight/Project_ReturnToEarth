using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractController : MonoBehaviour
{
    [SerializeField] float clickRange;

    private IInteractable nowInteract;
    public IInteractable NowInteract { get { return nowInteract; } }

    Collider2D[] colliders = new Collider2D[100];


    private void Awake()
    {
        Manager.Game.Inter = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //만약 터치를 했을 시
            InteractContol();
    }

    private void InteractContol()
    {
        Manager.Game.InGameUserLog.AllTouchCount++;
        Vector3 touchPosToVector = Input.mousePosition;

        //해당 클릭에 UI가 걸리면 interact 터치를 발동시키지 않는다.
        if (IsPointerOverUIElement(touchPosToVector))
            return;

        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touchPosToVector);
        transform.position = touchPos;

        //상호작용하는 놈들 있는지 검사
        int size = Physics2D.OverlapCircleNonAlloc (transform.position, clickRange, colliders);
        for (int i = 0; i < size; i++)
        {
            IInteractable interactObject = colliders[i].gameObject.GetComponent<IInteractable>();
            if (interactObject != null)
            {
                //카메라가 줌인 상태이면, nowInteract에 따라 작동이 변하고,
                //카메라가 줌인상태가 아니면, 모든 오브젝트와 상호작용이 가능하다.
                if(Manager.Game.Camera.CurrentCamState == CameraController.CameraState.ZoomInObject)
                {
                    CameraInteractController cameraInteract = nowInteract as CameraInteractController;
                    if (cameraInteract != null && interactObject != nowInteract && !cameraInteract.OnInteractAround)
                        break;
                }
                nowInteract = interactObject; 
                interactObject.Interact();
                Manager.Game.InGameUserLog.InteractTouchCount++;
            }

        }
        // 아이템 매니저가 클릭했던 정보들을 리셋한다. -> 안해도된다함...
        //ItemManager.Instance.ClickedReset();
    }
    

    public void InteractChange(IInteractable inter)
    {
        StartCoroutine(CoChangeInteract(inter));
    }


    public void CloseInteract()
    {
        nowInteract.Close();
        nowInteract = null;
    }

    /// <summary>
    /// 버튼에 할당하는 (뒤로가기 버튼)
    /// </summary>
    public void OnClickClose()
    {
        if (Manager.Game.Camera.CurrentCamState == CameraController.CameraState.ZoomInObject)
            Manager.Game.Camera.CurrentCamState = CameraController.CameraState.Base;
        CloseInteract();
    }

    private bool IsPointerOverUIElement(Vector2 screenPosition)
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    IEnumerator CoChangeInteract(IInteractable inter)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        nowInteract = inter;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, clickRange);
    }
}
