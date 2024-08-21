using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        Manager.Scene.LoadingText.text = "로딩중입니다.";
        Debug.Log("로딩중입니다.");
        Manager.Scene.LoadingBar.value = Mathf.Lerp(0.3f, 0.5f, 0.5f);
        yield return new WaitForSecondsRealtime(1f);
        Manager.Scene.LoadingText.text =  "기다리세요.";
        Debug.Log("기다리세요.");
        Manager.Scene.LoadingBar.value = Mathf.Lerp(0.5f, 0.7f, 0.5f);
        yield return new WaitForSecondsRealtime(1f);
        Manager.Scene.LoadingText.text = "곧 다 됩니다.";
        Debug.Log("곧 다 됩니다.");
        Manager.Scene.LoadingBar.value = Mathf.Lerp(0.7f, 0.9f, 0.5f);
        yield return new WaitForSecondsRealtime(1f);
        Manager.Scene.LoadingText.text = "뿡뿡아";
        Debug.Log("뿡뿡아");
        Manager.Scene.LoadingBar.value = Mathf.Lerp(0.9f, 1f, 0.5f);
        yield return null ;
    }

    public void TransScene()
    {
        Manager.Scene.LoadScene("Test2");
    }
}
