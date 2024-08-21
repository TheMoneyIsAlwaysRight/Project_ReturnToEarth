using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 클릭했을때 단순히 메시지만 나오는 상호작용 오브젝트
/// </summary>
public class ClickInteract : InteractObject
{
    private bool isConnect = false;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(info.CanInteract);
    }

    public override void Interact()
    {
        UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
        if (Manager.Data.TextBundleTables.ContainsKey(info.interactText))
        {
            TextBundleTable tt = Manager.Data.TextBundleTables[info.interactText];
            popup.SetLog(tt.ID);

        }

        if (isConnect == true)
            return;
        InteractObjectItem();
        isConnect = true;
    }

    public override void Load()
    {
        gameObject.SetActive(false);
    }
}
