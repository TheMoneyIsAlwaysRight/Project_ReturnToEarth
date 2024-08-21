using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class UI_ChapterButton : MonoBehaviour
{
    [SerializeField] UI_Script scriptUI;

    [SerializeField] Button button;

    [SerializeField] int stageID;

    [SerializeField] UI_Notice notice;

    [SerializeField] ChapterManager chapterManager;

    private bool isLoading = false; //정민 추가---> 로딩 중엔 버튼 클릭해도 예외처리.

    public Button Button { get { return button; } }
    public int StageID { get { return stageID; } set { stageID = value; } }
    public bool IsLoading { get { return isLoading; } set { isLoading = value; } }

    public void MoveScene()
    {
        if (isLoading)
            return;
        Manager.Game.CurStageKey = stageID; // = ChapterManager.Instance.CheckClear(stageID); // 이걸로 수정 예정.
        isLoading = true;                   //정민 추가---> 로딩 중엔 버튼 클릭해도 예외처리.


        // 권새롬 수정 --> 미니게임 확장 Define에서 하면 다른 클래스에서도 자동으로 적용되게 로직 수정
        int key = Manager.Chapter.CheckClear(); // 정민 추가 : 미니게임씬은 미니게임 씬으로 이동하고 아니면 방탈출 씬으로 보내버린다
        if (key == 0)
        {
            Notice();
            isLoading = false;
            return;
        }

        if (Define.MiniGames.ContainsKey(key))
            Manager.Scene.LoadScene(Define.MiniGames[key]);
        else
            Manager.Scene.LoadScene("InGameScene");

        //-------------------------------------------------

        #region 과거 로직
        //switch (Manager.Chapter.CheckClear())   // 미니게임씬은 미니게임 씬으로 이동하고 아니면 방탈출 씬으로 보내버린다
        //{                                   // 정민 추가 : 조건문의 stageID -> Manager.Game.CurStageKey로 수정 예정
        //    case Define.JUMP_GAME_SCENE_KEY:
        //        Manager.Scene.LoadScene("JumpGameScene");
        //        break;
        //    case Define.UPDOWN_GAME_SCENE_KEY:
        //        Manager.Scene.LoadScene("UpDownGameScene");
        //        break;
        //    case Define.PUSH_GAME_SCENE_KEY:
        //        Manager.Scene.LoadScene("PushPushGameScene");
        //        break;
        //    case 0:
        //        Notice();
        //        isLoading = false;
        //        break;
        //    default:
        //        Manager.Scene.LoadScene("InGameScene");
        //        break;
        //}
        #endregion
    }

    //권새롬 추가 --> 스토리버튼 할당
    public void OnClickedStory()
    {
        if (!Manager.Chapter.openStage.Contains(stageID+1))
        {
            Notice();
            return;
        }
        UI_Script ui_script = Manager.UI.ShowPopUpUI(scriptUI);
        ui_script.CharacterScripts.Execute(stageID, NewCharacterScripts.BackUI.None);
    }
    //-------------------------------

    public void Notice()
    {
        UI_Notice tmp = Manager.UI.ShowPopUpUI(notice);
        if (Manager.Game.LanguageSet == Define.LanguageSet.EN)
        {
            tmp.NoticeText = "Notice";
            tmp.DescriptionText = "It's the undeveloped stage.";
        }
        else
        {
            tmp.NoticeText = "알림";
            tmp.DescriptionText = "잠겨있습니다.";
        }
        tmp.Setting();
    }

    public void RoundCheck()
    {
        BackendGameData.Instance.ChangeRoundStage(stageID);
    }

    //-------------------------------------------------------------->정민 추가                         
    private void OnDisable()
    {
        isLoading = false;      //------> 재로드 시 isLoading 초기화.
    }
    //-------------------------------------------------------------->
}
