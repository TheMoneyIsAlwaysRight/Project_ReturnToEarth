using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Active_Controller : CameraInteractController
{
    [SerializeField] GameObject puzzle_Prefab;
    public override void PuzzleOnInteract()
    {
        if (puzzle_Prefab != null) 
        {
            puzzle_Prefab.gameObject.SetActive(true);
            
        }       
    }
}
