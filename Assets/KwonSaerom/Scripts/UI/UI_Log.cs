using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Log : PopUpUI
{
    enum GameObjects
    {
        Log
    }


    protected override void Awake()
    {
        base.Awake();
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void SetLog(int key)
    {
        if (Manager.Data.TextBundleTables.ContainsKey(key))
        {
            TextBundleTable textBundleTable = Manager.Data.TextBundleTables[key];
            key = textBundleTable.TEXT[0];
        }

        GetUI<TMP_Text>(GameObjects.Log.ToString()).text = LanguageSetting.GetLocaleText(key);
    }

    public void SetLog(string message)
    {
        GetUI<TMP_Text>(GameObjects.Log.ToString()).text = message;
    }
}
