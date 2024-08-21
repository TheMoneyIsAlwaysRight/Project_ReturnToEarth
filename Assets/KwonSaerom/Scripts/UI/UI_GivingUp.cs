using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GivingUp : PopUpUI
{
    enum Texts
    {
        OKText,
        ReturnText,
        GiveUpText
    }

    private void Start()
    {
        LocalUpdate();
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.OKText.ToString()).text = LanguageSetting.GetLocaleText(102208);
        GetUI<TMP_Text>(Texts.ReturnText.ToString()).text = LanguageSetting.GetLocaleText(102207);
        GetUI<TMP_Text>(Texts.GiveUpText.ToString()).text = LanguageSetting.GetLocaleText(102206);
    }
}
