using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pressure_Puzzle : BasePuzzle
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] List<Button> buttons;
    [SerializeField] List<GameObject> colors;
    [SerializeField] Slider slider;

    private int[] values = { 17, -11, 13, -9 };
    private const int MIN_MAX = 50;

    private UI_Log logUI;
    private int[] states = { 1, 1, 1, 1 };

    private void Awake()
    {
        // 셀프 다국어... 테이블 나오면 적용하장
        if(Manager.Game.LanguageSet == Define.LanguageSet.KR)
        {
            titleText.text = "기압조절장치";
        }else
        {
            titleText.text = "pressure control";
        }

        slider.maxValue = MIN_MAX;
        slider.minValue = -MIN_MAX;
        slider.value = Calculator(); // 초기값.

        for(int i=0;i<buttons.Count;i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => PlusValue(index));
        }
    }

    private void Start()
    {
        logUI = Manager.Resource.Load<UI_Log>("UI_Log");
    }

    public void ClickedOKButton()
    {
        if (slider.value == 0)
        {
            IsPuzzleClear = true;
        }else
        {
            UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
            popup.SetLog(102181);
        }
    }


    public void PlusValue(int index)
    {
        int ColorIndex = index * 2;

        if (colors[ColorIndex].activeSelf == false)
        {
            colors[ColorIndex].SetActive(true);
            states[index] = 2;
        }
        else if(colors[ColorIndex + 1].activeSelf == false)
        {
            colors[ColorIndex + 1].SetActive(true);
            states[index] = 3;
        }
        else
        {
            colors[ColorIndex].SetActive(false);
            colors[ColorIndex + 1].SetActive(false);
            states[index] = 1;
        }

        slider.value = Calculator();
    }

    private int Calculator()
    {
        int result = 0;
        for (int i=0;i< states.Length;i++)
        {
            result += values[i] * states[i];
        }
        return result;
    }
}
