using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeRunGameSettingUI : BaseUI
{

    [SerializeField] GameObject settingUI;  // 세팅창
    [SerializeField] AudioSource introBGM;
    [SerializeField] AudioSource miniGameBGM;

    public override void LocalUpdate()
    {

    }

    public void OnSetting()
    {
        // 세팅창 활성화
        settingUI.SetActive(true);
        introBGM.Pause();
        miniGameBGM.Pause();
        Time.timeScale = 0;
    }

    public void SettingOut()
    {
        settingUI.SetActive(false);
        // 일시정지 해제
        introBGM.UnPause();
        miniGameBGM.UnPause();
        Time.timeScale = 1;
    }
}


