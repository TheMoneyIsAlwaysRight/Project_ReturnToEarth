using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterToken : BaseUI
{
    enum Texts
    {
        ChapterText
    }

    [SerializeField] List<StageToken> stages;

    public int ChapterKey { get; set; }

    private int curChapterTextKey;
    private int curChapterImageKey;
    private const int START_CHAPTER_TEXT_KEY = 102503;
    private const int START_CHAPTER_IMAGE_KEY = 101500;

    private void Start()
    {
        curChapterTextKey = START_CHAPTER_TEXT_KEY + (ChapterKey - Define.START_CHAPTER) / 3;
        curChapterImageKey = START_CHAPTER_IMAGE_KEY + (ChapterKey - Define.START_CHAPTER);

        for(int i=0;i<stages.Count;i++)
        {
            stages[i].GetComponent<Image>().sprite = Manager.Resource.Load<Sprite>((curChapterImageKey + i).ToString());
            stages[i].ChapterKey = this.ChapterKey + i;
        }
        LocalUpdate();
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.ChapterText.ToString()).text = LanguageSetting.GetLocaleText(curChapterTextKey);
    }

}
