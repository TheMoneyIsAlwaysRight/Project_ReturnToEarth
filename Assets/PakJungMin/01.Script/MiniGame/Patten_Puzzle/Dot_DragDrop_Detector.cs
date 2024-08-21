
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/***************************************************************************************************
 

    잠금 해제 퍼즐의 각각 정점에 붙은 드래그 앤 드롭 감지기.

    

*****************************************************************************************************/
public class Dot_DragDrop_Detector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Dot dot;
    public UnityAction OnCheck;
    [SerializeField] public int number;

    private void Start()
    {
        dot = GetComponentInParent<Dot>();
    }
    //드래그를 시작했을 때  
    public void OnBeginDrag(PointerEventData eventData)
    {
        dot.dotCoordinator.prevDot = this.dot;
        dot.dotCoordinator.isDrag = true;
        dot.dotCoordinator.DrawLine(this.dot);

        dot.dotCoordinator.visited.Clear();
        dot.dotCoordinator.visited.Add(dot);
        OnCheck?.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        //OnDrag가 없으면 OnEndDrag가 실행되지 않음.
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OnCheck?.Invoke();
        for (int x = 0; x < dot.dotCoordinator.line_List.Count; x++)
        {
            Destroy(dot.dotCoordinator.line_List[x]);
        }
        dot.dotCoordinator.line_List.Clear();
        dot.dotCoordinator.numberList.Clear();
        dot.dotCoordinator.isDrag = false;
    }







    //이녀석은 유니티 에디터 상에서 붙여야함.event trigger 컴포넌트에
    public void OnMouseEnterDot()
    {
        if (dot.dotCoordinator.isDrag && !dot.dotCoordinator.visited.Contains(dot))
        {
            OnCheck?.Invoke();
            //이전 선분의 크기 조절
            dot.dotCoordinator.rect.sizeDelta = new Vector2(dot.dotCoordinator.rect.sizeDelta.x,
                Vector3.Distance(dot.dotCoordinator.prevDot.transform.localPosition,
                dot.transform.localPosition));
            //이전 선분의 회전값 조절
            dot.dotCoordinator.rect.rotation = Quaternion.FromToRotation(Vector3.up,
                (dot.transform.localPosition - dot.dotCoordinator.prevDot.transform.localPosition).normalized);
            //새로운 선분을 긋는 함수 호출
            dot.dotCoordinator.DrawLine(this.dot);
        }
    }
}

