using UnityEngine;
using UnityEngine.Events;

public class Clicker_Clue_Object : CameraInteractController
{
    public override void Load()
    {

    }

    public override void PuzzleOnInteract()
    {
        Manager.Game.Camera.CurrentCamState = CameraController.CameraState.Base;

        //---------------------------------Cut Here----------------------------------------------------
        if (Manager.Game.Inter == null)
        {
            Manager.Game.Inter = GameObject.Find("TouchController").GetComponent<InteractController>();
        }
        //---------------------------------------------------------------------------------------------

        Manager.Game.Inter.CloseInteract();
    }
}
