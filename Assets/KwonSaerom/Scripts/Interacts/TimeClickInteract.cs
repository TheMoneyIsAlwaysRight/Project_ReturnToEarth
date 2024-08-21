using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClickInteract : CameraInteractController
{
    [SerializeField] float LimitTime;
    [SerializeField] int clickNum;

    private int count = 0;
    private float nowTime = 0;
    private bool isInteract = false;

    public override void PuzzleOnInteract()
    {
        if (isInteract == true)
            return;
        count++;
        if (count == 1)
            StartCoroutine(ClickTime());
    }

    IEnumerator ClickTime()
    {
        while(true)
        {
            nowTime += Time.deltaTime;
            if(nowTime > LimitTime)
            {
                if (info.interactText != 0)
                {
                    UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
                    popup.SetLog(info.interactText);
                }

                count = 0;
                nowTime = 0;
                yield break;
            }

            if(count >= clickNum)
            {
                break;
            }

            yield return null;

            if(count == 0)
            {
                yield break;
            }
        }

        isInteract = true;
        InteractObjectItem();
    }

    public override void Close()
    {
        base.Close();
        count = 0;
        nowTime = 0;
    }

    public override void Load()
    {
        InteractObjectItem(true);
        isInteract = true;
    }
}
