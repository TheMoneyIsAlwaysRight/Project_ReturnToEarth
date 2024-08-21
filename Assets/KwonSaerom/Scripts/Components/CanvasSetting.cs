using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 컴포넌트처럼 씀. 
/// </summary>
public class CanvasSetting : MonoBehaviour
{
    private void Start()
    {
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        if (scaler != null)
            DisplayManager.Instance.SetCanvas(GetComponent<CanvasScaler>());
    }
}
