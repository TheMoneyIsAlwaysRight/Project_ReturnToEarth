using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static Alphabet_Number_RoterData;

public class Alphabet_Btn : MonoBehaviour
{
    [SerializeField]Alphabet_Number_RoterData roterData;
    [SerializeField] BtnState upDown;

    public void OnClickbtn()
    {
        roterData.ChangeState(this.upDown);
    }
}
