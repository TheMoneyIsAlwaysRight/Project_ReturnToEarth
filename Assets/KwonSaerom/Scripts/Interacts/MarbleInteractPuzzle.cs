using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MarbleInteractPuzzle : PuzzleActiveInteract
{
    [SerializeField] int connectKey;

    public override void CompletePuzzle()
    {
        LeverInteractPuzzle leverPuzzleObject = gameScene.FindInteractObject(connectKey) as LeverInteractPuzzle;
        leverPuzzleObject.OffMarble();
        base.CompletePuzzle();
    }

    public override void Load()
    {
        LeverInteractPuzzle leverPuzzleObject = gameScene.FindInteractObject(connectKey) as LeverInteractPuzzle;
        leverPuzzleObject.OffMarble();
    }
}
