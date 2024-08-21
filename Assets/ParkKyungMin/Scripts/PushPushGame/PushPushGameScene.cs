using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPushGameScene : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
    }
    public void LoadGameScene()
    {
        Manager.Scene.LoadScene("PushPushGameScene");
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
