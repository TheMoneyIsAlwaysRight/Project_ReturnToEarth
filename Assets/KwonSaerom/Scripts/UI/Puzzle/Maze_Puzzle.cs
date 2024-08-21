using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze_Puzzle : BasePuzzle
{
    [SerializeField] Image puzzleImage;
    [SerializeField] Sprite completeSprite;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject inGameExit;

    private GameObject MazeGame;
    private GameObject loadMazeGame;
    private bool isClear = false;

    private void Awake()
    {
        MazeGame = Manager.Resource.Load<GameObject>("Maze_Game");
        SetPuzzleUI(true);
    }

    public void GameStart()
    {
        if (IsPuzzleClear)
            return;

        SetPuzzleUI(false);

        //미로찾기 게임 생성하기
        if(loadMazeGame == null)
        {
            loadMazeGame = Instantiate(MazeGame);
            loadMazeGame.transform.position = Manager.Game.Camera.GetGameCamPosition();
            loadMazeGame.GetComponentInChildren<Maze_Exit>().Puzzle = this;
        }
        Manager.Game.Camera.CurrentCamState = CameraController.CameraState.GameCamera;
    }

    public void CompleteGame()
    {
        ReturnPuzzle();

        puzzleImage.sprite = completeSprite;
        Destroy(loadMazeGame);
        isClear = true;
    }

    public void ReturnPuzzle()
    {
        Manager.Game.Camera.CurrentCamState = CameraController.CameraState.ZoomInObject;
        SetPuzzleUI(true);
    }

    private void SetPuzzleUI(bool active)
    {
        panel.SetActive(active);
        inGameExit.SetActive(!active);
        PuzzleManager.Inst.SetBG(active);
    }

    public override void SetGameOff()
    {
        if(isClear)
            IsPuzzleClear = true;
        base.SetGameOff();
    }
}
