using UnityEngine;
using System.Collections.Generic;
//각 정점을 서로 이은 뒤, 찌끄러뜨린 형태 --> 사실상 n각형 도형 문제.

//선을 생성하고 Line List에 추가하는 기능.

//연결리스트 처럼 각 정점이 다음 정점의 데이터 등을 알게 한뒤,
//서로 연결하는 선을 긋는다.

//dotList를 순회하며, 어떤 정점에 다음 정점의 데이터를 넣기.
public class LinePuzzle_LogicManager : MonoBehaviour
{
    [HideInInspector]
    public List<LinePuzzle_Dot> dot_list = new List<LinePuzzle_Dot>();

    [SerializeField] Canvas ui_PuzzleCanvas;  //모든 퍼즐의 상위 오브젝트인 동시에 캔버스
    [SerializeField] Canvas in_PuzzleCanvas;  //각각 퍼즐 안에서 실제 퍼즐 역할을 하는 캔버스.
    [SerializeField] Line_Puzzle puzzle;     //각각 퍼즐들의 기반 클래스.
    [SerializeField] GameObject line_Prefab;
    [SerializeField] GameObject LineSet;     //생성된 Line Object들을 모아놓는 집합 역할의 부모 Object
    [SerializeField] GameObject gizmo;
    float minDistance = 0.001f;

    [HideInInspector]
    public LinePuzzle_Dot dragDot;
    [HideInInspector]
    public bool isDrag;

    List<Transform> gizmoList = new List<Transform>();
    List<Transform> lines = new List<Transform>();
    int random;
    Stack<int> visited = new Stack<int>();
    /*
     * 두 직선이 교차하며, 교차점이 게임 상 화면에 있을 조건
     * 0. 두 직선의 기울기를 구한다. 이때 평행일 경우, 교차하지 않으므로 종료한다.
     * 1. 공식으로 교차점을 구한 뒤, 이 교차점이 네 꼭지점 사이 안에 있으면 된다.
     * 
     * 각각 정점을 검사하면서, 정점의 tail,head 정점 과의 직선의 방정식을 생성하고, 검사한다.

    */
    public void CheckAnswer()
    {
        if (gizmoList.Count > 0)
        {
            for (int x = 0; x < gizmoList.Count; x++)
            {
                Destroy(gizmoList[x].gameObject);
            }
            gizmoList.Clear();
        }

        {
            LinePuzzle_Line[] lines = LineSet.GetComponentsInChildren<LinePuzzle_Line>();
            for (int x = 0; x < lines.Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    //평행한 경우 제외
                    if (x == y) { continue; }

                    float curSlope; //이 직선의 기울기
                    float compare_Slope; //검사할 다른 직선의 기울기

           
                    float curIntercept; //직선의 y절편
                    float compare_Intercept;//직선의 y절편
                    float resultX, resultY; //교차점의 좌표

                    //각각 직선의 기울기 구하기 : 각 직선을 이루는 두 점으로부터 기울기 계산.
                    curSlope = (lines[x].startPosY - lines[x].endPosY) /
                        (lines[x].startPosX - lines[x].endPosX);

                    compare_Slope = (lines[y].startPosY - lines[y].endPosY) /
                        (lines[y].startPosX - lines[y].endPosX);


                    if (curSlope == compare_Slope) { Debug.Log("두 직선은 평행하다."); continue; }
                    else
                    {
                        //각각 직선들의 Y절편 구하기 : 
                        curIntercept = lines[x].endPosY - curSlope * lines[x].endPosX;
                        compare_Intercept = lines[y].endPosY - compare_Slope * lines[y].endPosX;

                        //교차점의 좌표
                        resultX = (compare_Intercept - curIntercept) / (curSlope - compare_Slope);
                        resultY = curSlope * resultX + curIntercept;

                        //각각 선분의 최솟값/최대값 안에 존재하는지 여부 확인.

                        //현재 선분과 검사할 다음 선분의 x,y 값의 최소,최대값 안에 점이 있어야 교차점이 존재한다.

                        if ((((resultX > Mathf.Min(lines[x].startPosX, lines[x].endPosX) + minDistance
                            && resultX < Mathf.Max(lines[x].startPosX, lines[x].endPosX) - minDistance

                            && (resultX > Mathf.Min(lines[y].startPosX, lines[y].endPosX) + minDistance
                            && resultX < Mathf.Max(lines[y].startPosX, lines[y].endPosX) - minDistance)


                            && (resultY > Mathf.Min(lines[x].startPosY, lines[x].endPosY) + minDistance
                            && resultY < Mathf.Max(lines[x].startPosY, lines[x].endPosY) - minDistance

                            && (resultY > Mathf.Min(lines[y].startPosY, lines[y].endPosY) + minDistance
                            && resultY < Mathf.Max(lines[y].startPosY, lines[y].endPosY) - minDistance))))))

                        {
                            Vector3 gizmoDot;
                            //교차점에서 기즈모 역할의 Image 오브젝트 생성.
                            RectTransformUtility.ScreenPointToWorldPointInRectangle(ui_PuzzleCanvas.GetComponent<RectTransform>(), new Vector2(resultX, resultY), null, out gizmoDot);
                            GameObject a = Instantiate(gizmo, in_PuzzleCanvas.transform);
                            a.transform.parent = in_PuzzleCanvas.transform;
                            a.transform.localPosition = gizmoDot;
                            gizmoList.Add(a.transform);
                        }
                    }
                }
            }
            if (gizmoList.Count <= 0)
            {
                puzzle.IsPuzzleClear = true;
            }
        }
    }

    //Method : 점들을 무작위로 이어서 퍼즐을 만드는 함수.
    public void SetLevel()
    {
        /*  
            어떤 정점의 다음 정점이 자기 자신을 가리키는 경우 제외.
            어떤 두 정점의 다음 정점이 서로를 가리키는 경우 제외.

            ->BackTracking

            0.자기 자신을 제외한 다른 정점을 고른다.
            1.다음 차례의 정점은 자기자신 및 전에 선택한 정점이 고른 선택지를 고를 수 없다.
            2.만약 고를 수 있는 선택지가 없다면, 이전 선택지로 돌아가서 다른 선택지를 고른다.
        */

        //이전의 교차점들을 나타내는 기즈모들이 있다면 모두 제거.
        if (gizmoList.Count > 0)
        {
            for (int x = 0; x < gizmoList.Count; x++)
            {
                Destroy(gizmoList[x].gameObject);
            }
            gizmoList.Clear();
        }
        //모든 정점들의 다음 정점 필드를 제거.
        for (int x = 0; x < dot_list.Count; x++)
        {
            dot_list[x].tail = null;
        }
        //이전의 생성된 선분들이 있다면 모두 제거.
        for (int x = 0; x < lines.Count; x++)
        {
            Destroy(lines[x].gameObject);
        }
        //선분 리스트 초기화.
        lines.Clear();

        while (dot_list.Count >= visited.Count)
        {

            int random = UnityEngine.Random.Range(0, dot_list.Count);
            int index = 0;
            bool canRoot = false;

            //정점 리스트를 순회하며, 무작위로 선택된 정점이 어떤 정점의 다음 필드가 될 수 있는지 확인.
            for (int x = 0; x < dot_list.Count; x++)
            {
                index = x;
                // 다음 정점이 자기 자신이 아닐 것, 방문한 적이 없을 것.
                if (x != random && !visited.Contains(random) && dot_list[x].tail == null)
                {
                    canRoot = true;
                    break;
                }
                else
                {
                    canRoot = false;
                }
            }
            //현재 시점에서 이동이 가능한 경우
            if (canRoot)
            {
                if (dot_list[random].tail != dot_list[index])
                {
                    dot_list[index].tail = dot_list[random];
                    visited.Push(random);
                }
            }
            else
            {
                foreach (LinePuzzle_Dot dot in dot_list)
                {
                    dot.tail = null;
                }
                visited.Clear();
            }
            if (dot_list.Count == visited.Count)
            {
                DrawLine();
                break;
            }
        }
    }
    public void DrawLine()
    {
        Vector3 dotPos;
        Vector3 tailPos;

        foreach (LinePuzzle_Dot dot in dot_list)
        {
            dotPos = dot.gameObject.transform.localPosition;
            tailPos = dot.tail.gameObject.transform.localPosition;

            GameObject lineObject = Instantiate(line_Prefab, in_PuzzleCanvas.transform);
            lineObject.transform.localPosition = dotPos;
            lineObject.transform.parent = LineSet.transform;

            LinePuzzle_Line line = lineObject.GetComponent<LinePuzzle_Line>();
            dot.tailLine = lineObject;
            lines.Add(lineObject.transform);

            line.startPosX = dotPos.x;
            line.startPosY = dotPos.y;

            line.endPosX = tailPos.x;
            line.endPosY = tailPos.y;

            RectTransform rect = lineObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, Vector3.Distance(dotPos, tailPos));
            rect.rotation = Quaternion.FromToRotation(Vector3.up, (tailPos - dotPos).normalized);


        }
        for (int x = 0; x < dot_list.Count; x++)
        {
            SetHead(dot_list[x]);
        }
    }
    private void Update()
    {
        if (isDrag)
        {
            //실시간 마우스의 위치에 따른 점 위치 갱신
            Vector3 mousePos = in_PuzzleCanvas.transform.InverseTransformPoint(Input.mousePosition);
            dragDot.transform.localPosition = mousePos;


            LinePuzzle_Line tailLine = dragDot.tailLine.GetComponent<LinePuzzle_Line>();
            LinePuzzle_Line headLine = dragDot.headLine.GetComponent<LinePuzzle_Line>();

            //드래그 중인 정점의 다음 선분의 위치 <- 드래그 중인 정점의 다음 정점의 로컬 좌표.
            dragDot.tailLine.transform.localPosition = dragDot.tail.transform.localPosition;

            //다음 선분의 시작점의 위치 <- 드래그 중인 정점의 로컬 좌표
            tailLine.startPosX = dragDot.transform.localPosition.x;
            tailLine.startPosY = dragDot.transform.localPosition.y;

            //점과 다음 점까지의 거리
            float nextDistance = Vector3.Distance(dragDot.transform.localPosition,
            dragDot.GetComponent<LinePuzzle_Dot>().tail.transform.localPosition);


            //드래그 중인 정점의 위치에 따른 다음 선분의 크기,회전값 조정
            RectTransform rect = dragDot.tailLine.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, nextDistance);

            rect.rotation = Quaternion.FromToRotation(Vector3.up,
            (dragDot.transform.localPosition - dragDot.GetComponent<LinePuzzle_Dot>().tail.transform.localPosition).normalized);

            //드래그 중인 정점의 이전 선분의 위치 <- 드래그 중인 정점의 이전 정점의 로컬 좌표.
            dragDot.headLine.transform.localPosition = dragDot.head.transform.localPosition;

            //이전 선분의 끝점의 위치 <- 다음 선분의 시작점의 위치.
            headLine.endPosX = tailLine.startPosX;
            headLine.endPosY = tailLine.startPosY;

            //점과 이전 점까지의 거리
            float prevDistance = Vector3.Distance(dragDot.transform.localPosition,
            dragDot.GetComponent<LinePuzzle_Dot>().head.transform.localPosition);

            //드래그 중인 정점의 위치에 따른 이전 선분의 크기,회전값 조정
            RectTransform rects = dragDot.headLine.GetComponent<RectTransform>();
            rects.sizeDelta = new Vector2(rects.sizeDelta.x,prevDistance);
            rects.rotation = Quaternion.FromToRotation(Vector3.up,
            (dragDot.gameObject.transform.localPosition - dragDot.GetComponent<LinePuzzle_Dot>().head.gameObject.transform.localPosition).normalized);
        }
    }
    public void SetHead(LinePuzzle_Dot dot)
    {
        for (int x = 0; x < dot_list.Count; x++)
        {
            if (dot_list[x].tail == dot)
            {
                dot.head = dot_list[x];
                dot.headLine = dot_list[x].tailLine;
                break;
            }
        }
    }
    /***********************************************************************************
                                   Logic Flow
    ************************************************************************************/

    private void Start()
    {
        SetLevel();
    }
    private void OnEnable()
    {
        ui_PuzzleCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
    private void OnDisable()
    {
        ui_PuzzleCanvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

}
