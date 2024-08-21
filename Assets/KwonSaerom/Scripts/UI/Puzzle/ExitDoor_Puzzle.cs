using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor_Puzzle : BasePuzzle
{
    [SerializeField] string answerString;
    [HideInInspector]
    public List<UpDownToken> Tokens = new List<UpDownToken>();
    private List<int> answer = new List<int>();

    private void Awake()
    {
        if (answerString.Equals("NOTE_PUZZLE"))
            InitNotePuzzle();
        else
        {
            for (int i = 0; i < answerString.Length; i++)
                answer.Add(answerString[i] - 'L');
        }
    }

    private void OnEnable()
    {
        InitNotePuzzle();
    }

    public void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        if (answer.Count == 0)
            return;

        bool isComplete = true;

        for (int i = 0; i < answer.Count; i++)
        {
            if (Tokens[i].CurInput != answer[i])
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
            IsPuzzleClear = true;

    }

    private void InitNotePuzzle()
    {
        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)Define.PuzzleAnswerKey.Chap05_1_Note) == false)
            return;

        if (answer.Count > 0)
            return;

        List<object> puzzleAnswer = PuzzleManager.Inst.CorrectAnswers[(int)Define.PuzzleAnswerKey.Chap05_1_Note];
        for(int i=0;i< puzzleAnswer.Count;i++)
        {
            answer.Add((int)puzzleAnswer[i]);
        }
    }
}
