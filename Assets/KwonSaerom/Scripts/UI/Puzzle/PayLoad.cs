using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PayLoad : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Transform canvas;
    [SerializeField] char answer;

    private Vector3 previousPosition;     
    private RectTransform rect;
    private Image image;

    public char Answer { get { return answer; } }

    private Vector2 offset;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //이전 Pos 저장
        previousPosition = rect.localPosition;

        // UI 요소와 마우스 포인터 사이의 상대적 위치를 계산.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform, eventData.position, eventData.pressEventCamera, out offset);

        PayLoadSlot slot = transform.parent.GetComponent<PayLoadSlot>();
        if(slot != null)
        {
            slot.CurAnswer = ' ';
        }

        // 현재 드래그중인 UI가 화면의 최상단에 출력되도록 하기 위해
        transform.SetParent(canvas);        // 부모 오브젝트를 Canvas로 설정
        transform.SetAsLastSibling();       // 가장 앞에 보이도록 마지막 자식으로 설정

        image.raycastTarget = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform.parent, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            rect.localPosition = localPointerPosition - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }

    public void SetPreviousPosition()
    {
        rect.localPosition = previousPosition;
    }
}
