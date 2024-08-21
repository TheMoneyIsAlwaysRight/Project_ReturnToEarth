using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class Frequency_LogicManager : MonoBehaviour
{
    public Frequency_Puzzle puzzle;
    public Frequency_Switch[] switchList; //스위치들이 인덱스에 정확하게 매칭시키기 위해 배열 사용.

    List<bool> solution = new List<bool>();


    private void Awake()
    {
        switchList = new Frequency_Switch[10];
        InitPuzzleAnswer();
    }

    private void OnEnable()
    {
        InitPuzzleAnswer();
    }

    // 권새롬 추가 -----> 퍼즐 답이 정해졌을때 Init
    private void InitPuzzleAnswer()
    {
        if (solution.Count > 0)
            return;

        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap03_2_Power) == false)
            return;

        List<object> objectList = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap03_2_Power];

        for (int x = 0; x < objectList.Count; x++)
        {
            solution.Add((bool)objectList[x]);
        }
    }
    //------------------------

    public void CheckAnswer()
    {
        //권새롬 추가 --> 예외처리.
        if (solution.Count == 0)
            return;
        //------------------------

        for (int x = 0; x < switchList.Length; x++)
        {
            if (switchList[x].value != solution[x])
            {
                Debug.Log("정답이 아닙니다.");
                return;
            }
        }
        puzzle.IsPuzzleClear = true;

        //권새롬 추가 --> 오브젝트 사라지고 생기는게 있음
        InGameScene gameScene = Manager.Scene.GetCurScene() as InGameScene;
        gameScene.FindInteractObject(107133).GetComponent<ConditionUnActiveInteract>().Count += 1;
    }
}
