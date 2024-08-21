using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Roter_Button_Controller : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] RoterData roterData;
    [SerializeField] ButtonState curState;

    public enum ButtonState
    {
        Up,
        Down
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(curState)
        {
            case ButtonState.Up:
                roterData.CurNumber++;
                break;
            case ButtonState.Down:
                roterData.CurNumber--;
                break;
        }
    }
}
