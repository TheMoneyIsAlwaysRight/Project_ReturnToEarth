using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageToken : BaseUI
{
    [SerializeField] UI_Script scriptUI;
    [SerializeField] GameObject lockImg;

    public int ChapterKey { get; set; }

    private int curStageTextKey;

    private const int START_STAGE_TEXT_KEY = 102222;
    private const int START_STAGE_TEXT_KEY_2 = 102463; //무인도 텍스트는 떨어져있음
    private const int LAST_STAGE_KEY = 108018; //마지막 스테이지 key

    private void Start()
    {
        if(ChapterKey >= Define.HIDDEN_CHAPTER - 1) //무인도 챕터일때
            curStageTextKey = START_STAGE_TEXT_KEY_2 + (ChapterKey - Define.HIDDEN_CHAPTER + 1);
        else
            curStageTextKey = START_STAGE_TEXT_KEY + (ChapterKey - Define.START_CHAPTER);

        if (ChapterKey == Define.LAST_CHAPTER)
            lockImg.SetActive(!ChapterManager.Instance.openStage.Contains(ChapterKey));
        else
            lockImg.SetActive(!ChapterManager.Instance.openStage.Contains(ChapterKey + 1)); //잠금 이미지

        LocalUpdate();
    }

    enum Texts
    {
        StageText
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.StageText.ToString()).text = LanguageSetting.GetLocaleText(curStageTextKey);
    }

    public void OnClickStory()
    {
        UI_Script ui_script = Manager.UI.ShowPopUpUI(scriptUI);
        ui_script.CharacterScripts.Execute(ChapterKey, NewCharacterScripts.BackUI.StoryUI);
    }
}
