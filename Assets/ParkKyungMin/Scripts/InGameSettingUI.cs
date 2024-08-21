using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InGameSettingUI : BaseUI
{
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject givingUI;


    public void OnGivingUP()
    {
        givingUI.SetActive(true);
        settingUI.SetActive(false);
    }

    public void ChapterSceneLoad()
    {
        //게임 포기시의 게임 로그 추가
        Manager.Game.SaveGameLog(false);
        Manager.Resource.Clear();
        Manager.Scene.LoadScene("ChapterScene");
    }

    public void OnSetting()
    {
        givingUI.SetActive(false);
        settingUI.SetActive(true);
    }

    public override void LocalUpdate()
    {

    }
}
