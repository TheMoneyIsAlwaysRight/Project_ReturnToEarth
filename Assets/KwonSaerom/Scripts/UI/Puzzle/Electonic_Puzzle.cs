using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Electonic_Puzzle : BasePuzzle
{
    [Serializable]
    public class ImagesContainer
    {
        public Button button;
        public List<Sprite> images;
        public HoseColor Hose { get; set; }

        public void SetImageColor(int hose)
        {
            button.image.sprite = images[hose];
            Hose = (HoseColor)hose;
        }

        public void SetImageColor(HoseColor hose)
        {
            button.image.sprite = images[(int)hose];
            Hose = hose;
        }
    }

    [SerializeField] TMP_Text debug;
    [SerializeField] List<ImagesContainer> imagesContainer;
    [SerializeField] List<Button> buttons;

    List<HoseColor> hoseColors = new List<HoseColor>();
    List<bool> checkColors = new List<bool>();
    int clickedIndex = -1;

    private void Awake()
    {
        for(int i=0;i<buttons.Count;i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnClickHose(index));

            // 체크컬러 초기화
            checkColors.Add(false);
        }
    }

    private void OnEnable()
    {
        if (hoseColors.Count > 0)
            return;

        List<object> answer = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Hose];
        foreach(object an in answer)
            hoseColors.Add((HoseColor)an);
    }

    private void Start()
    {
        RandomInit();
    }


    private void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        bool isComplete = true;
        for (int i=0;i<hoseColors.Count;i++)
        {
            if (hoseColors[i] != imagesContainer[i].Hose)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            IsPuzzleClear = true;
            GameObject.Find("107019(Clone)").GetComponent<RandomInitInteract>().RandomInit();
        }

    }

    public void OnClickHose(int index)
    {
        //첫번째 클릭이면
        if(clickedIndex == -1)
        {
            clickedIndex = index;
            return;
        }

        //두번째 클릭이면
        HoseColor tmpColor = imagesContainer[index].Hose;
        imagesContainer[index].SetImageColor(imagesContainer[clickedIndex].Hose);
        imagesContainer[clickedIndex].SetImageColor(tmpColor);

        clickedIndex = -1;
        
        //퍼즐이 맞는지 검사
        CheckPuzzleState();
    }

    private void RandomInit()
    {
        if (hoseColors.Count == 0)
            return;

        int count = buttons.Count;
        for (int i = 0; i < count; i++)
        {
            int random = UnityEngine.Random.Range(0, count);
            while(true)
            {
                if (checkColors[random] == false)
                {
                    checkColors[random] = true;
                    imagesContainer[i].SetImageColor(random);
                    break;
                }
                random = UnityEngine.Random.Range(0, count);
            }
        }

        //랜덤이 정답과 같으면 안됨..
        bool isComplete = true;
        for (int i = 0; i < hoseColors.Count; i++)
        {
            if (hoseColors[i] != imagesContainer[i].Hose)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            RandomInit();
        }
    }
}
