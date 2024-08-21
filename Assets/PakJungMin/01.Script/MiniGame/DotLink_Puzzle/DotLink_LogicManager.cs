using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DotLink_LogicManager : MonoBehaviour
{



    /***************************************************************************************
                                  **** Field ****
     ***************************************************************************************/
    [HideInInspector]
    public DotLink_Dot curDot;          //드래그 중에 최근에 방문한 점.
    [HideInInspector]
    public DotLink_Line curLine;        //현재 선
    [HideInInspector]
    public bool isDrag;                 //현재 드래그 중 여부
    [HideInInspector]
    public RectTransform curRect;           
    [HideInInspector]
    public List<DotLink_Dot> visited;   //방문한 점들 리스트, 재방문 불가능.

    public DotLink_ColorData colorData; //선 색깔 데이터

    [Header("문제의 정답")]
    public List<int> correct = new List<int>();

    public GameObject dotSet;           // 점들의 집합 역할 상위 오브젝트
    public GameObject lineSet;          // 선들의 집합 역할 상위 오브젝트

    [Header("알파벳 퍼즐")]
    [SerializeField] DotLink_Puzzle puzzle;
    [Header("선 프리팹")]
    [SerializeField] GameObject line_Prefab;  //선 오브젝트 프리팹
    [Header("UI_Puzzle Canvas")]
    [SerializeField] Canvas uipuzzleCanvas; //UI_Puzzle 프리팹 캔버스
    [Header("InPuzzleCanvas")]
    [SerializeField] Canvas inpuzzleCanvas; //실제 퍼즐 역할 캔버스

    public Dictionary<string, DotLink_Dot> dotDic = new Dictionary<string, DotLink_Dot>(); //정점 딕셔너리
    public DotLink_LevelGenerator levelGenerator; // 레벨 제작기.

    public List<DotLink_Dot> redList = new List<DotLink_Dot>();
    public List<DotLink_Dot> blueList = new List<DotLink_Dot>();
    public List<DotLink_Dot> greenList = new List<DotLink_Dot>();
    public List<DotLink_Dot> yellowList = new List<DotLink_Dot>();

    /***************************************************************************************
                                  **** Method ****
     ***************************************************************************************/

    //Method : 드래그 중인 색깔의 선분을 생성하는 함수.
    public void DrawLine(DotLink_Dot dot)
    {
        //선분 오브젝트 생성 및 트랜스폼 값을 inpuzzleCanvas의 트랜스폼으로 설정.
        GameObject line = Instantiate(line_Prefab, inpuzzleCanvas.transform);

        //이 선분 오브젝트을 현재 선으로 설정.
        curLine = line.GetComponent<DotLink_Line>();
        dot.ownLine = curLine;

        //이 선분 오브젝트의 부모를 선 집합으로 설정.
        line.transform.parent = lineSet.transform;
        line.gameObject.name = dot.gameObject.name;

        //이 선분의 색깔을 드래그를 시작했던 정점의 색으로 변경.
        switch (curDot.CurState)
        {
            case DotLink_Dot.State.Blue:
                line.GetComponent<Image>().color = colorData.blueColor;
                break;
            case DotLink_Dot.State.Red:
                line.GetComponent<Image>().color = colorData.redColor;
                break;
            case DotLink_Dot.State.Yellow:
                line.GetComponent<Image>().color = colorData.yellowColor;
                break;
            case DotLink_Dot.State.Green:
                line.GetComponent<Image>().color = colorData.greenColor;
                break;

            default:
                return;
        }
        //이 선분의 로컬 좌표를, 좌표 역할의 투명한 정점의 로컬 위치로 할당.
        line.transform.localPosition = dot.gameObject.transform.localPosition;

        //현재 Rect를 이 선분 오브젝트의 Rect로 설정.
        curRect = line.GetComponent<RectTransform>();
    }

    //Method : 각 색깔별 정점들이 겹치지 않고 연결됨 여부 검사 후 게임 클리어 판단 함수
    public void CheckAnswer()
    {
        //각각 선들의 최소 길이를 넘지 않으면 제외.
        if(redList.Count < 2 || blueList.Count < 2 || greenList.Count < 2 || yellowList.Count < 2) { return; }

        //색깔별 선들을 모아놓은 리스트의 첫번째 요소와 마지막 요소가 시작 정점이라면, 클리어한 것으로 판정.
        if ((redList[0].startDot && redList[redList.Count - 1].startDot) &&
        (blueList[0].startDot && blueList[blueList.Count - 1].startDot) &&
        (greenList[0].startDot && greenList[greenList.Count - 1].startDot) &&
        (yellowList[0].startDot && yellowList[yellowList.Count - 1].startDot))
        {
            puzzle.IsPuzzleClear = true;
        }
    }
    /*********************************************************************************************
                                ****Unity Flow****
    **********************************************************************************************/
    private void Awake()
    {
        uipuzzleCanvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        DotLink_Dot[] array = dotSet.GetComponentsInChildren<DotLink_Dot>();

        int x = 0;
        int y = 4;
        foreach (DotLink_Dot dot in array)
        {
            dot.posX = x;
            dot.posY = y;
            x++;
            dot.gameObject.name = $"{dot.posX},{dot.posY}";
            dotDic.Add(dot.gameObject.name, dot);
            dot.GetComponentInChildren<DotLink_DotDectector>().OnCheck += CheckAnswer;
            if (x > 4)
            {
                y--;
                x = 0;
            }
        }
        levelGenerator.Generate();
    }
}
