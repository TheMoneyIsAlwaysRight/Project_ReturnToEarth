using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Class : 선 잇기 퍼즐에서 각각 칸의 좌표역할을 하는 정점 클래스
public class DotLink_DotDectector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    DotLink_Dot dot;
    public UnityAction OnCheck;
    [SerializeField] public int number;

    private void Start()
    {
        dot = GetComponentInParent<DotLink_Dot>();
    }

    //Method : 드래그 시작 시 자동으로 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.dot.CurState == DotLink_Dot.State.Blank) { return; } //비어 있는 점은 드래그 차단
        if (!this.dot.startDot) { return; }

        dot.logicManager.curDot = this.dot;
        dot.logicManager.isDrag = true;

        switch (dot.CurState)
        {
            case DotLink_Dot.State.Red:
                ClearList(dot.logicManager.redList);
                dot.logicManager.redList.Add(this.dot);
                break;
            case DotLink_Dot.State.Blue:
                ClearList(dot.logicManager.blueList);
                dot.logicManager.blueList.Add(this.dot);
                break;
            case DotLink_Dot.State.Yellow:
                ClearList(dot.logicManager.yellowList);
                dot.logicManager.yellowList.Add(this.dot);
                break;
            case DotLink_Dot.State.Green:
                ClearList(dot.logicManager.greenList);
                dot.logicManager.greenList.Add(this.dot);
                break;
        }

        dot.logicManager.DrawLine(this.dot);
        dot.logicManager.visited.Clear();
        dot.logicManager.visited.Add(dot);
        OnCheck?.Invoke();
    }

    //Method : 드래그 중일 때. 이 함수 없으면 EndDrag가 호출 안됨.
    public void OnDrag(PointerEventData eventData)
    {

    }

    //Method : 드래그 종료 시 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        OnCheck?.Invoke();
        dot.logicManager.isDrag = false;
        dot.logicManager.curDot = null;

    }
    //Method : 드래그 중, 다른 정점을 만났을 때, 그 정점을 기준으로 다시 선을 그리는 함수.
    //-----> 각 정점들의 이벤트 트리거 컴포넌트의 이벤트에 등록 
    public void OnMouseEnterDot()
    {

        if (!dot.logicManager.isDrag) { return; } //드래그 중이 아닐 경우 차단.

        DotLink_Dot curDot = dot.logicManager.curDot;

        //드래그 중인 색깔과 만난 정점의 색이 다르면, 만난 정점의 색깔 선 연결을 전부 해제.
        if(this.dot.CurState != curDot.CurState)
        {
            switch (this.dot.CurState)
            {
                case DotLink_Dot.State.Red:
                    ClearList(dot.logicManager.redList);
                    break;
                case DotLink_Dot.State.Blue:
                    ClearList(dot.logicManager.blueList);
                    break;
                case DotLink_Dot.State.Yellow:
                    ClearList(dot.logicManager.yellowList);
                    break;
                case DotLink_Dot.State.Green:
                    ClearList(dot.logicManager.greenList);
                    break;
            }
        }

        //색깔이 다른 양 끝점에 접근 차단.
        if (this.dot.startDot && dot.CurState != curDot.CurState)
        {
            return;
        }

        //대각선 이동 차단.
        if (curDot != null && (curDot.posX != this.dot.posX) && (curDot.posY != this.dot.posY)) { return; }

        // 드래그 중이며, 방문한 적이 없는 점일 경우.
        if (!dot.logicManager.visited.Contains(dot))
        {
            this.dot.ChangeState(curDot.CurState); // 이 점의 상태 -> 이전 점의 상태 

            //현재 선의 크기와 기울기를 이전 정점과 만난 정점과의 거리 및 기울기로 설정.
            dot.logicManager.curRect.sizeDelta = new Vector2(dot.logicManager.curRect.sizeDelta.x, Vector3.Distance(dot.logicManager.curDot.transform.localPosition, dot.transform.localPosition));
            dot.logicManager.curRect.rotation = Quaternion.FromToRotation(Vector3.up, (dot.transform.localPosition - dot.logicManager.curDot.transform.localPosition).normalized);

            // 방문했음을 기록.
            dot.logicManager.visited.Add(dot);

            // 현재 드래그 중인 정점을 이 점으로 변경하고, 전의 정점을 기록.
            curDot.nextDot = this.dot;
            this.dot.prevDot = curDot;
            dot.logicManager.curDot = this.dot; 

            // 현재 정점의 색깔에 따라 색 연결 리스트에 추가.
            if (curDot.nextDot != null)
            {
                switch (dot.CurState)
                {
                    case DotLink_Dot.State.Red:
                        if (!dot.logicManager.redList.Contains(this.dot))
                        {
                            dot.logicManager.redList.Add(this.dot);
                        }
                        break;
                    case DotLink_Dot.State.Blue:
                        if (!dot.logicManager.blueList.Contains(this.dot))
                        { dot.logicManager.blueList.Add(this.dot); }
                        break;
                    case DotLink_Dot.State.Yellow:
                        if (!dot.logicManager.yellowList.Contains(this.dot))
                        { dot.logicManager.yellowList.Add(this.dot); }
                        break;
                    case DotLink_Dot.State.Green:
                        if (!dot.logicManager.greenList.Contains(this.dot))
                        {
                            dot.logicManager.greenList.Add(this.dot);
                        }
                        break;
                }
            }

            // 이 정점의 색깔에 맞는 선 생성.
            dot.logicManager.DrawLine(this.dot);

        }

    }
    public void ClearList(List<DotLink_Dot> list)
    {

        for (int x = 0; x < list.Count; x++)
        {
            list[x].prevDot = null;
            list[x].nextDot = null;
            if (!list[x].startDot)
            {
                list[x].ChangeState(DotLink_Dot.State.Blank);
            }

            if (list[x].ownLine != null)
            {
                Destroy(list[x].ownLine.gameObject);
            }
            list[x].ownLine = null;
        }
        list.Clear();
    }
}