using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    [SerializeField] GameObject hint;


    public void OnHint()
    {
        hint.SetActive(true);
        // 일시 정지
        Time.timeScale = 0;
    }

    public void OnReturn()
    {
        hint.SetActive(false);
        // 일시정지 해제
        Time.timeScale = 1;
    }
}
