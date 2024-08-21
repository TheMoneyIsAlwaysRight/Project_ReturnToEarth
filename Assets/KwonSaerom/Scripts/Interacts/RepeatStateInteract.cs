using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatStateInteract : MonoBehaviour
{
    [SerializeField] GameObject render;
    [SerializeField] float time;
    private void Awake()
    {
        StartCoroutine(Repeat());
    }

    IEnumerator Repeat()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(time);
            render.SetActive(false);
            yield return new WaitForSecondsRealtime(time);
            render.SetActive(true);
        }
    }
}
