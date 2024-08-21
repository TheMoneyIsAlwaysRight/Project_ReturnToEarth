using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSceneUI : MonoBehaviour
{
    [SerializeField] GameObject stageUI;
    [SerializeField] GameObject chapterUI;

    public void OnStage()
    {
        chapterUI.SetActive(false);
        stageUI.SetActive(true);
    }

    public void StageOut()
    {
        stageUI.SetActive(false);
        chapterUI.SetActive(true);
    }
    public void LobbySceneLoad()
    {
        Manager.Scene.LoadScene("LobbyScene");
    }
    public void GameSceneLoad()
    {
        Manager.Scene.LoadScene("InGameScene");
    }
}
