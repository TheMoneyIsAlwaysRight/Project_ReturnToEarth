using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Puzzle : PopUpUI
{
    enum GameObjects
    {
        Back
    }

    public override void LocalUpdate()
    {

    }
    public void SetImage(bool active)
    {
        GetUI<Image>(GameObjects.Back.ToString()).gameObject.SetActive(active);
    }
}
