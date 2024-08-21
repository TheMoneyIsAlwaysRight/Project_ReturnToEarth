using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisualEffect : PooledObject
{

    [Tooltip("the time after object will be destroyed")]
    public float destructionTime;

    private void OnEnable()
    {
        StartCoroutine(Destruction()); 
    }

    IEnumerator Destruction() 
    {
        yield return new WaitForSeconds(destructionTime);
        Release();
    }
}
