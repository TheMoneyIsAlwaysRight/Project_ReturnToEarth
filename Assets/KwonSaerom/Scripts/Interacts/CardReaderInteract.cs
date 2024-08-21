using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CardReaderInteract : CameraInteractController
{
    [Space(Define.INSPECTOR_SPACE)]
    [SerializeField] int useItemID;
    [SerializeField] int openDoorObjectID;

    Clue_Object clue_Object;
    InteractObject lastObject;
    private bool onCardTag = false;

    protected override void Start()
    {
       base.Start();
       gameScene = Manager.Scene.GetCurScene() as InGameScene;
       clue_Object = GetComponent<Clue_Object>();
    }

    

    public override void PuzzleOnInteract()
    {
        if (onCardTag)
        {
            UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
            popup.SetLog(102242); //"이미 문을 열었다"

            return;
        }

        Item equipItem = ItemManager.Instance.GetEquipItem(); //GetEquipItem()는 firstClickItem을 불러옴. (그게장착)
        if (equipItem == null || equipItem.ItemData.ID != useItemID)
        {
            UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
            popup.SetLog(102171); // 하드코딩. -> 텍스트번들 다 나오면 수정해야함
            return;
        }

        //카드를 잘 장착하고 리더기를 눌렀을때.
        if (IsPassLastObject() == false)
            return;

        onCardTag = true;

        InteractObject doorObject = gameScene.FindInteractObject(openDoorObjectID);
        DoorOpenInteract door = doorObject as DoorOpenInteract;
        door.Open();

        clue_Object.IsClear = true;
        Close();
    }

    public override void Load()
    {
        onCardTag = true;
        InteractObject doorObject = gameScene.FindInteractObject(openDoorObjectID);
        DoorOpenInteract door = doorObject as DoorOpenInteract;
        door.Open();
    }

}
