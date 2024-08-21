using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameSetting : PopUpUI
{
    [SerializeField] Slider sFXSlider;
    [SerializeField] Slider bGMSlider;

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
}
