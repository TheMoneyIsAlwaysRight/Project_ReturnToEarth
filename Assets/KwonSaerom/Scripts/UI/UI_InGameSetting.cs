using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InGameSetting : PopUpUI
{
    enum Texts
    {
        SettingText,
        BGMText,
        SFXText,
        OnText,
        OffText,
        GiveupText
    }

    private void Start()
    {
        LocalUpdate();
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.SettingText.ToString()).text = LanguageSetting.GetLocaleText(102184);
        GetUI<TMP_Text>(Texts.BGMText.ToString()).text = LanguageSetting.GetLocaleText(102187);
        GetUI<TMP_Text>(Texts.SFXText.ToString()).text = LanguageSetting.GetLocaleText(102186);
        GetUI<TMP_Text>(Texts.OnText.ToString()).text = LanguageSetting.GetLocaleText(102192);
        GetUI<TMP_Text>(Texts.OffText.ToString()).text = LanguageSetting.GetLocaleText(102193);
        GetUI<TMP_Text>(Texts.GiveupText.ToString()).text = LanguageSetting.GetLocaleText(102194);
    }

}
