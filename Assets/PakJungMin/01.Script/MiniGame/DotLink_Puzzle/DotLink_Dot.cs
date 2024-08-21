using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotLink_Dot : MonoBehaviour
{
    public enum State
    {
        Red,
        Blue,
        Green,
        Yellow,
        Blank
    }

    public DotLink_LogicManager logicManager;

    public int posX;
    public int posY;
    public bool startDot;
    [SerializeField] State curState;
    public State CurState { get { return curState; } }


    public DotLink_Dot prevDot; //양 끝 정점일 경우는 자기 자신.
    public DotLink_Dot nextDot;

    public DotLink_Line ownLine;

    private void Start()
    {
        if(!startDot)
        {
            curState = State.Blank;
        }
    }

    public void ChangeState(State newState)
    {
        curState = newState;
    }
    
}
