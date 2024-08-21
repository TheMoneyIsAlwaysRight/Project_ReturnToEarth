using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Coding_Letter : MonoBehaviour,IPointerClickHandler
{
    public enum State
    {
        Word_L,
        Word_I,
        Word_S,
        Word_T,
        Word_E,
        Word_N,

    }
    [SerializeField] Coding_LogicManager logicManager;

    public State curState;
    public Image thisImage;

    public State CurState { get { return curState; } }
    public void ChangeState(State state)
    {
        curState = state;

        thisImage.sprite = logicManager.spriteData.sprites[(int)curState];

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (logicManager.curLetter == null)
        { 
            logicManager.curLetter = this;
        }
        else
        {
            logicManager.SwapLetter(this);
        }

    } 
    public void Start()
    {
        logicManager.letterList[(int)curState] = this;
    }
}
