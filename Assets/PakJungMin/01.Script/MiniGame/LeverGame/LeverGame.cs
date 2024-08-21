using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverGame : BasePuzzle
{
    public enum LeverState
    {
        Up,
        Middle,
        Down
    }

    // 정민추가 --> 정답 레버 스테이트
    [SerializeField] LeverState left;        // 0,1,2
    [SerializeField] LeverState middle;        // 0,1,2
    [SerializeField] LeverState right;        // 0,1,2
    
    [SerializeField] Lever Left_Lever;
    [SerializeField] Lever Middle_Lever;
    [SerializeField] Lever Right_Lever;

    private bool isClear = false;
    private LeverInteractPuzzle leverPuzzleObject;

    private void Update()
    {
        if (!isClear && Input.GetMouseButtonUp(0))
        {
            CheckLeverState();
        }
    }

    public void CheckLeverState()
    {
        if(Left_Lever.CurState == left && Middle_Lever.CurState == middle && Right_Lever.CurState == right)
        {
            SetLeverObject(true);
            isClear = true;
            IsPuzzleClear = true;
            return;
        }
    }

    public void ExitPuzzle()
    {
        SetLeverObject();
        SetGameOff();
    }

    private void SetLeverObject(bool isClear = false)
    {
        if (leverPuzzleObject == null)
            leverPuzzleObject = PuzzleObject as LeverInteractPuzzle;
        if(leverPuzzleObject != null)
            leverPuzzleObject.SetLeverSprite(Left_Lever.CurState, Middle_Lever.CurState, Right_Lever.CurState, isClear);
    }

}
