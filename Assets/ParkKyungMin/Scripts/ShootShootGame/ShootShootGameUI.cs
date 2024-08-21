using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShootGameUI : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public void LoadGameScene()
    {
        Manager.Scene.LoadScene("ShootShootGameScene");
        Time.timeScale = 1;
    }

    public void LoadChapterScene()
    {
        Manager.Scene.LoadScene("ChapterScene");
        Time.timeScale = 1;
    }
}
