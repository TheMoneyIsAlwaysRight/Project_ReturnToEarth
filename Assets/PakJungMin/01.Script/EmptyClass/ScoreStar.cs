using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStar : MonoBehaviour
{
    private void Awake()
    {
        StageSellect stageSellect = GetComponentInParent<StageSellect>();
        stageSellect.starList.Add(this);
    }
}
