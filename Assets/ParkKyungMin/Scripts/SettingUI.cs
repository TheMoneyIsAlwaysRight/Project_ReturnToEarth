using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] GameObject settingUI;  // 세팅창
    [SerializeField] AudioSource miniGameBGM;

    public override void LocalUpdate()
    {

    }

    public void OnSetting()
    {
        // 세팅창 활성화
        settingUI.SetActive(true);
        if (miniGameBGM != null)
        {
            miniGameBGM.Pause();
        }
        Time.timeScale = 0;
    }

    public void SettingOut()
    {
        settingUI.SetActive(false);
        // 일시정지 해제
        if (miniGameBGM != null)
        {
            miniGameBGM.UnPause();
        }
        Time.timeScale = 1;
    }
}
