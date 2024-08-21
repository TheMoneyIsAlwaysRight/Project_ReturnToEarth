using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clue_Object : MonoBehaviour
{
    [SerializeField] int priority;
    [SerializeField] bool isClear;
    UnityAction onCheckAction;

    public int Prio { get { return priority; } set { priority = value; } }
    public bool IsClear { 
        get { return isClear; } 
        set 
        { 
            isClear = value;

            // 권새롬 추가 ---> isClear 가 바뀔때마다 플로우 체크 호출
            OnCheckFlow?.Invoke();
        } 
    }

    public UnityAction OnCheckFlow { get { return onCheckAction; } set { onCheckAction = value; } }
}
