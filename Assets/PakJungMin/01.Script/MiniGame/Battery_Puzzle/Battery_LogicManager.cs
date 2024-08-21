using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Battery_LogicManager : MonoBehaviour
{
    //권새롬 추가 --> 답을 분별하기 위해 추가함
    public enum BatteryState
    {
        _1860W = 0,
        _1870W,
        _1880W,
        _1890W
    }

    [Serializable]
    public class Battery
    {
        public BatteryState BatteryW;
        public string Answer;
    }

    [SerializeField] List<Battery> batteries;
    //---------------------------------------

    public Battery_Puzzle puzzle;
    public GameObject batterySet;
    public Dictionary<string, Battery_Battery> batteryDic = new Dictionary<string, Battery_Battery>();
    public Battery_SpriteData spriteData;

    Battery_Battery[] batArray;

    private List<bool> correct;
    public void Start()
    {
        batArray = batterySet.GetComponentsInChildren<Battery_Battery>();

        int x = 0;
        int y = 4;
        foreach (Battery_Battery battery in batArray)
        {
            battery.gameObject.name = $"{x},{y}";
            x++;
            batteryDic.Add(battery.gameObject.name, battery);
            if (x > 4)
            {
                y--;
                x = 0;
            }
        }

        InitCorrect();
    }

    //권새롬 추가 ---> 답 초기화
    private void OnEnable()
    {
        InitCorrect();
    }

    private void InitCorrect()
    {
        if (correct != null)
            return;
        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap04_2_Battery) == false)
            return;

        int answer = (int)PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap04_2_Battery][0];

        string stringAnswer = batteries[answer].Answer;
        correct = new List<bool>();
        
        for(int i=0;i< stringAnswer.Length;i++)
        {
            correct.Add(stringAnswer[i] == '1');
        }
    }
    //-------------------------------

    public void CheckAnswer()
    {
        //권새롬 추가--> 예외처리
        if (correct == null)
            return;
        //-----------------

        int z = 0;
        for(int y=4;y>=0;y--)
        {
            for(int x=4;x>=0;x--)
            {
                Debug.Log($"정답 인덱스 {z} 값 : {correct[z]} // 딕셔너리 인덱스{x},{y}값 : {batteryDic[$"{x},{y}"].CurState}");
                if (batteryDic[$"{x},{y}"].CurState != correct[z])
                {

                    return;
                }
                z++;
            }
        }
        puzzle.IsPuzzleClear = true;
    }




}
