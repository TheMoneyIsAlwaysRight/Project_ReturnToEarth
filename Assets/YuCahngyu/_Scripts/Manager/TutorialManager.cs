using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] UserTutorial tutorial; // 튜토리얼 UI

    public void PullUpTutorial()
    {
        Manager.UI.ClosePopUpUI();
        Manager.UI.ShowPopUpUI(tutorial);
    }

    public void RePullUp()
    {
        Manager.UI.ShowPopUpUI(tutorial);
    }
}
