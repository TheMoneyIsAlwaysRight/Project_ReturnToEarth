using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


/// <summary>
/// 80% 권새롬 작성
/// 챕터버튼을 누르면 동작하는 코드들
/// </summary>
/// 
public class ChapterButtonUI : MonoBehaviour
{
    [SerializeField] GameObject entryUI;

    private bool isEntryActive = false;
    private bool onScrollViewCoroutine = false;
    private Button[] chapterButtons;
    private Scrollbar chapterScrollbar;
    private ScrollRect chpaterScrollRect;
    private float blockValue = 0.7f; //스크롤뷰의 스크롤을 막는 높이 계산.

    private void Awake()
    {
        chapterButtons = GetComponentsInChildren<Button>();
        chapterScrollbar = GetComponentInChildren<Scrollbar>();
        chpaterScrollRect = GetComponentInChildren<ScrollRect>();
        //챕터 버튼에 OnClick 할당
        LayerMask layer = LayerMask.GetMask("ChapterUI");
        for (int i = 0; i < chapterButtons.Length; i++)
        {
            if (Extension.Contain(layer, chapterButtons[i].gameObject.layer) == false) //챕터 버튼 아니면 이 Listener을 넣으면 안됨.
                return;
            int index = i;
            GameObject go = chapterButtons[i].gameObject;
            chapterButtons[i].onClick.AddListener(() => ClickEntry(index, go)); //챕터 정보, 버튼을 전달해줌.
        }
        SetScrollerValue(blockValue);
    }

    private void Update()
    {
        if (chapterScrollbar.value <= blockValue && !isEntryActive && Input.GetMouseButtonUp(0))
            SetScrollerValue(blockValue);
    }

    public void ClickEntry(int chapterIndex, GameObject button)
    {
        Debug.Log("Click"+chapterIndex);
        isEntryActive = isEntryActive ? false : true; // 삼항연산자 사용. isEntryActive가 true면 false로 바꾸고, false면 true로 바꿔준다.
        if (isEntryActive == true)
            OnEntry(chapterIndex, button);
        else
            OffEntry();
        SetActiveButtons(chapterIndex, !isEntryActive);
    }


    private void OnEntry(int chapterIndex, GameObject button)
    {
        entryUI.SetActive(true);
        entryUI.transform.position = button.transform.position;
        entryUI.transform.SetSiblingIndex(chapterIndex);
        float scrollerValue = 0;
        switch (chapterIndex)
        {
            case 4:
                scrollerValue = 1;
                break;
            case 3:
                scrollerValue = 0.85f;
                break;
            case 2:
                scrollerValue = 0.6f;
                break;
            case 1:
                scrollerValue = 0.3f;
                break;
            case 0:
                scrollerValue = 0f;
                break;
            default:
                break;
        }
        SetScrollerValue(scrollerValue);
    }

    
    private void OffEntry()
    {
        entryUI.SetActive(false);
    }

    public void SetScrollerValue(float value)
    {
        if (onScrollViewCoroutine)
            return;
        onScrollViewCoroutine = true;
        StartCoroutine(CoSetScrollerValue(value));
    }

    public void BackEntry() //EntryUI 에서 뒤로가기를 누르면 호출되는 함수.
    {
        isEntryActive = false;
        OffEntry();
        for (int i = 0; i < chapterButtons.Length; i++)
            chapterButtons[i].interactable = true;
    }

    public void GameSceneLoad()
    {
        Manager.Scene.LoadScene("InGameScene");
    }

    public void SetActiveButtons(int onIndex, bool active)
    {
        for (int i = 0; i < chapterButtons.Length; i++)
        {
            if (i == onIndex)
                chapterButtons[i].interactable = true;
            else
                chapterButtons[i].interactable = active;
        }
    }

    IEnumerator CoSetScrollerValue(float value)
    {
        chpaterScrollRect.vertical = false;
        float time = 0;
        float speed = 3f;
        float curValue = chapterScrollbar.value;
        while (true)
        {
            float tmpValue = Mathf.Lerp(curValue, value, time);
            if (tmpValue == value)
                break;
            chapterScrollbar.value = tmpValue;
            time += Time.deltaTime * speed;
            yield return null;
        }
        onScrollViewCoroutine = false;
        chpaterScrollRect.vertical = true;
    }
}
