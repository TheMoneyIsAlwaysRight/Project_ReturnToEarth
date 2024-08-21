using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : Singleton<FrameManager>
{
    protected override void Awake()
    {
        base.Awake();
#if UNITY_IOS || UNITY_ANDROID
        Application.targetFrameRate = 60;
#else
        QualitySettings.vSyncCount = 1;
#endif
    }
}
