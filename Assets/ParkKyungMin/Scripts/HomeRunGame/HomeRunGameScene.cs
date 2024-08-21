using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeRunGameScene : BaseScene
{
    [SerializeField] AudioSource introBGM;
    [SerializeField] AudioSource inGameBGM;
    [SerializeField] GameObject timerUI;

    private void Start()
    {
        StartCoroutine(PlayIntroThenShowUI());

        Manager.Sound.StopBGM();
    }

    IEnumerator PlayIntroThenShowUI()
    {

        yield return new WaitForSeconds(9.5f);

        timerUI.SetActive(true);

        yield return new WaitForSecondsRealtime(3.4f);


        Time.timeScale = 1;

        inGameBGM.Play();
    }

    public void LoadGameScene()
    {
        Manager.Scene.LoadScene("HomeRunGameScene");
        Time.timeScale = 1;
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public void LoadChapterScene()
    {
        Manager.Scene.LoadScene("ChapterScene");
        Time.timeScale = 1;
    }

}
