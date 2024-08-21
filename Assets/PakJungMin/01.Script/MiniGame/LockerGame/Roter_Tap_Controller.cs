using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Roter_Tap_Controller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RoterData roterData;

    float middlePos;       //픽셀 기준. 
    float curRoterPos;     //픽셀 기준. 
    float prevRoterPos;    //픽셀 기준. 

    Vector2 RectLocalPixelPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        prevRoterPos = GetComponent<RectTransform>().anchoredPosition.y; //기존의 위치 저장.
    }
    //레버를 조작중(드래그 중)
    public void OnDrag(PointerEventData eventData)
    {
        // 화면 상의 마우스 오브젝트 좌표를, 화면 상의 픽셀 좌표로 변환.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out RectLocalPixelPos);

        curRoterPos = RectLocalPixelPos.y;


    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(curRoterPos - prevRoterPos) <= 100f)
        {
            return;
        }

        if (prevRoterPos - curRoterPos < 0) // 아래에서 위로 회전자 드래그
        {
            roterData.CurNumber++;

        }
        else if (prevRoterPos - curRoterPos >= 0) // 위에서 아래로 회전자 드래그
        {
            roterData.CurNumber--;
        }

    }




}
