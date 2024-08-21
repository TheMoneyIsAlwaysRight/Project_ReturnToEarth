using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_QuitGame : PopUpUI
{
    enum Texts
    {
        Log,
        NoText,
        YesText
    }


    private void Start()
    {
        LocalUpdate();
    }
    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.Log.ToString()).text = LanguageSetting.GetLocaleText(102518);
        GetUI<TMP_Text>(Texts.NoText.ToString()).text = LanguageSetting.GetLocaleText(102519);
        GetUI<TMP_Text>(Texts.YesText.ToString()).text = LanguageSetting.GetLocaleText(102520);

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
