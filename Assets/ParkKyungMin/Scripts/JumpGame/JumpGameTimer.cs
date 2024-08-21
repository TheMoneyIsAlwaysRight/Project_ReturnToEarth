using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpGameTimer : BaseUI
{

    [SerializeField] Slider timerSlider;
    [SerializeField] GameObject gameoverUI;  //  게임오버 UI 창
    [SerializeField] float timer;

    bool gameOver = false;
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
        gameoverUI.SetActive(false);
    }

    private void Start()
    {
        timeSet(timer);

    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            return;
        // 게이지바를 현재시간에 맞춤 
        timerSlider.value = timer;
        timer -= Time.deltaTime;
        timeSet(timer);

        if (timer <= 0)
        {
            timer = 0;  // 타이머가 0 이하로 내려가지 않도록 설정
            TimeOut();  // 게임 오버 처리
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

    private void TimeOut()
    {
        // 게임오버 UI 창 활성화 (timeout)
        Time.timeScale = 0;
        gameOver = true;
        gameoverUI.SetActive(true);
    }
}

