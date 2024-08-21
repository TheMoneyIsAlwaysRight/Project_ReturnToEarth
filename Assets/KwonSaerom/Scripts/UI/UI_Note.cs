using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Note : PopUpUI
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<string> answers;

    private void Start()
    {
        InitAnswer();
    }

    private void InitAnswer()
    {
        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap05_1_Note) == true)
        {
            //하드코딩..
            List<object> answerTmp = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap05_1_Note];
            if ((int)answerTmp[1] == '4')
                image.sprite = sprites[0];
            else
                image.sprite = sprites[1];
            return;
        }

        int randomNum = Random.Range(0, sprites.Count);
        image.sprite = sprites[randomNum];

        List<object> answer = new List<object>();
        for (int i = 0; i < answers[randomNum].Length; i++)
        {
            answer.Add(answers[randomNum][i]-'0');
        }
        PuzzleManager.Inst.CorrectAnswers.Add((int)PuzzleAnswerKey.Chap05_1_Note, answer);
    }
}
