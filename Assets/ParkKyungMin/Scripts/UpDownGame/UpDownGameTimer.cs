using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownGameTimer : BaseUI
{
    [SerializeField] Slider timerSlider;
    [SerializeField] GameObject clearUI;        // 클리어 UI 창
    [SerializeField] float timer;

    bool isUpload = false;

    float tmpTimer = 0;

    enum GameObjects
    {
        timer
    }
    public override void LocalUpdate()
    {

    }

    protected override void Awake()
    {
        base.Awake();

        // timer의 최대값을 시간으로 맞춤
        timerSlider.maxValue = timer;
        clearUI.SetActive(false);
    }

    private void Start()
    {
        timeSet(timer);

    }

    void Update()
    {
        // 게이지바를 현재시간에 맞춤 
        timerSlider.value = timer;
        timer -= Time.deltaTime;
        timeSet(timer);

        if (timer <= 0)
        {
            timer = 0;  // 타이머가 0 이하로 내려가지 않도록 설정
            if (isUpload == false)
            {
                ClearGame();  // 게임 클리어 처리
                isUpload = true;
            }
        }

        if (tmpTimer >= 60f)
        {
            tmpTimer = 0;  // 임시 타이머 초기화
        }
    }

    private void timeSet(float time)
    {
        int hour = (int)time / 60;
        int min = (int)time % 60;
    }

    private void ClearGame()
    {
        // 클리어 UI 창 활성화
        clearUI.SetActive(true);
        Manager.Chapter.RecordData(3); //정민 추가 : 스테이지 클리어 기록용.
        MinigameStory.Instance.StartClearStory(); //새롬 추가 : 뒤에스토리 나오게
        Time.timeScale = 0;
    }
}
