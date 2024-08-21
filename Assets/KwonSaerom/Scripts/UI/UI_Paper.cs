using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Paper : PopUpUI
{
    enum GameObjects
    {
        PaperImage
    } 

    [SerializeField] Sprite baseImage;
    [SerializeField] Sprite changeImage;
    [SerializeField] List<Sprite> numberImages;
    [SerializeField] List<Image> answerImages; //결과값
    private const int RED_LENSE_KEY = 106019;


    protected override void Awake()
    {
        base.Awake();
        SetPaper();
    }

    // 플레이어가 렌즈를 착용 했는지의 여부에 따라 종이가 달라짐.
    private void SetPaper()
    {
        Item equipItem = ItemManager.Instance.GetEquipItem();
        // 빨간 렌즈를 착용 했으면
        if (equipItem != null && equipItem.ItemData.ID == RED_LENSE_KEY)
        {
            GetUI<Image>(GameObjects.PaperImage.ToString()).sprite = changeImage;
            SetNumber();
        }else
            GetUI<Image>(GameObjects.PaperImage.ToString()).sprite = baseImage;
    }


    private void SetNumber()
    {
        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap02_1_USB) == false)
            SetInitNumber();
        List<object> answer = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap02_1_USB];

        for (int i = 0; i < answerImages.Count; i++)
        {
            answerImages[i].gameObject.SetActive(true);
            answerImages[i].sprite = numberImages[(int)answer[i]-1];
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

        for (int i = 0; i < answerImages.Count; i++)
        {
            randomNum = Random.Range(0, numberSize);
            while (true)
            {
                if (isCheck[randomNum] == false)
                {
                    isCheck[randomNum] = true;
                    break;
                }
                randomNum = Random.Range(0, numberSize);
            }
            selectInfo.Add(randomNum + 1);
        }

        PuzzleManager.Inst.CorrectAnswers.Add((int)PuzzleAnswerKey.Chap02_1_USB, selectInfo);
    }
}
