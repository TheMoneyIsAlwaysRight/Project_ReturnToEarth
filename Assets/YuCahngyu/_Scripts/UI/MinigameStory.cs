using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinigameStory : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] UI_Script scriptUI;

    private static MinigameStory instance;
    public static MinigameStory Instance { get { return instance; } }

    private bool onScript = false; //스크립트를 이미 띄웠는지 검사

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        Time.timeScale = 0f;
    }

    private void Start()
    {
        // initPitch = audioSource.pitch;

        //시작하자 마자 스크립트 시작
        UI_Script script = Manager.UI.ShowPopUpUI(scriptUI);
        script.CharacterScripts.Execute(true, NewCharacterScripts.BackUI.MiniGameUI);
    }

    public void StartClearStory()
    {
        if (onScript == true)
            return;

        onScript = true;
        UI_Script script = Manager.UI.ShowPopUpUI(scriptUI);
        script.CharacterScripts.Execute(false, NewCharacterScripts.BackUI.MiniGameUI);
    }

    public void AudioStart()
    {
        audioSource.Play();
    }
}
