using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : BaseUI
{
    public Slider timerSlider;  //timerSlider -경민추가 
    public float CurTimer { get { return ownTimer; } set { ownTimer = value; } }
    public float MaxTimer { get { return maxTimer; } }      // 찬규 추가 --> 스토리 UI종료 시 흘러간 시간 리셋
    float[] starTime = { 7 * 60f, 3 * 60f };
    float maxTimer = Define.MAX_TIMER;
    float ownTimer;
    enum GameObjects
    {
        timer
    }


    IEnumerator GameTimer()
    {
        while (true)
        {
            ownTimer -= Time.deltaTime;
            yield return null;
            if (0 >= ownTimer)
            {
                TimeOut();
                break;
            }
        }
    }

    public override void LocalUpdate()
    {

    }
    protected override void Awake()
    {
        base.Awake();
        //Manager.Game.Timer = this;
        // timer의 최대값을 시간으로 맞춤
        TimerInit();
        timerSlider.maxValue = maxTimer;
    }
    private void Start()
    {
        Debug.Log("게임 카운트 다운 시작");
        StartCoroutine(GameTimer());
    }
    // Update is called once per frame
    void Update()
    {
        // 게이지바를 현재시간에 맞춤 
        timerSlider.value = ownTimer;
        // maxTimer -= Time.deltaTime;
    }

    void TimeOut()
    {
        Manager.Game.Gameover();
    }

    //별
    public int StarCalculate()
    {
        if (starTime[0] < ownTimer) // 7~10 별 3개
        {
            return 3;
        }
        else if (starTime[1] < ownTimer) // 3~7 별 2개
        {
            return 2;
        }
        else // 0~3 별1개
        {
            return 1;
        }
    }

    // 권새롬 추가 --> 타이머 초기화 함수
    public void TimerInit()
    {
        ownTimer = maxTimer;
    }

    public void SetTimer(float value)
    {
        ownTimer = value == 0 ? maxTimer : value;
    }
}
