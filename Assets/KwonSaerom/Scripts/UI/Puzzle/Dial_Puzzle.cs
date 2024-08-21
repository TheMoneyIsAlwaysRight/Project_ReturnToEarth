using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Dial_Puzzle : BasePuzzle
{
    // Electonic_Puzzle 클래스 복사함
    [Serializable]
    public class ImagesContainer
    {
        public int Num;
        public int CurImageIndex = 0;
        public List<Sprite> images;

        public void InCreaseIndex()
        {
            if (CurImageIndex >= images.Count - 1)
                CurImageIndex = 0;
            else
                CurImageIndex++;
        }
    }

    [SerializeField] TMP_Text debug;
    [SerializeField] List<ImagesContainer> imagesContainer;
    [SerializeField] List<Button> buttons;

    List<Dial> dialHours = new List<Dial>();
    List<Dial> inputHours = new List<Dial>();
     
    private void Awake()
    {
        inputHours = new List<Dial>();
        for (int i=0;i<buttons.Count;i++)
        {
            inputHours.Add(Dial.Hour12);
            int index = i;
            buttons[i].onClick.AddListener(() => OnClickHose(index));
        }
    }

    private void OnEnable()
    {
        if (dialHours.Count > 0)
            return;

        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Dial) == false)
            return;
        List<object> answer = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Dial];

        foreach(object an in answer)
            dialHours.Add((Dial)an);
    }

    private void CheckPuzzleState()
    {
        if (dialHours.Count == 0)
            return;

        if (IsPuzzleClear)
            return;
        bool isComplete = true;

        for(int i=0;i<dialHours.Count;i++)
        {
            if (dialHours[i] != inputHours[i])
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
            IsPuzzleClear = true;

    }

    public void OnClickHose(int index)
    {
        ImagesContainer ic = imagesContainer[index];
        ic.InCreaseIndex();
        buttons[index].image.sprite = ic.images[ic.CurImageIndex];
        inputHours[index] = (Dial)ic.CurImageIndex;

        //퍼즐이 맞는지 검사
        CheckPuzzleState();
    }

}
