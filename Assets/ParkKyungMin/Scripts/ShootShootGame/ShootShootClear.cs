using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShootClear : MonoBehaviour
{
    [SerializeField] GameObject[] stars; // 별 오브젝트 배열
    [SerializeField] ShootPlayerController playerController;

    public int starScore; //정민 추가 : 스테이지 별 개수 기록용

    private void OnEnable()
    {
        PointCount();
    }

    public void PointCount()
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // 별 개수에 따름
        if (playerController.point >= 160)
        {
            stars[2].SetActive(true); // 별 3개 UI
            stars[1].SetActive(true); // 별 3개 UI
            stars[0].SetActive(true); // 별 3개 UI
            starScore = 3;
        }
        else if (playerController.point >= 140)
        {
            stars[1].SetActive(true); // 별 2개 UI
            stars[0].SetActive(true); // 별 2개 UI
            starScore = 2;
        }
        else if (playerController.point >= 120)
        {
            stars[0].SetActive(true); // 별 1개 UI
            starScore = 1;
        }

        Manager.Chapter.RecordData(starScore); //정민 추가 : 스테이지 클리어 기록용.
        MinigameStory.Instance.StartClearStory(); //새롬 추가 : 뒤에스토리 나오게
    }
}
