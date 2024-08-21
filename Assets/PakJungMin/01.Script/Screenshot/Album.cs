using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Album : MonoBehaviour,IPointerClickHandler
{
    [Header("앨범 뒤 패널")]
    [SerializeField] GameObject album_background;

    private void OnEnable()
    {
        album_background.SetActive(true);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        album_background.SetActive(false);
        gameObject.SetActive(false);
    }

}
