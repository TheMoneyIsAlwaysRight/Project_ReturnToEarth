using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebURL : MonoBehaviour
{
    public void OpenWeb(string webURL)
    {
        Application.OpenURL(webURL);
    }
}
