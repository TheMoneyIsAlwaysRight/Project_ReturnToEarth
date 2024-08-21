using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 권새롬 추가
/// 카메라 줌인되고 아이템을 획득 or 오브젝트 활성화 할 수 있는 컨트롤러
/// </summary>
public class CameraZoomInInteract : CameraInteractController
{
    [Header("상호작용 후 없어질건지 여부 : True면 없어짐")]
    [SerializeField] bool isInteractAfterNoneActive;
    bool isInteract = false;
    public override void PuzzleOnInteract()
    {
        if (isInteract)
            return;

        isInteract = true;
        InteractObjectItem();
        Close();
        if (isInteractAfterNoneActive)
            gameObject.SetActive(false);
    }

    public override void Load()
    {
        isInteract = true;
        InteractObjectItem(true);
        gameObject.SetActive(!isInteractAfterNoneActive);
    }

}
