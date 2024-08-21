using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunRunGameScene : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
    }

    public void LoadGameScene()
    {
        Manager.Scene.LoadScene("RunRunGameScene");
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
