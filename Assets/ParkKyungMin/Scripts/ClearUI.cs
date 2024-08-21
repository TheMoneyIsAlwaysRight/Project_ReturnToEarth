using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClearUI : PopUpUI
{
    enum Texts
    {
        DescriptionText,
        EscapeTime
    }

    enum GameObjects
    {
        StoryImage
    }

    [SerializeField] GameObject[] stars; // 별 오브젝트 배열

    private int lastImageKey;
    private bool isNext = false;
    private void Start()
    {
        int uiNum = Manager.Data.StageLoad(Manager.Game.CurStageKey).FrontStory;
        int[] imageKeys = Manager.Data.UILoad(uiNum).Img;
        lastImageKey = imageKeys[imageKeys.Length - 1];

        GetUI<Image>(GameObjects.StoryImage.ToString()).sprite = Manager.Resource.Load<Sprite>(lastImageKey.ToString());
        GetUI<TMP_Text>(Texts.EscapeTime.ToString()).text = $"Escape Time : {Manager.Game.Timer.MaxTimer - Manager.Game.Timer.CurTimer}";
        LocalUpdate();
    }

    public override void LocalUpdate()
    {
        int key = Define.START_CLEAR_STORY_TEXT_KEY + (Manager.Game.CurStageKey - Define.START_CHAPTER) * 2;
        GetUI<TMP_Text>(Texts.DescriptionText.ToString()).text = LanguageSetting.GetLocaleText(key);
    }

    public void DisplayStars(int starCount)
    {
        // 배열을 돌면서 설정된 갯수만큼 별을 활성화
        for (int i = 0; i < stars.Length; i++)
        {
            // starCount보다 작으면 활성화, 그렇지 않으면 비활성화
            stars[i].SetActive(i < starCount);
        }
    }


    public void GoStage()
    {
        Manager.Scene.LoadScene("ChapterScene");
    }

    public void RePlay()
    {
        MoveScene(Manager.Game.CurStageKey);
    }

    public void NextStage()
    {
        if (isNext == true)
            return;
        isNext = true;
        int key = ++Manager.Game.CurStageKey;
        MoveScene(key);
    }

   private void MoveScene(int key)
    {
        if(Define.MiniGames.ContainsKey(key))
            Manager.Scene.LoadScene(Define.MiniGames[key]);
        else
            Manager.Scene.LoadScene("InGameScene");
    }

}
