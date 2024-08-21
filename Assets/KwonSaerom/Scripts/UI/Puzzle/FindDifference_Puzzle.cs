using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindDifference_Puzzle : BasePuzzle
{
    [SerializeField] List<DiffToken> tokens;

    private const int RANDOM_COUNT = 5; //몇개 틀린그림 찾을건지
    private List<DiffToken> answers = new List<DiffToken>();
    private List<bool> check = new List<bool>(); //겹치는거
    private int count = 0;

    private void Awake()
    {
        for(int i=0;i< tokens.Count;i++)
        {
            tokens[i].gameObject.SetActive(false);
            tokens[i].CurPuzzle = this;
            check.Add(false);
        }

        while (count < RANDOM_COUNT)
        {
            int randomNum = Random.Range(0, tokens.Count);
            while(true)
            {
                if (check[randomNum] ==false)
                {
                    answers.Add(tokens[randomNum]);
                    tokens[randomNum].gameObject.SetActive(true);
                    check[randomNum] = true;
                    count++;
                    break;
                }
                randomNum = Random.Range(0, tokens.Count);
            }
        }
    }
    public void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        bool isComplete = true;

        for (int i = 0; i < answers.Count; i++)
        {
            if (answers[i].AnswerCheck == false)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
            IsPuzzleClear = true;
    }
}
