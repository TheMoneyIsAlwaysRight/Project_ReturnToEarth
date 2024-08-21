using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;
public class LanguageSetting
{
    public static string GetLocaleText(int key)
    {
        TextTable textTable;
        if (!Manager.Data.TextTables.ContainsKey(key))
        {
            Debug.Log("텍스트 테이블에 텍스트가 없음");
            Manager.UI.ClosePopUpUI();
            return "";
        }

        textTable = Manager.Data.TextTables[key];

        switch (Manager.Game.LanguageSet)
        {
            case LanguageSet.KR:
                return textTable.Text_KR;
            case LanguageSet.EN:
                return textTable.Text_EN;
            case LanguageSet.SP:
                return textTable.Text_SP;
            case LanguageSet.FR:
                return textTable.Text_FR;
            default:
                return textTable.Text_EN;
        }
    }
}
