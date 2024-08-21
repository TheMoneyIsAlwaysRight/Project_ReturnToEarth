using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LobbyScene : BaseScene
{
    private void Start()
    {
        Manager.Sound.StopBGM();
        Manager.Sound.PlayBGM("Login");
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }


    // 스토리보기는 나중에 추가해야함
}
