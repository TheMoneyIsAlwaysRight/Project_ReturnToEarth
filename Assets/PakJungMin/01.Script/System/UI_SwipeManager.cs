using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SwipeManager : MonoBehaviour
{
    [SerializeField] UI_SwipeController[] swipes;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] UI_FloorButton[] floorBtns;
    [SerializeField] int number; //테스트용

    Coroutine trans_NextStage;

    //Coroutine : 0.5초 뒤 다음 스테이지 선택화면 띄우는 함수
    IEnumerator TransNextStage()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeStageSellect(Manager.Chapter.prevStage + 1);
    }

    //Method : ChapterScene에서 특정 스테이지 선택화면 활성화 함수
    public void ChangeStageSellect(int stageNumber) 
    {
        for(int x=0;x<swipes.Length;x++)
        {
            for(int y=0;y<swipes[x].Stages.Length;y++)
            {
                if (swipes[x].Stages[y].SceneID == stageNumber)
                {
                    floorBtns[x].ViewChapter();

                    switch (y)
                    {
                        case 0:
                            swipes[x].Scroll_Pos = 0;
                            break;
                        case 1:
                            swipes[x].Scroll_Pos = 0.5f;
                            break;
                        case 2:
                            swipes[x].Scroll_Pos = 1;
                            break;
                    }
                    return;
                }
            }
        }
    }
    //*******************************************************************************
    //                                Logic Flow
    //*******************************************************************************
    private void Start()
    {
        swipes = GetComponentsInChildren<UI_SwipeController>();
    }

    private void OnEnable()
    {
        if (Manager.Chapter.prevStage != 0)
        {
            trans_NextStage = StartCoroutine(TransNextStage());
        }
    }
}
