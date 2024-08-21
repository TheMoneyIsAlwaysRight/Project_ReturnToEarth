using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunRunGameTimer : BaseUI
{
    [SerializeField] Slider timerSlider;
    [SerializeField] GameObject clearUI;        // 클리어 UI 창
    [SerializeField] GameObject clearVideo;     // 클리어시 재생되는 영상
    [SerializeField] float timer;
    [SerializeField] AudioSource bGM;

    float tmpTimer = 0;
    bool isClear = false;

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

        if (timer <= 0 && isClear == false)
        {
            isClear = true; //권새롬추가 --> isClear 예외처리(한번만 호출되게)
            timer = 0;  // 타이머가 0 이하로 내려가지 않도록 설정
            StartCoroutine(ClearGame());  // 게임 클리어 처리
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

    private IEnumerator ClearGame()
    {
        bGM.Stop();
        Time.timeScale = 0;

        clearVideo.SetActive(true);
        
        // timeScale이 0인 상태에서 2.5초 뒤에 재생시킴
        yield return new WaitForSecondsRealtime(2.2f);
        clearVideo.SetActive(false); //권새롬 추가 --> 비디오끄게 (레이캐스트 안먹히는 문제가 있을수도 있음)

        // 클리어 UI 창 활성화
        MinigameStory.Instance.StartClearStory(); //새롬 추가 : 뒤에스토리 나오게
        clearUI.SetActive(true);
        Manager.Chapter.RecordData(3); //정민 추가 : 스테이지 클리어 기록용.
    }
}
