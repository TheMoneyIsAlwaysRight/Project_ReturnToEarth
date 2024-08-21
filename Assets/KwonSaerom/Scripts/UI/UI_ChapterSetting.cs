using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_ChapterSetting : PopUpUI
{
    enum Texts
    {
        SettingText,
        WithdrawalText,
        ChangeNicknameText,
        InquiryText,
        CouponText,
        PolicyText,
        TermsOfUseText,
        ConnectedText,
        BGMText,
        SFXText,
        LanguageText
    }

    enum GameObjects
    {
        Dropdown
    }

    private void Start()
    {
        LocalUpdate();
        InitLocaleDropDown();
    }

    // 다국어 설정 드롭다운 초기 세팅.
    private void InitLocaleDropDown()
    {
        TMP_Dropdown dropdown = GetUI<TMP_Dropdown>(GameObjects.Dropdown.ToString());
        dropdown.onValueChanged.AddListener(LanguageChange);
        //옵션을 테이블 연결하여
    }

    public void LanguageChange(int index)
    {
        Manager.Game.LanguageSet = (LanguageSet)index;
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.SettingText.ToString()).text = LanguageSetting.GetLocaleText(102184);
        GetUI<TMP_Text>(Texts.BGMText.ToString()).text = LanguageSetting.GetLocaleText(102187);
        GetUI<TMP_Text>(Texts.SFXText.ToString()).text = LanguageSetting.GetLocaleText(102186);
        GetUI<TMP_Text>(Texts.TermsOfUseText.ToString()).text = LanguageSetting.GetLocaleText(102199);
        GetUI<TMP_Text>(Texts.PolicyText.ToString()).text = LanguageSetting.GetLocaleText(102200);
        GetUI<TMP_Text>(Texts.CouponText.ToString()).text = LanguageSetting.GetLocaleText(102201);
        GetUI<TMP_Text>(Texts.ChangeNicknameText.ToString()).text = LanguageSetting.GetLocaleText(102202);
        GetUI<TMP_Text>(Texts.LanguageText.ToString()).text = LanguageSetting.GetLocaleText(102191);

        //애매쓰~
        GetUI<TMP_Text>(Texts.ConnectedText.ToString()).text = LanguageSetting.GetLocaleText(102197);
        GetUI<TMP_Text>(Texts.WithdrawalText.ToString()).text = LanguageSetting.GetLocaleText(102207);

    }
}
