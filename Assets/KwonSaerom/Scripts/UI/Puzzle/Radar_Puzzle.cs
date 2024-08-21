using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Puzzle : BasePuzzle
{
    [SerializeField] string answer;
    [SerializeField] List<PayLoadSlot> slots;

    public void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        bool isComplete = true;

        for (int i = 0; i < answer.Length; i++)
        {
            if (slots[i].CurAnswer != answer[i])
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        { 
            IsPuzzleClear = true;

            ///권새롬 추가 --> 오브젝트 사라지고 생기는게 있음
            InGameScene gameScene = Manager.Scene.GetCurScene() as InGameScene;
            gameScene.FindInteractObject(107133).GetComponent<ConditionUnActiveInteract>().Count += 1;
        }

    }
}
