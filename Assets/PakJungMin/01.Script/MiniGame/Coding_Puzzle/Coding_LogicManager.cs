using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coding_LogicManager : MonoBehaviour
{

    [SerializeField] Coding_Puzzle puzzle;
    [SerializeField] List<Coding_Letter.State> correct;
    public Coding_SpriteData spriteData;

    public Coding_Letter[] letterList = new Coding_Letter[6];

    public Coding_Letter curLetter;

    public void CheckAnswer()
    {
        for(int x=0;x<correct.Count;x++)
        {
            if (letterList[x].CurState != correct[x])
            {
                Debug.Log($"현재 문자 : {letterList[x]}가 정답 {correct[x]}와 다르다");
                return;
            }
        }
        puzzle.IsPuzzleClear = true;
    }

    public void SwapLetter(Coding_Letter letter)
    {
        if (letter != curLetter)
        {
            Coding_Letter.State prevState = letter.CurState;
            Coding_Letter.State newState = curLetter.CurState;

            letter.ChangeState(newState);

            curLetter.ChangeState(prevState);

            curLetter = null;
            CheckAnswer();
        }
    }
}
