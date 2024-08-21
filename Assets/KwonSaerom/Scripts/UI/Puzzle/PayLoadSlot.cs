using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PayLoadSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Radar_Puzzle puzzle;
    public char CurAnswer { get; set; }

    private Image image;
    private RectTransform rect;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }


    public void OnDrop(PointerEventData eventData)
    {
        PayLoad payLoad = eventData.pointerDrag.GetComponent<PayLoad>();
        // pointerDrag는 현재 드래그하고 있는 대상(=아이템)
        if (payLoad != null)
        {
            int count = GetComponentsInChildren<PayLoad>().Length;
            if(count > 0)
            {
                payLoad.SetPreviousPosition(); //원래 자리로 돌아가기(이미 슬롯이 차있다)
                return;
            }
            // 드래그하고 있는 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트 위치와 동일하게 설정
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

            CurAnswer = payLoad.Answer;
            puzzle.CheckPuzzleState();
        }

    }
}
