using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLocker : MonoBehaviour
{   
    private void Awake()
    {
        StageSellect stageSellect = GetComponentInParent<StageSellect>();
        stageSellect.Img_Locker = this;    
    }    
}
