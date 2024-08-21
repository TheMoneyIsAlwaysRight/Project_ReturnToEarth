using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorial : MonoBehaviour
{
    [SerializeField] int TextBundleKey;

    private UI_Log logPrefab;
    private int[] textKeys;

    private void Start()
    {
        logPrefab = Manager.Resource.Load<UI_Log>("UI_Log");
        textKeys = Manager.Data.TextBundleLoad(TextBundleKey).TEXT;
    }


    public void TutorialLog()
    {
        for (int i = textKeys.Length - 1; i >= 0; i--)
        {
            UI_Log log = Manager.UI.ShowPopUpUI(logPrefab);
            log.SetLog(textKeys[i]);
        }
    }

}
