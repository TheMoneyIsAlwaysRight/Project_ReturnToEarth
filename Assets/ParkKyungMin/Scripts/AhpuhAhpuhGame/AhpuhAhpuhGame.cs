using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhpuhAhpuhGame : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
    }

    public void LoadGameScene()
    {
        Manager.Scene.LoadScene("AhpuhAhpuhGameScene");
        Time.timeScale = 1;
    }

    public void LoadChapterScene()
    {
        Manager.Scene.LoadScene("ChapterScene");
        Time.timeScale = 1;
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
