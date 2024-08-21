using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 제작 : 찬규 
/// 각 층을 클릭하면 스테이지를 보이게 해준다
/// </summary>
public class UI_FloorButton : MonoBehaviour
{
    [SerializeField] Sprite open;       // 선택한 상태일 때 스프라이트
    [SerializeField] Sprite close;      // 비선택 상태일 때 스프라이트
    [SerializeField] ScrollRect scrollRect;  // 스크롤렉트
    [SerializeField] RectTransform content;  // 각 층의 스테이지 스크롤

    [SerializeField] UI_FloorButton[] floors;           // 각 층 버튼의 배열

    [SerializeField] bool isOpen;    // 열려있는 상태면 true, 아니면 false
    [SerializeField] bool isSellect; // 선택 된 상태면 true

    public Sprite Open { get { return open; } }
    public Sprite Close { get { return close; } }
    public RectTransform Content { get { return content; } }

    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    public bool IsSellect { get { return isSellect; } set { isSellect = value; } }

    private void Start()
    {
        if (isOpen)         // 열려있으면 스테이지 스크롤을 켜주고 닫혀있으면 꺼준다
        {
            Content.gameObject.SetActive(true);
            GetComponent<Image>().sprite = Open;
        }
        else
        {
            Content.gameObject.SetActive(false);
            GetComponent<Image>().sprite = Close;
        }
    }

    /// <summary>
    /// 현재 선택한 챕터를 보여주는 함수
    /// </summary>
    public void ViewChapter()
    {
        if (isOpen)         // 클릭한 문이 열려있으면 리턴
            return;

        isSellect = true;   // 선택 됨

        foreach (UI_FloorButton i in floors)
        {
            if (i.isSellect)    // 선택 된 버튼 이외에는 전부 false로 바꿈
                i.IsOpen = true;
            else
                i.IsOpen = false;

            if (i.isOpen)         // 열려있으면 스테이지 스크롤을 켜주고 닫혀있으면 꺼준다
            {
                i.Content.gameObject.SetActive(true);
                i.GetComponent<Image>().sprite = i.Open;
                scrollRect.content = i.Content;
            }
            else
            {
                i.Content.gameObject.SetActive(false);
                i.GetComponent<Image>().sprite = i.Close;
            }

            i.IsSellect = false;    // 불리언값을 false로 바꿔준다
        }
    }
}
