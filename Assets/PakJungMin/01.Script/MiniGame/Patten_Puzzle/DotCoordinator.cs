using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static Define;

public class DotCoordinator : MonoBehaviour
{   
    
    
/***************************************************************************************
                              **** Field ****
                             
 ***************************************************************************************/
    [HideInInspector]
    public Dot prevDot; //드래그 중에 최근에 방문한 점.
    [HideInInspector]
    public bool isDrag; //점끼리 선을 잇는 게 가능한 지 여부.
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public List<Dot> visited; //방문한 점들 리스트, 재방문 불가능.
    [HideInInspector]
    public List<GameObject> line_List; //생성된 선 오브젝트 리스트.
    [HideInInspector]
    public List<int> numberList; //플레이어가 고른 점들의 답안.
    [Header("문제의 정답")]
    public StringBuilder Correct_answer; //문제의 정답.
    [HideInInspector]
    public StringBuilder answer;  //플레이어가 입력하는 답안.

    [Header("선 프리팹")]
    [SerializeField] GameObject line_Prefab;  //선 오브젝트 프리팹
    [Header("퍼즐 패널 안의 실제 퍼즐 역할의 캔버스")]
    [SerializeField] Canvas canvas_pattenPuzzle;
    [SerializeField] Patten_Puzzle puzzle;

    [SerializeField] Canvas main;

/***************************************************************************************
                              **** Method ****
                             
 ***************************************************************************************/
 
    //Method : 선분을 그리는 함수.
    public void DrawLine(Dot dot)
    {
        //새로 방문한 점을 라인 렌더러의 정점으로 추가
        prevDot = dot;
        GameObject line= Instantiate(line_Prefab,canvas_pattenPuzzle.transform);
        visited.Add(dot);
        line_List.Add(line);
        numberList.Add(dot.gameObject.GetComponentInChildren<Dot_DragDrop_Detector>().number);
        line.transform.localPosition = dot.gameObject.transform.localPosition; /// ****재복습 필요,
        rect = line.GetComponent<RectTransform>();
    }
    //Method : 정답 검사 함수.
    public void CheckAnswer()
    {
        if (Correct_answer == null)
            return;
        for(int x=0;x<numberList.Count;x++)
        {
            answer.Append(numberList[x].ToString());
        }

        if (answer.ToString() == Correct_answer.ToString())
        {
            Debug.Log("패턴 풀림");
            puzzle.IsPuzzleClear = true;
        }
        else
        {
            answer.Clear();
        }
    }
/*********************************************************************************************
                            ****Function Flow****
                                                   
**********************************************************************************************/
    private void Awake()
    {
        line_List = new List<GameObject>();
        numberList = new List<int>();
        answer = new StringBuilder();
        Dot_DragDrop_Detector[] a = GetComponentsInChildren<Dot_DragDrop_Detector>();

        for(int x=0;x<a.Length;x++)
        {
            a[x].OnCheck += CheckAnswer;
        }
    }

    private void OnEnable()
    {
        main.renderMode = RenderMode.ScreenSpaceOverlay;

        if (!PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap02_1_Pattern))
            return;

        if (Correct_answer != null)
            return;

        Correct_answer = new StringBuilder();
        List<object> correct_List = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap02_1_Pattern];

        for (int x = 0; x < correct_List.Count; x++)
        {
            Correct_answer.Append(correct_List[x].ToString());
        }
        Debug.Log(Correct_answer);
    }

    private void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = canvas_pattenPuzzle.transform.InverseTransformPoint(Input.mousePosition);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x,
                Vector3.Distance(mousePos, prevDot.transform.localPosition));
            rect.rotation = Quaternion.FromToRotation(Vector3.up,
                (mousePos - prevDot.transform.localPosition).normalized);
        }
    }

    private void OnDisable()
    {
        main.renderMode = RenderMode.ScreenSpaceCamera;
    }

}
