using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpDownToken : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Image image;

    public int CurInput { get; set; } = 0;
    private ExitDoor_Puzzle puzzle;
    private void Start()
    {
        puzzle = GetComponentInParent<ExitDoor_Puzzle>();
        puzzle.Tokens.Add(this);
    }

    public void OnClickedUpButton()
    {
        CurInput++;
        if (CurInput >= sprites.Count)
            CurInput = 0;
        image.sprite = sprites[CurInput];
        puzzle.CheckPuzzleState();
    }

    public void OnClickedDownButton()
    {
        CurInput--;
        if (CurInput < 0)
            CurInput = sprites.Count - 1;
        image.sprite = sprites[CurInput];
        puzzle.CheckPuzzleState();
    }
}
