using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crane_Puzzle : BasePuzzle
{
    [Serializable]
    public class CraneCondition
    {
        public Button PosButton;
        public Image PosImage;
        public bool OnDown; //내리기를 눌렀는지
        public bool OnUp; //올리기를 눌렀는지.

        public List<int> UpCondition; //올리는 조건
        public List<int> UpActive; //올릴시에 활성화 되는 오브젝트
        public List<int> UpUnActive; //올릴때 비활성화 되는 오브젝트
        public List<int> DownActive; //내릴때 활성화 되는 오브젝트
        public List<int> DownUnActive; //올릴때 비활성화 되는 오브젝트
    }

    [SerializeField] List<CraneCondition> craneConditions;
    private InGameScene gameScene;
    private UI_Log logUI;
    private int curPos = 0; //cransConditions 의 인덱스.

    private void Awake()
    {
        gameScene = Manager.Scene.GetCurScene() as InGameScene;

        //초기화
        for(int i=0;i<craneConditions.Count;i++)
        {
            int index = i;
            craneConditions[i].PosButton.onClick.AddListener(() => OnClickedPosButton(index));
            craneConditions[i].PosImage.gameObject.SetActive(false);
        }
        craneConditions[0].PosImage.gameObject.SetActive(true);
    }

    private void Start()
    {
        logUI = Manager.Resource.Load<UI_Log>("UI_Log");
    }

    public void OnClickedDownButton()
    {
        CraneCondition crane = craneConditions[curPos];
        if (crane.OnDown)
        {
            Log();
            return;
        }

        for (int i = 0; i < crane.DownActive.Count; i++)
            gameScene.FindInteractObject(crane.DownActive[i]).gameObject.SetActive(true);

        for (int i = 0; i < crane.DownUnActive.Count; i++)
        {
            GameObject gameObject = gameScene.FindInteractObject(crane.DownUnActive[i]).gameObject;
            gameObject.SetActive(false);
            gameObject.GetComponent<Clue_Object>().IsClear = true;
        }

        crane.OnDown = true;
        PuzzleObject.gameObject.SetActive(false);
        SetGameOff();
    }

    public void OnClickedUpButton()
    {
        CraneCondition crane = craneConditions[curPos];
        if (crane.OnUp)
        {
            Log();
            return;
        }

        for(int i=0;i< crane.UpCondition.Count;i++)
        {
            InteractObject inter = gameScene.FindInteractObject(crane.UpCondition[i]);
            if(inter.GetComponent<Clue_Object>().IsClear == false)
            {
                Log();
                return;
            }
        }

        for (int i = 0; i < crane.UpActive.Count; i++)
            gameScene.FindInteractObject(crane.UpActive[i]).gameObject.SetActive(true);

        for (int i = 0; i < crane.UpUnActive.Count; i++)
        {
            GameObject gameObject = gameScene.FindInteractObject(crane.UpUnActive[i]).gameObject;
            gameObject.SetActive(false);
            gameObject.GetComponent<Clue_Object>().IsClear = true;
        }

        crane.OnUp = true;
        PuzzleObject.gameObject.SetActive(false);
        SetGameOff();
    }

    public void OnClickedPosButton(int myPos)
    {
        CraneCondition crane = craneConditions[curPos];
        if(crane.OnUp == false || crane.OnDown == false || curPos + 1 != myPos)
        {
            Log();
            return;
        }

        curPos++;
        crane.PosImage.gameObject.SetActive(false);
        craneConditions[curPos].PosImage.gameObject.SetActive(true);
    }


    public void Log()
    {
        UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
        popup.SetLog(102424); //테이블에 추가 부탁
    }
}
