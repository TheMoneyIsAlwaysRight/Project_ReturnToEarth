using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 뒤끝 SDK namespace 추가
using BackEnd;
using Unity.VisualScripting;

public class BackendGameLog
{
    private static BackendGameLog _instance = null;

    public static BackendGameLog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameLog();
            }

            return _instance;
        }
    }

    public void GameLogInsert()
    {
        Param param = new Param();

        param.Add("clearStage", 1);
        param.Add("currentMoney", 100000);

        Debug.Log("게임 로그 삽입을 시도합니다.");

        var bro = Backend.GameLog.InsertLog("ClearStage", param);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("게임 로그 삽입 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("게임 로그 삽입에 성공했습니다. : " + bro);
    }

    /// <summary>
    /// 유저로그 넣는 함수
    /// </summary>    
    public void UserLogInsert(UserLog userLog)
    {
        Param param = new Param();

        param.Add("StageID", userLog.StageID);
        param.Add("Rounds", userLog.Rounds);
        param.Add("isClear", userLog.IsClear);
        param.Add("clearTime", userLog.ClearTime);
        param.Add("touchCount", userLog.AllTouchCount);
        param.Add("InteractTouchCount", userLog.InteractTouchCount);
        param.Add("objectClearSequence", userLog.ObjectClearSequence);
        param.Add("screenChangeCount", userLog.ScreenChangeCount);

        Debug.Log("게임 로그 삽입을 시도합니다.");

        var bro = Backend.GameLog.InsertLog("UserLog", param);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("게임 로그 삽입 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("게임 로그 삽입에 성공했습니다. : " + bro);
    }
}

public class UserLog
{
    public int StageID;     // 스테이지 ID : InGameScene
    public int Rounds;      // 몇 회차인지 : InGameScene
    public bool IsClear;    // 클리어 여부 : GameManager
    public float ClearTime; // 클리어 시간 : GameManager
    public int AllTouchCount;  // 모든 터치 횟수 : InteractController
    public int InteractTouchCount;  // 상호작용 터치 횟수 : InteractController
    public List<int> ObjectClearSequence;   // 오브젝트 클리어 순서 : InteractObject
    public int ScreenChangeCount;   // 화면전환 횟수 : SceneController

    public UserLog()
    {
        ClearData();
    }

    public void ClearData()
    {
        StageID = 0;
        Rounds = 0;
        IsClear = false;
        ClearTime = 0;
        AllTouchCount = 0;
        InteractTouchCount = 0;
        ObjectClearSequence = new List<int>();
        ScreenChangeCount = 0;
    }
}
