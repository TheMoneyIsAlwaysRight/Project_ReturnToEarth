using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SceneMove : BaseUI
{
    private SceneController sceneController;
        
    
    enum GameObjects
    {
        Left,
        Right
    }

    public override void LocalUpdate()
    {

    }
    protected override void Awake()
    {
        base.Awake();
        sceneController = GameObject.Find("Scenes").GetComponent<SceneController>();
        GetUI<Button>(GameObjects.Left.ToString()).onClick.AddListener(sceneController.MoveLeft);
        GetUI<Button>(GameObjects.Right.ToString()).onClick.AddListener(sceneController.MoveRight);
    }
}
