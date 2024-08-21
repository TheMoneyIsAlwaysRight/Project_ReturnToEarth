using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frequency_Switch : MonoBehaviour
{

    [SerializeField] Frequency_LogicManager logicManager;

    [Header("스위치가 매칭된 인덱스")]
    public int number;

    [Header("Renderer")]
    [SerializeField] Image renderer_; //Renderer의 스프라이트.

    [Header("True Stae Image")]
    [SerializeField] Sprite trueSprite;
    [Header("False State Image")]
    [SerializeField] Sprite falseSprite;

    public bool value;


    public void ChangeState()
    {
        value = !value;

        if(value)
        {
            renderer_.sprite = trueSprite;
        }
        else
        {
            renderer_.sprite = falseSprite;
        }
        logicManager.CheckAnswer();
    }








    private void Start()
    {
        logicManager.switchList[number] = this;
    }


}
