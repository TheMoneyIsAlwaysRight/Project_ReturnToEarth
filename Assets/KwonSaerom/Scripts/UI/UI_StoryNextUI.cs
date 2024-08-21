using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UI_StoryNextUI : BaseUI
{
    enum GameObjects
    {
        Star_One,
        Star_Two,
        Star_Three,
        StoryImage
    }

    enum Texts
    {
        RePlayStoryText,
        ReturnStoryText,
        NextStoryText,
        StoryText
    }

    [SerializeField] UI_Notice notice;

    public int ChapterKey { get; set; }
    public int LastImgKey { get; set; }

    private int curStageTextKey;
    private UI_Script scriptUI;
    private UI_Log logPrefab;

    protected override void Awake()
    {
        base.Awake();
        scriptUI = GetComponentInParent<UI_Script>();
        logPrefab = Manager.Resource.Load<UI_Log>("UI_Log");
        notice.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        curStageTextKey = Define.START_CLEAR_STORY_TEXT_KEY + (ChapterKey - Define.START_CHAPTER) * 2;
        GetUI<Image>(GameObjects.StoryImage.ToString()).sprite = Manager.Resource.Load<Sprite>(LastImgKey.ToString());

        SetStar();
        LocalUpdate();
    }

    public override void LocalUpdate()
    {
        GetUI<TMP_Text>(Texts.RePlayStoryText.ToString()).text = LanguageSetting.GetLocaleText(102509);
        GetUI<TMP_Text>(Texts.ReturnStoryText.ToString()).text = LanguageSetting.GetLocaleText(102510);
        GetUI<TMP_Text>(Texts.NextStoryText.ToString()).text = LanguageSetting.GetLocaleText(102511);

        GetUI<TMP_Text>(Texts.StoryText.ToString()).text = LanguageSetting.GetLocaleText(curStageTextKey);
    }

    public void Replay()
    {
        scriptUI.CharacterScripts.Execute(ChapterKey, NewCharacterScripts.BackUI.StoryUI);
        gameObject.SetActive(false);
    }

    public void ReturnStoryUI()
    {
        Manager.UI.ClosePopUpUI();
    }

    public void NextStory()
    {
        if (ChapterManager.Instance.openStage.Contains(ChapterKey + 2) == false)
        {
            notice.gameObject.SetActive(true);
            if (Manager.Game.LanguageSet == Define.LanguageSet.EN)
            {
                notice.NoticeText = "Notice";
                notice.DescriptionText = "It's the undeveloped stage.";
            }
            else
            {
                notice.NoticeText = "알림";
                notice.DescriptionText = "잠겨있습니다.";
            }
            notice.Setting();
            return;
        }

        ChapterKey++;
        scriptUI.CharacterScripts.Execute(ChapterKey, NewCharacterScripts.BackUI.StoryUI);
        gameObject.SetActive(false);
    }

    public void ExitNotice()
    {
        notice.gameObject.SetActive(false);
    }

    private void SetStar()
    {
        int nowKey = ChapterKey - Define.START_CHAPTER;
        int starNum = ChapterManager.Instance.Local[nowKey];

        if (starNum == 3)
        {
            GetUI(GameObjects.Star_One.ToString()).SetActive(false);
            GetUI(GameObjects.Star_Two.ToString()).SetActive(false);
            GetUI(GameObjects.Star_Three.ToString()).SetActive(true);
        }else if(starNum == 2 )
        {
            GetUI(GameObjects.Star_One.ToString()).SetActive(false);
            GetUI(GameObjects.Star_Two.ToString()).SetActive(true);
            GetUI(GameObjects.Star_Three.ToString()).SetActive(false);
        }
        else
        {
            GetUI(GameObjects.Star_One.ToString()).SetActive(true);
            GetUI(GameObjects.Star_Two.ToString()).SetActive(false);
            GetUI(GameObjects.Star_Three.ToString()).SetActive(false);
        }

    }
}
