using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Universe_Puzzle;

public class PlanetToken : MonoBehaviour,IEndDragHandler
{
    [SerializeField] List<float> attachPos;
    [SerializeField] Image handleImage;
    [SerializeField] TMP_Text debugText;

    public int CurPos { get; set; } = 7; //처음엔 0으로 초기화. 안쪽으로 들어갈수록 +1 됨.
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if(slider.value < (attachPos[1] - attachPos[0]) / 2)
        {
            slider.value = attachPos[0];
            CurPos = attachPos.Count - 1;
            return;
        }

        if((attachPos[attachPos.Count-1] + attachPos[attachPos.Count - 2]) / 2 <= slider.value)
        {
            slider.value = attachPos[attachPos.Count-1];
            CurPos = 0;
            return;
        }

        for(int i=1;i<attachPos.Count-1;i++)
        {
            //중간사이값으로 계산.
            if ((attachPos[i] - attachPos[i-1])/2 <= slider.value && slider.value < (attachPos[i+1] + attachPos[i]) / 2)
            {
                slider.value = attachPos[i];
                CurPos = attachPos.Count - 1 - i;
                break;
            }
        }
    }

    public void SetHandleInfo(Planet info)
    {
        handleImage.sprite = info.sprite;
        debugText.gameObject.SetActive(false);
        //if(debugText != null)
        //    debugText.text = (int)info.state+"";

    }
}
