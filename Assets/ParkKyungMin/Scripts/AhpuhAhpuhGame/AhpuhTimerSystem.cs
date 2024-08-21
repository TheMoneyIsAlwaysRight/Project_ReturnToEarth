using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class AhpuhTimerSystem : BaseUI
{
    [SerializeField] Slider timerSlider;
    [SerializeField] GameObject clearUI;  // 클리어 UI 창
    [SerializeField] float timer;
    [SerializeField] AhpuhPlayerController playerController;
    [SerializeField] GameObject gameOver;

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
    // Update is called once per frame
    void Update()
    {
        // 게이지바를 현재시간에 맞춤 
        timerSlider.value = timer;
        timer -= Time.deltaTime;
        timeSet(timer);

        if (timer <= 0)
        {
            timer = 0;  // 타이머가 0 이하로 내려가지 않도록 설정

            // 별 갯수가 0일때는 Gameover
            if (playerController.starPoint == 0)
            {
                GameOver();
            }
            else
            {
                ClearGame();  // 게임 클리어 처리
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
  
        Time.timeScale = 0;
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
