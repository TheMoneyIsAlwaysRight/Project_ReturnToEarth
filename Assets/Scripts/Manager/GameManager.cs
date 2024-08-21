using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static Define;
using static UnityEngine.Rendering.VolumeComponent;

public class GameManager : Singleton<GameManager>
{
    public CameraController Camera { get; set; }
    public Inventory Inven { get; set; } // 권새롬추가 : 게임 인벤토리 할당(언제든지 접근)
    public InteractController Inter { get; set; } // 권새롬추가 : 인터렉트 컨트롤러(터치 컨트롤러) 할당
    public int CurStageKey { get; set; } = 108011; //임의로 챕터 2-1 키를 넣었음.
    public TimerSystem Timer { set { timer = value; } get { return timer; } }
    public UserLog InGameUserLog { get; set; } = new UserLog(); //유저로그

    public LanguageSet LanguageSet
    {
        get
        {
            return languageSet;
        }
        set
        {
            languageSet = value;
            OnLocalChanged?.Invoke();
        }
    }  // 찬규 : 언어설정 // 새롬수정 : event 추가

    private LanguageSet languageSet;
    private TimerSystem timer;
    public UnityAction OnLocalChanged;

    protected override void Awake()
    {
        base.Awake();

        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            LanguageSet = LanguageSet.KR;
        }
        else if (Application.systemLanguage == SystemLanguage.French)
        {
            LanguageSet = LanguageSet.FR;
        }
        else if (Application.systemLanguage == SystemLanguage.Spanish)
        {
            LanguageSet = LanguageSet.SP;
        }
        else
        {
            LanguageSet = LanguageSet.EN;
        }
    }

    public void Gameover() //게임 오버
    {
        GameObject gameover = Manager.Resource.Load<GameObject>("UI_Gameover");
        PopUpUI pop = gameover.GetComponent<PopUpUI>();
        Manager.UI.ShowPopUpUI(pop);

        // 권새롬 추가 --> 클리어 못한 로그 저장
        SaveGameLog(false);
        Manager.Resource.Clear(); 
        //ClearGame();
    }

    public void Clear()
    {
        UI_Script ui_script = Manager.UI.ShowPopUpUI(Manager.Scene.GetCurScene().GetComponent<InGameScene>().ScriptUI);     // 찬규 추가 : 클리어창 전에 스토리ui 띄워주기
        //ChapterManager.Instance.ClearLevel(CurStageKey); //정민 추가 ---> 클리어 여부 챕터 매니저에 기록용
        ui_script.CharacterScripts.Execute(false);
        Manager.Resource.Clear();
    }

    public void ShowClearUI()
    {
        ClearUI clearUIPrefab = Manager.Resource.Load<GameObject>("UI_Clear").GetComponent<ClearUI>();
        ClearUI clear = Manager.UI.ShowPopUpUI(clearUIPrefab);
        
        /* 정민 추가 : 별 개수를 챕터 매니저의 데이터에 기록하기 위해 따로 변수로 빼서 할당.
        --------------------------------------------------------------------------------------*/
        int starScore = timer.StarCalculate();
        Manager.Chapter.RecordData(starScore); 
        //---------------------------------------------------------------------------------------
        
        clear.DisplayStars(starScore);
        //ClearGame();

        //권새롬 추가 --> 클리어 로그 저장
        SaveGameLog(true);
    }

    //권새롬 추가 --> 로그 저장 함수
    public void SaveGameLog(bool isClear)
    {
        if(InGameUserLog == null)
        {
            Debug.Log($"InGameUserLog is {InGameUserLog}");
            return;
        }

        InGameUserLog.IsClear = isClear;
        if(isClear == true)
            InGameUserLog.ClearTime = timer.MaxTimer - timer.CurTimer;
        BackendManager.Instance.UserLogInsert(InGameUserLog);
        InGameUserLog.ClearData();
    }

    #region 중간저장
    /*
    //권새롬 추가 --> 강제 종료되거나, 앱이 종료되었을때 호출되는 콜백함수. 
    public void OnApplicationQuit()
    {
        Debug.Log("강제종료됨");
        SaveGame();
    }

    //권새롬 추가 --> 게임 중간저장기능
    public void SaveGame()
    {
        //인게임씬이 아니면 저장할것이 없음.
        InGameScene gameScene = Manager.Scene.GetCurScene() as InGameScene;
        if (gameScene == null)
            return;

        //0. 기존 테이블 불러오기.
        int stageProcessID = Manager.Data.StageTables[CurStageKey].StageProcessID;
        int inventoryID = Manager.Data.StageTables[CurStageKey].InventoryID;
        StageProcessTable stageProcess = Manager.Data.StageProcessTables[stageProcessID];
        InventoryTable inventory = Manager.Data.InventoryTables[inventoryID];


        //1. 인벤토리 저장
        List<int> itemIDList = new List<int>();
        List<Item> itemList = Inven.ItemList;
        for(int i=0;i< itemList.Count;i++)
        {
            itemIDList.Add(itemList[i].ItemData.ID);
        }
        inventory.slot = itemIDList.ToArray();

        //2. 스테이지 상황 저장
        List<int> objectIDList = new List<int>();
        List<bool> objectIsClears = new List<bool>();
        List<InteractObject> objects = gameScene.InteractObjects;
        for(int i=0;i<objects.Count;i++)
        {
            objectIDList.Add(objects[i].InteractID);
            Clue_Object clue = objects[i].GetComponent<Clue_Object>();
            if(clue != null)
                objectIsClears.Add(objects[i].GetComponent<Clue_Object>().IsClear);
            else
                objectIsClears.Add(false);
        }

        //3. 타이머 저장, 테이블에 데이터 저장.
        stageProcess.Timer = timer.CurTimer;
        stageProcess.ObjectID = objectIDList.ToArray();
        stageProcess.IsClear = objectIsClears.ToArray();


        //4. 데이터 로컬에 Save
        Manager.Data.SaveTable("InventoryTable");
        Manager.Data.SaveTable("StageProcessTable");
    }
    
    //권새롬 추가 --> 게임 클리어시, 게임오버시 데이터 초기화 하는 함수
    public void ClearGame()
    {
        InGameScene gameScene = Manager.Scene.GetCurScene() as InGameScene;
        if (gameScene == null)
            return;

        //0. 기존 테이블 불러오기.
        int stageProcessID = Manager.Data.StageTables[CurStageKey].StageProcessID;
        int inventoryID = Manager.Data.StageTables[CurStageKey].InventoryID;
        StageProcessTable stageProcess = Manager.Data.StageProcessTables[stageProcessID];
        InventoryTable inventory = Manager.Data.InventoryTables[inventoryID];


        //1. 인벤토리 초기화
        Inven.ItemList.Clear();
        List<int> itemIDList = new List<int>();
        List<Item> itemList = Inven.ItemList;
        for (int i = 0; i < itemList.Count; i++)
        {
            itemIDList.Add(itemList[i].ItemData.ID);
        }
        inventory.slot = itemIDList.ToArray();


        //2. 스테이지 진행상황 초기화
        List<int> objectIDList = new List<int>();
        List<bool> objectIsClears = new List<bool>();
        stageProcess.ObjectID = objectIDList.ToArray();
        stageProcess.IsClear = objectIsClears.ToArray();


        //3. 타이머 초기화
        timer.TimerInit();
        stageProcess.Timer = timer.CurTimer;


        //4. 저장 
        Manager.Data.SaveTable("InventoryTable");
        Manager.Data.SaveTable("StageProcessTable");
    }
    */
    #endregion

}

