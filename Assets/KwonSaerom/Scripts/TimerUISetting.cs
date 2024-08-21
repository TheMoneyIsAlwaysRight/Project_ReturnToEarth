using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUISetting : MonoBehaviour
{
    [SerializeField] TimerSystem hiddenTimer;
    [SerializeField] TimerSystem baseTimer;

    private void Awake()
    {
        if (Manager.Game.CurStageKey != Define.HIDDEN_CHAPTER)
        {
            Manager.Game.Timer = baseTimer;
            return;
        }

        baseTimer.gameObject.SetActive(false);
        hiddenTimer.gameObject.SetActive(true);
        Manager.Game.Timer = hiddenTimer;
    }
}
