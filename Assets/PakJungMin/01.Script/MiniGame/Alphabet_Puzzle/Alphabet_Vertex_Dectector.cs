using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Alphabet_Vertex_Dectector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

/***************************************************************************************************
 

    잠금 해제 퍼즐의 각각 정점에 붙은 드래그 앤 드롭 감지기.

    

*****************************************************************************************************/
    Alphabet_Vertax vertext;

    public UnityAction OnCheck;

    [SerializeField] public int number;

    private void Start()
    {
        vertext = GetComponentInParent<Alphabet_Vertax>();
    }
    //드래그를 시작했을 때  
    public void OnBeginDrag(PointerEventData eventData)
    {
        vertext.logicManager.prevDot = this.vertext;
        vertext.logicManager.isDrag = true;
        vertext.logicManager.DrawLine(this.vertext);

        vertext.logicManager.visited.Clear();
        vertext.logicManager.visited.Add(vertext);
        OnCheck?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //OnDrag가 없으면 OnEndDrag가 실행되지 않음.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnCheck?.Invoke();
        for (int x = 0; x < vertext.logicManager.line_List.Count; x++)
        {
            Destroy(vertext.logicManager.line_List[x]);
        }
        vertext.logicManager.line_List.Clear();
        vertext.logicManager.numberList.Clear();
        vertext.logicManager.isDrag = false;
    }
    public void OnMouseEnterDot()
    {
        if (vertext.logicManager.isDrag && !vertext.logicManager.visited.Contains(vertext))
        {
            OnCheck?.Invoke();
            vertext.logicManager.rect.sizeDelta = new Vector2(vertext.logicManager.rect.sizeDelta.x, Vector3.Distance(vertext.logicManager.prevDot.transform.localPosition, vertext.transform.localPosition));
            vertext.logicManager.rect.rotation = Quaternion.FromToRotation(Vector3.up, (vertext.transform.localPosition - vertext.logicManager.prevDot.transform.localPosition).normalized);
           
            vertext.logicManager.DrawLine(this.vertext);
        }
    }
}
