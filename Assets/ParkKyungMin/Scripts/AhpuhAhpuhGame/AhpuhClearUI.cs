using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhpuhClearUI : MonoBehaviour
{
    [SerializeField] GameObject[] stars; // 별 오브젝트 배열
    [SerializeField] AhpuhPlayerController playerController;

    public int starScore;

    private void Start()
    {
        StarCount();
    }
    
    public void StarCount()
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // 별 개수에 따름
        if (playerController.starPoint >= 30)
        {
            stars[2].SetActive(true); // 별 3개 UI
            stars[1].SetActive(true); // 별 3개 UI
            stars[0].SetActive(true); // 별 3개 UI
            starScore = 3;
        }
        else if (playerController.starPoint >= 20)
        {
            stars[1].SetActive(true); // 별 2개 UI
            stars[0].SetActive(true); // 별 2개 UI
            starScore = 2;
        }
        else
        {
            stars[0].SetActive(true); // 별 1개 UI
            starScore = 1;
        }
        Manager.Chapter.RecordData(starScore); //정민 추가 : 스테이지 클리어 기록용.
        MinigameStory.Instance.StartClearStory(); //새롬 추가 : 뒤에스토리 나오게
    }
}
