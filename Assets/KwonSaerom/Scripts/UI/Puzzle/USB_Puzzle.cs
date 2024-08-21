using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class USB_Puzzle : BasePuzzle
{
    [SerializeField] List<Button> buttons;
    [SerializeField] TMP_Text inputText;

    private string password = ""; //정답 비밀번호 -> 맨 앞이 0이면 음수. (string += 안하려고 이런 로직을 짬)
    private StringBuilder inputPassword = new StringBuilder();
    private bool inputZero = false;

    private void Awake()
    {
        inputText.text = "";
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnClickNumber(index));
        }
    }

    private void OnEnable()
    {
        FindAnswer();
    }

    private void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        if (password == "")
            return;

        if (password.Equals(inputPassword.ToString()))
        {
            IsPuzzleClear = true;
        }

    }

    public void OnClickNumber(int index)
    {
        if (index == buttons.Count - 1)
        {
            if (inputPassword.Length == 0)
                return;
            inputPassword.Remove(inputPassword.Length - 1, 1);
        }
        else
            inputPassword.Append(index);

        inputText.text = inputPassword.ToString();

        //퍼즐이 맞는지 검사
        CheckPuzzleState();
    }

    private void FindAnswer()
    {
        if (password != "")
            return;

        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap02_1_USB) == false)
            return;

        List<object> answer = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap02_1_USB];
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < answer.Count; i++)
        {
            sb.Append(answer[i]);
        }

        password = sb.ToString();
        Debug.Log(password);
    }
}
