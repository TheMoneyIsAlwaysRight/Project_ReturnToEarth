using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : PopUpUI
{
    [SerializeField] NewCharacterScripts characterScripts;
    [SerializeField] UI_StoryNextUI storyUI;

    public NewCharacterScripts CharacterScripts { get { return characterScripts; } set { characterScripts = value; } }

    protected override void Awake()
    {
        base.Awake();
        storyUI.gameObject.SetActive(false);
        characterScripts.StoryUI = storyUI;
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void CloseSelf()
    {
        Debug.Log("대사창 닫음");
        Manager.UI.ClosePopUpUI();
    }
}
