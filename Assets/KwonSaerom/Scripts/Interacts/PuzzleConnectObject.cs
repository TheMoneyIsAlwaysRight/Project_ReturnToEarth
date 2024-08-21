using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConnectObject : PuzzleActiveInteract
{
    [SerializeField] int connectKey;

    public override void CompletePuzzle()
    {
        GameObject connectObject = gameScene.FindInteractObject(connectKey).gameObject;
        connectObject.SetActive(false);
        base.CompletePuzzle();
    }

    public override void Load()
    {
        GameObject connectObject = gameScene.FindInteractObject(connectKey).gameObject;
        connectObject.SetActive(false);
        CompletePuzzle();
    }
}
