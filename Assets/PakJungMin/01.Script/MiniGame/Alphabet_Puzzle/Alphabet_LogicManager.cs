using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static Define;

public class Alphabet_LogicManager : MonoBehaviour
{



    /***************************************************************************************
                                  **** Field ****

     ***************************************************************************************/
    [HideInInspector]
    public Alphabet_Vertax prevDot; //드래그 중에 최근에 방문한 점.
    [HideInInspector]
    public bool isDrag; //점끼리 선을 잇는 게 가능한 지 여부.
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public List<Alphabet_Vertax> visited; //방문한 점들 리스트, 재방문 불가능.
    [HideInInspector]
    public List<GameObject> line_List; //생성된 선 오브젝트 리스트.

    public List<int> numberList; //플레이어가 고른 점들의 답안.
    [Header("문제의 정답")]
    //public StringBuilder Correct_answer; //문제의 정답.

    public List<int> correct = new List<int>();

    //-----------임시
    public GameObject vertexs;
    //--------------



    [Header("알파벳 퍼즐")]
    [SerializeField] Alphabet_Puzzle puzzle;
    [Header("선 프리팹")]
    [SerializeField] GameObject line_Prefab;  //선 오브젝트 프리팹
    [Header("UI_Puzzle Canvas")]
    [SerializeField] Canvas uipuzzleCanvas;
    [Header("InPuzzleCanvas")]
    [SerializeField] Canvas inpuzzleCanvas;

    /***************************************************************************************
                                  **** Method ****

     ***************************************************************************************/

    //List<int> ClearStage = {3,3,2,0,0,0,0,0,0....}????
    public void DrawLine(Alphabet_Vertax vertax)
    {
        //새로 방문한 점을 라인 렌더러의 정점으로 추가
        prevDot = vertax;
        GameObject line = Instantiate(line_Prefab, inpuzzleCanvas.transform);
        visited.Add(vertax);
        line_List.Add(line);
        numberList.Add(vertax.number);
        line.transform.localPosition = vertax.gameObject.transform.localPosition; /// ****재복습 필요,
        rect = line.GetComponent<RectTransform>();
    }

    public void CheckAnswer()
    {
        if (numberList.Count == correct.Count)
        {
            for (int x = 0; x < numberList.Count; x++)
            {
                if (numberList[x] != correct[x])
                {
                    Debug.Log("틀렸습니다");
                    return;
                }
            }
            puzzle.IsPuzzleClear = true;
        }
    }
    /*********************************************************************************************
                                ****Function Flow****

    **********************************************************************************************/
    private void Awake()
    {
        line_List = new List<GameObject>();
        numberList = new List<int>();
        Alphabet_Vertax[] array = vertexs.GetComponentsInChildren<Alphabet_Vertax>();

        for (int x = 0; x < array.Length; x++)
        {
            array[x].GetComponentInChildren<Alphabet_Vertex_Dectector>().OnCheck += CheckAnswer;
            if(array[x].GetComponentInChildren<Alphabet_Vertex_Dectector>().OnCheck != null)
            {
                Debug.Log("이벤트 함수 등록됨...");
            }
            else
            {
                Debug.Log("오류!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
    }
    private void Start()
    {
        Alphabet_Vertax[] array = vertexs.GetComponentsInChildren<Alphabet_Vertax>();

        for(int x=0;x<array.Length;x++)
        {
            array[x].number = x;
        }

    }
    private void OnEnable()
    {
        uipuzzleCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        if (!PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap02_1_Pattern))
            return;

        List<object> correct_List = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap02_1_Pattern];
    }

    private void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = inpuzzleCanvas.transform.InverseTransformPoint(Input.mousePosition);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, Vector3.Distance(mousePos, prevDot.transform.localPosition));
            rect.rotation = Quaternion.FromToRotation(Vector3.up, (mousePos - prevDot.transform.localPosition).normalized);
        }
    }

    private void OnDisable()
    {
        uipuzzleCanvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

}
