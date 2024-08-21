using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Class : 돋보기 힌트 지시기에 붙는 클래스
public class HintIndicator : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        HintManager.Inst.is_Hint_Activated = false;
        gameObject.SetActive(false);
    }
}