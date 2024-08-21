using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpenInteract : CameraInteractController
{
    bool isEnter = false;
    Clue_Object clue;

    protected override void Awake()
    {
        base.Awake();
        clue = GetComponent<Clue_Object>();
    }
    protected override void Start()
    {
        base.Start();
    }

    public override void PuzzleOnInteract()
    {
        if(isEnter == false)
        {
            UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
            if(info.interactText == 0)
                popup.SetLog(102170); //하드코딩. -> 코드 수정 요망
            else
                popup.SetLog(info.interactText); //하드코딩. -> 코드 수정 요망
            return;
        }
        Debug.Log("클리어!");
        clue.IsClear = true;
    }

    public void Open()
    {
        Debug.Log("Open");
        GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        isEnter = true;
        //나가기 버튼 활성화
    }
}
