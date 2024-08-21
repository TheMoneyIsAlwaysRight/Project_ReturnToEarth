using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UpDownGameScene : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
    }

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpDownGameScene");
        Time.timeScale = 1;
    }

    public void LoadChapterScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ChapterScene");
        Time.timeScale = 1;
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}

