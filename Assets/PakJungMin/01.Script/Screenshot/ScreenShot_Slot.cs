using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot_Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject ALbum;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(GetComponent<Image>().sprite != null)
            {
            ALbum.SetActive(true);
            ALbum.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        }
    }
}
