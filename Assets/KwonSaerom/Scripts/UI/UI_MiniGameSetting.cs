using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_MiniGameSetting : BaseUI
{

    enum Texts
    {
        SettingText,
        BGMText,
        SFXText,
        OnText,
        OffText,
        GiveupText,
        RealGiveupText,
        ReturnText,
        GiveUp
    }

    enum GameObjects
    {
        Button_On,
        Button_Off
    }


    // Start is called before the first frame update
    void Start()
    {
        GetUI<Button>(GameObjects.Button_On.ToString()).onClick.AddListener(() => OnClickedLanguage(LanguageSet.KR));
        GetUI<Button>(GameObjects.Button_Off.ToString()).onClick.AddListener(() => OnClickedLanguage(LanguageSet.EN));
        LocalUpdate();
    }

    public void OnClickedLanguage(LanguageSet set)
    {
        Manager.Game.LanguageSet = set;
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.SettingText.ToString()).text = LanguageSetting.GetLocaleText(102184);
        GetUI<TMP_Text>(Texts.BGMText.ToString()).text = LanguageSetting.GetLocaleText(102187);
        GetUI<TMP_Text>(Texts.SFXText.ToString()).text = LanguageSetting.GetLocaleText(102186);
        GetUI<TMP_Text>(Texts.OnText.ToString()).text = LanguageSetting.GetLocaleText(102192);
        GetUI<TMP_Text>(Texts.OffText.ToString()).text = LanguageSetting.GetLocaleText(102193);
        GetUI<TMP_Text>(Texts.GiveupText.ToString()).text = LanguageSetting.GetLocaleText(102194);

        GetUI<TMP_Text>(Texts.RealGiveupText.ToString()).text = LanguageSetting.GetLocaleText(102206);
        GetUI<TMP_Text>(Texts.ReturnText.ToString()).text = LanguageSetting.GetLocaleText(102207);
        GetUI<TMP_Text>(Texts.GiveUp.ToString()).text = LanguageSetting.GetLocaleText(102208);
    }

}
