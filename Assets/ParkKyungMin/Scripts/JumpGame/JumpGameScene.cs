using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class JumpGameScene : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
    }

    public void LoadGameScene()
    {
        Manager.Scene.LoadScene("JumpGameScene");
        Time.timeScale = 1;
    }

    public void LoadChapterScene()
    {
        Debug.Log("챕터씬 간당");
        Manager.Scene.LoadScene("ChapterScene");
        Time.timeScale = 1;
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
