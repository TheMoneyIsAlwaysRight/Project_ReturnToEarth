using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equation_Number : MonoBehaviour
{

    [Header("값")]
    public int number;

    [Header("개수")]
    public int count = 0;

    [SerializeField] Equation_LogicManager logicManager;
    [SerializeField] TMP_InputField inputNumber; //inputfield의 자식 오브젝트의 tmp_text

    private void Start()
    {
        logicManager.numberList.Add(this);
        inputNumber.text = "0";
    }

    public void Inputcount()
    {
        if (inputNumber.text == "" || inputNumber.text == " ")
        {
            inputNumber.text = "0";
        }
            count = Int32.Parse(inputNumber.text);
            logicManager.CheckAnswer();
    }
}
