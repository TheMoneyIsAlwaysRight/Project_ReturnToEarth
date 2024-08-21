using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BlinkBlink : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    Coroutine tmpCoroutine;
    float delay = 0.5f;

    void Start()
    {
        tmpCoroutine = StartCoroutine(BlinkBlink());
    }

    IEnumerator BlinkBlink()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

        yield return new WaitForSeconds(delay);

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        yield return new WaitForSeconds(delay);

        tmpCoroutine = StartCoroutine(BlinkBlink());
    }

    public void StopBlink()
    {
        StopCoroutine(tmpCoroutine);
    }
}
