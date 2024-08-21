using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[Serializable]
public class ScriptBundle
{
    public bool isImage;    // 이미지면 true, 영상이면 false
    public int imageID;
    public int videoID;
    public int[] textID;

    public ScriptBundle(bool isImage, int imageID, int videoID, int textBundleID)
    {
        this.isImage = isImage;
        this.imageID = imageID;
        this.videoID = videoID;
        textID = Manager.Data.TextBundleLoad(textBundleID).TEXT;
    }
}

public class NewCharacterScripts : MonoBehaviour
{
    //권새롬 추가 --> 스토리가 끝난 뒤에 나오는 UI
    public enum BackUI
    {
        ClearUI, // 게임씬일때 클리어 대사 출력되고 Clear UI 나옴 
        StoryUI, // 스토리 팝업에서 스토리 다 끝나고 나오는 UI
        MiniGameUI, // 미니게임 스토리
        None //나오는거 없음
    }
    //-----------------------------------------------

    [SerializeField] TMP_Text script;           // 텍스트 출력
    [SerializeField] TMP_Text stageName;        // 스테이지 이름
    [SerializeField] Image image;               // 텍스트 위에 뜰 이미지
    [SerializeField] Queue<ScriptBundle> scriptBundle;  // 이미지와 텍스트번들을 들고있는 스크립트번들
    [SerializeField] TimerSystem timer;     // 타이머 시스템
    [SerializeField] Video video;   // 영상 재생 시스템

    [SerializeField] List<int> textIDs;  // 텍스트번들의 텍스트테이블 id값
    [SerializeField] Queue<string> texts;     // 실제 재생 될 텍스트

    [SerializeField] float textDelay;           // 출력 할 대사 속도
    [SerializeField] bool check;              // true == "Front" or false == "Back"

    public UI_StoryNextUI StoryUI { get; set; }

    bool isNext;                                // 다음 대사로 넘어가도 되는지 체크
    bool isClick;                               // 연속 클릭 방지
    BackUI backUI = BackUI.ClearUI;

    private void Start()
    {
        timer = Manager.Game.Timer;
    }

    public void Execute(bool check, BackUI MiniGameUI = BackUI.ClearUI)
    {
        backUI = MiniGameUI;

        int chapterKey = Manager.Game.CurStageKey;
        stageName.text = Manager.Data.StageLoad(chapterKey).Name;   // 해당 스테이지 이름 표기해주고

        LocalUpdate();

        scriptBundle.Clear();
        textIDs.Clear();
        texts.Clear();

        this.check = check;

        PullScriptBundle(chapterKey);     // 스크립트번들 리스트에 각 ui테이블의 번들id와 이미지id를 넣은 상태

        video.Release(); // 권새롬 추가 : 영상 재생하기전에 릴리즈 시킨다.
        PrintScript();
    }

    //권새롬 추가 --> 본래의 Execute 오버로딩. 스토리 UI 용
    public void Execute(int chapterKey, BackUI backUI)
    {
        this.backUI = backUI;
        StoryUI.ChapterKey = chapterKey;

        // chapterKey = chapterKey == -1 ? Manager.Game.CurStageKey : chapterKey;
        stageName.text = Manager.Data.StageLoad(chapterKey).Name;

        LocalUpdate();

        scriptBundle.Clear();
        textIDs.Clear();
        texts.Clear();

        check = true;
        PullScriptBundle(chapterKey);

        check = false;
        PullScriptBundle(chapterKey);

        video.Release(); // 권새롬 추가 : 영상 재생하기전에 릴리즈 시킨다.
        PrintScript();
    }
    //--------------------------------------------------

    public void LocalUpdate()
    {
        scriptBundle = new Queue<ScriptBundle>();
        textIDs = new List<int>();
        texts = new Queue<string>();
    }

    /// <summary>
    /// 스크립트 번들 리스트를 만드는 함수
    /// </summary>
    public void PullScriptBundle(int stageNum)
    {
        //int stageNum = Manager.Scene.GetCurScene().ID;  // 스테이지테이블을 불러오기 위한 id값
        int uiNum;                                      // 스테이지 테이블에서 뽑아낼 ui테이블의 id값

        if (check == true)      // 입장스토리일 경우
        {
            uiNum = Manager.Data.StageLoad(stageNum).FrontStory;

            while (true)
            {
                bool isImage;
                int cutID;
                if (Manager.Data.UILoad(uiNum).Img.Length != 0)   // img가 있는 경우
                {
                    isImage = true;
                    cutID = Manager.Data.UILoad(uiNum).Img[0];
                }
                else     // img 대신 video가 있는 경우
                {
                    isImage = false;
                    cutID = Manager.Data.UILoad(uiNum).Video;
                }
                int textBundleID = Manager.Data.UILoad(uiNum).Text;

                if (isImage)
                {
                    scriptBundle.Enqueue(new ScriptBundle(isImage, cutID, 0, textBundleID));
                }
                else
                {
                    scriptBundle.Enqueue(new ScriptBundle(!isImage, 0, cutID, textBundleID));
                }

                if (Manager.Data.UILoad(uiNum).UI == null)
                    break;
                uiNum = Manager.Data.UILoad(uiNum).UI[0];
            }
        }
        else                    // 클리어스토리일 경우
        {
            uiNum = Manager.Data.StageLoad(stageNum).BackStory;

            while (true)
            {
                int cutID;
                int textBundleID = Manager.Data.UILoad(uiNum).Text;

                if (Manager.Data.UILoad(uiNum).Img != null && Manager.Data.UILoad(uiNum).Img.Length != 0)   // img가 있는 경우
                {
                    cutID = Manager.Data.UILoad(uiNum).Img[0];
                    scriptBundle.Enqueue(new ScriptBundle(true, cutID, 0, textBundleID));
                }
                else     // img 대신 video가 있는 경우
                {
                    cutID = Manager.Data.UILoad(uiNum).Video;
                    scriptBundle.Enqueue(new ScriptBundle(false, 0, cutID, textBundleID));
                }

                if (Manager.Data.UILoad(uiNum).UI == null || Manager.Data.UILoad(uiNum).UI[0] == Define.CLEAR_UI_NUM)
                    break;
                uiNum = Manager.Data.UILoad(uiNum).UI[0];
            }
        }
    }

    public void ScriptBundleLoad(int scriptBundleNum)
    {
        if (scriptBundleNum == 0)
            return;

        int[] textsID = Manager.Data.TextBundleLoad(scriptBundleNum).TEXT;

        for (int i = 0; i < textsID.Length; i++)
        {
            this.texts.Enqueue(TextLoad(textsID[i]));
        }
    }

    /// <summary>
    /// 스크립트번들에 있는 텍스트번들에서 텍스트를 뽑아내는 함수
    /// </summary>
    public string TextLoad(int ID)
    {
        return LanguageSetting.GetLocaleText(ID);
    }

    Coroutine tmpRoutine;
    /// <summary>
    /// 스크립트 출력하는 함수
    /// </summary>
    public void PrintScript()
    {
        // 꺼낼 번들도 없고 대사도 없다면
        if (scriptBundle.Count == 0 && texts.Count == 0)
        {
            CloseScripts();
            return;
        }

        // 텍스트는 없지만 번들이 남아있다면
        if (texts.Count == 0 && scriptBundle.Count > 0)
        {
            // 번들 하나를 큐에서 꺼낸다 
            ScriptBundle bundle = scriptBundle.Dequeue();
            video.VideoPlayer.source = VideoSource.VideoClip;

            // 재생 시킬 때 해당 번들에 있는 이미지를 로딩해서 ui에 넣는다        
            if (bundle.isImage)         // 이미지일때
            {
                image.gameObject.SetActive(true);
                video.gameObject.SetActive(false);
                image.sprite = Manager.Resource.Load<Sprite>($"{bundle.imageID}");
                StoryUI.LastImgKey = bundle.imageID; //권새롬 추가 --> 스토리UI에 넣기위한 imageKey 저장
                video.VideoPlayer.clip = null;
            }
            else                        // 동영상일때
            {
                video.gameObject.SetActive(true);
                image.gameObject.SetActive(false);
                video.VideoPlayer.clip = Manager.Resource.Load<VideoClip>($"{bundle.videoID}");

                // 재생시킬때 한번 재생이 되었으면
                // 나머지 텍스트들이 나올 때 까지는 반복재생? 아니면 그냥 멈추기?
                // 그냥 멈추기로 하기로 했으니 불리언 체크가 필요함
                video.VideoPlayer.Play();

                image.sprite = null;
            }

            // 해당 번들에 있는 대사를 리스트에 넣는다
            for (int i = 0; i < bundle.textID.Length; i++)
            {
                texts.Enqueue(TextLoad(bundle.textID[i]));
            }
        }

        // 클릭체크 등등 하면서 
        if (isClick == false)
        {
            script.text = "";
            if (isNext == false)
            {
                isClick = true;
                tmpRoutine = StartCoroutine(Print());
                isClick = false;
            }
            else
            {
                isClick = true;
                isNext = false;
                StopCoroutine(tmpRoutine);
                script.text = texts.Dequeue();
                isClick = false;
            }
        }
    }

    IEnumerator Print()
    {
        isNext = true;
        for (int i = 0; i < texts.Peek().Length; i++)
        {
            string t_letter = texts.Peek()[i].ToString();
            script.text += t_letter;

            yield return new WaitForSecondsRealtime(textDelay);
        }
        script.text = texts.Dequeue();
        isNext = false;
    }

    public void CloseScripts()
    {
        switch (backUI)
        {
            case BackUI.ClearUI:
                if (check == true)
                {
                    Debug.Log("프린트 스크립트에서 대사창 닫음");

                    timer.CurTimer = timer.MaxTimer;
                    if (Manager.Scene.GetCurScene().ID == Define.START_CHAPTER)   // 1-1이면 튜토리얼을 켜야함
                        Manager.Tutorial.PullUpTutorial();
                    else
                        Manager.UI.ClosePopUpUI();
                }
                else if (backUI == BackUI.ClearUI)  // check == false 
                {
                    Manager.Game.ShowClearUI();
                }
                break;
            case BackUI.StoryUI:
                if (check == false)
                {
                    StoryUI.gameObject.SetActive(true);
                }
                break;
            case BackUI.None:
                if (check == false)
                    Manager.UI.ClosePopUpUI();
                break;
            case BackUI.MiniGameUI:
                MinigameStory.Instance.AudioStart();
                Manager.UI.ClosePopUpUI();
                break;
        }
    }
}
