using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : PopUpUI
{
    [SerializeField] Slider sFXSlider;
    [SerializeField] Slider bGMSlider;
    [SerializeField] Button text_KR;
    [SerializeField] Button text_EN;
    // 그 외 아래 버튼들의 기능을 만들어야 한다면 추가해서 만들면 됨

    protected override void Awake()
    {
        base.Awake();
        LocalUpdate();
    }

    public override void LocalUpdate()
    {

    }

    public void SFXCon()
    {
        float sound = sFXSlider.value;

        if (sound == -40f) Manager.Sound.Mixer.SetFloat("SFXVolume", -80);
        else Manager.Sound.Mixer.SetFloat("SFXVolume", sound);
    }
    
    public void BGMCon()
    {
        float sound = bGMSlider.value;

        if (sound == -40f) Manager.Sound.Mixer.SetFloat("BGMVolume", -80);
        else Manager.Sound.Mixer.SetFloat("BGMVolume", sound);
    }

    public void LanguageKR()
    {
        Manager.Game.LanguageSet = Define.LanguageSet.KR;
    }
    
    public void LanguageEN()
    {
        Manager.Game.LanguageSet = Define.LanguageSet.EN;
    }

    
}
