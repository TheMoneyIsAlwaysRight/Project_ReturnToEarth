using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Density_Button : MonoBehaviour
{
    [SerializeField] Density_LogicManager logicManager;
    [SerializeField] Density_Data spritedata;

    [SerializeField] Image thisimage;

    [Header("스위치가 매칭된 인덱스")]
    public int number;

    public int value;

    public void Start()
    {
        logicManager.switchList[number] = this;
    }


    public void ChangeState()
    {
        value++;
        if(value>9)
        {
            value = 0;
        }

        thisimage.sprite = spritedata.sprites[value];

        logicManager.CheckAnswer();
    }

}
