using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Battery_Battery : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] Battery_LogicManager logicManager;
    [SerializeField] Image thisImage; //자식 오브젝트의 렌더러.

    bool curState;

    public bool CurState { get { return curState; } }

    public void ChangeState()
    {
        curState = !curState;
        Debug.Log("aaa : " + curState);
        if (curState)
        {
            thisImage.sprite = logicManager.spriteData.sprites[1];
        }
        else
        {
            thisImage.sprite = logicManager.spriteData.sprites[0];
        }
        logicManager.CheckAnswer();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeState();
    }

    private void Start()
    {
        curState = false;
        thisImage.sprite = logicManager.spriteData.sprites[0];
    }
}
