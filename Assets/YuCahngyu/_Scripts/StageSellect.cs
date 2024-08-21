using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StageSellect : BaseUI
{
    enum Texts
    {
        DescriptionText
    }

    [SerializeField] UI_ChapterButton enterButton;    // 스테이지를 진입하는 버튼
    [SerializeField] int sceneID;       // 스테이지 ID
    [SerializeField] StageSellect[] stages;       // 해당 챕터의 스테이지 3개

    //정민 추가 -------------------------------------------------------------------------------
    [SerializeField] GameObject starSet;                    // 별 Img 게임 오브젝트 배열 Set
    public List<ScoreStar> starList;                        // 별 Img 게임 오브젝트 배열   
    public StageLocker Img_Locker;                          // 자물쇠 Img 게임 오브젝트.
    int stageScore;                                         // 현재 스테이지의 별 개수. 
    //여기까지 -------------------------------------------------------------------------------


    bool isClicked;     // 지금 눌렀는지 체크
    bool isSellect;     // 선택 되었는지 체크

    /*UI_ChapterButton 스크립트로 이동
    private bool isLoading = false; //정민 추가---> 로딩 중엔 버튼 클릭해도 예외처리.
    */

    private const int START_FRONT_TEXT = 102466;
    public UI_ChapterButton EnterButton { get { return enterButton; } }
    public int SceneID { get { return sceneID; } set { sceneID = value; } }
    public bool IsClicked { get { return isClicked; } set { isClicked = value; } }
    public bool IsSellect { get { return isSellect; } set { isSellect = value; } }

    //정민 추가--------------------------------------------------------------------------------------------------------------------

    //Method : 오브젝트 활성화시, 데이터 새로고침 및 스테이지 점수,별(점수) 활성화.
    private void OnEnable()
    {
        Setting();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        LocalUpdate();
        Setting();
    }


    //권새롬 추가 --> 스테이지별 설명란 추가하기위해
    public override void LocalUpdate()
    {
        int key = START_FRONT_TEXT + (sceneID - Define.START_CHAPTER) * 2;

        //예외추가 --> 클리어된 ID여야만 스크립트가 열려야하고, 1-1은 기본적으로 보임.
        if (BackendGameData.Instance.IsClearStage(SceneID) == true || SceneID == Define.START_CHAPTER)
            GetUI<TMP_Text>(Texts.DescriptionText.ToString()).text = LanguageSetting.GetLocaleText(key);
        else
            GetUI<TMP_Text>(Texts.DescriptionText.ToString()).text = "";

    }

    // 유찬규 추가 -> OnEnable에 있는 기능을 함수로 묶기
    public void Setting()
    {
        //데이터를 새로고침
        ChapterManager.Instance.RefreshData();

        //선택된 챕터의 스테이지의 별들을 모두 비활성화.
        for (int x = 0; x < starList.Count; x++)
        {
            starList[x].gameObject.SetActive(false);
        }

        //현재 ID값에 해당하는 이 스테이지 점수 반환 및 별 활성화.
        if (ChapterManager.Instance.Local[this.sceneID - 108001] > 0)
        {
            stageScore = ChapterManager.Instance.Local[this.sceneID - 108001];
            //점수만큼 별들을 재활성화
            for (int x = 0; x < stageScore; x++)
            {
                starList[x].gameObject.SetActive(true);
            }
        }

        // 스테이지 해금 여부에 따른 자물쇠 이미지 활성화/비활성화
        if (!ChapterManager.Instance.openStage.Contains(this.sceneID))
        {
            this.Img_Locker.gameObject.SetActive(true);
        }
        else
        {
            this.Img_Locker.gameObject.SetActive(false);
        }
    }

    #region 이동된 기능
    /* 이하 기능이 이동되고 필요없어짐
     * */

    /* UI_ChapterButton 스크립트로 이동
     
    public void MoveScene()
    {
        if (isLoading)
            return;

        Manager.Game.CurStageKey = sceneID;
        isLoading = true;   //정민 추가---> 로딩 중엔 버튼 클릭해도 예외처리.

        switch (sceneID)        // 미니게임씬은 미니게임 씬으로 이동하고 아니면 방탈출 씬으로 보내버린다
        {
            case Define.JUMP_GAME_SCENE_KEY:
                Manager.Scene.LoadScene("JumpGameScene");
                break;
            default:
                Manager.Scene.LoadScene("InGameScene");
                break;
        }
    }

    //-------------------------------------------------------------->정민 추가                         
    private void OnDisable()
    {
        isLoading = false;      //------> 재로드 시 isLoading 초기화.
    }
    //-------------------------------------------------------------->
    */

    // 스테이지를 선택했을 때 나오는 함수
    //public void SellectStage()
    //{
    //    isClicked = true;

    //    foreach (StageSellect i in stages)
    //    {
    //        if (i.IsClicked)
    //            i.IsSellect = true;
    //        else
    //            i.IsSellect = false;

    //        if (!i.IsSellect)
    //            continue;

    //        i.EnterButton.StageID = i.SceneID;
    //        i.EnterButton.Button.interactable = true;
    //    }

    //    isClicked = false;
    //}
    #endregion
}

