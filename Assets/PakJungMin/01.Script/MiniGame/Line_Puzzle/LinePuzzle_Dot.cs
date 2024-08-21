using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LinePuzzle_Dot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] LinePuzzle_LogicManager LogicManager;
    UnityAction OncheckAnswer;

    [HideInInspector]
    public GameObject tailLine; //다음 선분
    [HideInInspector]
    public GameObject headLine; //이전 선분
    [HideInInspector]
    public LinePuzzle_Dot tail; //다음 정점.
    [HideInInspector]
    public LinePuzzle_Dot head; //이전 정점

    public void OnBeginDrag(PointerEventData eventData)
    {
        //잡고 있는 공의 색 변경? 메소드 정도.
        LogicManager.dragDot = this;
        LogicManager.isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LogicManager.CheckAnswer();
        OncheckAnswer?.Invoke();
        LogicManager.dragDot = null;
        LogicManager.isDrag = false;
    }

    public void Awake()
    {
        LogicManager.dot_list.Add(this);
    }

}
