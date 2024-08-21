using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    [SerializeField] LeverGame.LeverState curState;
    [SerializeField] List<Sprite> leverStateSprites;
    [SerializeField] Image curImage;

    private void Start()
    {
        curState = LeverGame.LeverState.Middle;
    }

    public LeverGame.LeverState CurState { set 
        { 
            curState = value;
            curImage.sprite = leverStateSprites[(int)value];
        }
        get { return curState; } }
}
