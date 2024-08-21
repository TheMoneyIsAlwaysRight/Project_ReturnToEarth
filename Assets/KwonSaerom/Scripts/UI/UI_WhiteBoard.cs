using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_WhiteBoard : PopUpUI
{
    [SerializeField] List<Sprite> numberImages;
    [SerializeField] GameObject NumberToken;
    [SerializeField] Transform numberLayoutTransform;

    protected override void Awake()
    {
        base.Awake();
        SetNumber();
    }

    private void SetNumber()
    {
        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap02_1_Pattern) == false)
            SetInitNumber();
        List<object> answer = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap02_1_Pattern];

        for(int i=0;i<answer.Count;i++)
        {
            GameObject go = Instantiate(NumberToken, numberLayoutTransform);
            go.GetComponent<Image>().sprite = numberImages[(int)answer[i]-1];
        }
    }

    // 퍼즐 정답 넣기
    private void SetInitNumber()
    {
        int randomNum = -1;
        int numberSize = numberImages.Count;
        List<object> selectInfo = new List<object>();

        List<bool> isCheck = new List<bool>();
        for (int i = 0; i < numberSize; i++)
            isCheck.Add(false);

        int patternSize = Random.Range(3, numberSize);
        for (int i = 0; i < patternSize; i++)
        {
            randomNum = Random.Range(1, numberSize);
            while (true)
            {
                if (isCheck[randomNum] == false)
                {
                    isCheck[randomNum] = true;
                    break;
                }
                randomNum = Random.Range(1, numberSize);
            }
            selectInfo.Add(randomNum);
        }

        PuzzleManager.Inst.CorrectAnswers.Add((int)PuzzleAnswerKey.Chap02_1_Pattern, selectInfo);
    }

}
