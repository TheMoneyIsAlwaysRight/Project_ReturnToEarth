using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Story : PopUpUI
{
    enum Texts
    {
        StageSeeText,
        Text_Star
    }

    [SerializeField] List<ChapterToken> chapters;

    private void Start()
    {
        for(int i=0;i<chapters.Count;i++)
        {
            chapters[i].ChapterKey = Define.START_CHAPTER + i * 3;
        }
        LocalUpdate();
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.StageSeeText.ToString()).text = LanguageSetting.GetLocaleText(102220);
        GetUI<TMP_Text>(Texts.Text_Star.ToString()).text = ChapterManager.Instance.Score.ToString();
    }

}
