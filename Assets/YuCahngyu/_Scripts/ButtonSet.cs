using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSet : MonoBehaviour
{
    [SerializeField] LobbySceneUI scene;

    private void Start()
    {
        scene.ButtonSet();
    }
}
