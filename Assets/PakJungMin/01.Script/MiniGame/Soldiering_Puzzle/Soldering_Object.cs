using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Soldering_Object : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    [SerializeField] Soldering_LogicManager logicManager;

    [SerializeField] Image thisImage;

    [SerializeField] float maxTime;
    [SerializeField] float ownTime;

    bool isClear;
    public bool IsClear 
    { 
        get
        { 
            return isClear;
        }
    }

    Coroutine coroutine;
    IEnumerator SolderingTime()
    {
        while(true)
        {
            ownTime -= Time.deltaTime;
            if(ownTime <= 0)
            {
                isClear = true;
                thisImage.color = new Color(255, 255, 255, 0);
                logicManager.CheckAnswer();
                break;
            }
            yield return null;    
        }
    }

    private void Start()
    {
        logicManager.soldObjects.Add(this);
        ownTime = maxTime;   
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("sss");
        if (coroutine == null)
        {
            coroutine = StartCoroutine(SolderingTime());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (ownTime > 0)
        {
            StopCoroutine(coroutine);
            ownTime = maxTime;
            coroutine = null;
        }
        //else
        //{
        //    isClear = true;
        //    thisImage.color = new Color(255, 255, 255, 0);
        //    logicManager.CheckAnswer();
        //}
    }
}
