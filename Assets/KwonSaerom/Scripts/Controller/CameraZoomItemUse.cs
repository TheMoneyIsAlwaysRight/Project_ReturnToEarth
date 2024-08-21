using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomItemUse : CameraInteractController
{
    [SerializeField] bool initActive;
    private bool isInteract = false;

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(initActive);
    }


    public override void PuzzleOnInteract()
    {
        if (isInteract) //이미 상호작용했으면 return.
            return;

        if (IsPassLastObject() == false)
            return;

        Item equipItem = ItemManager.Instance.GetEquipItem(); //GetEquipItem()는 firstClickItem을 불러옴. (그게장착)
        if (equipItem == null || equipItem.ItemData.ID != info.RequiredID[0])
        {
            UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
            popup.SetLog(info.interactText);
            return;
        }

        if (equipItem.ItemData.Permanent == false)
        {
            Manager.Game.Inven.RemoveItem(equipItem);
            Manager.Game.Inven.SetInven();
        }

        InteractObjectItem();
        gameObject.SetActive(false);
        isInteract = true;
    }

    public override void Load()
    {
        InteractObjectItem(true);
        gameObject.SetActive(false);
        isInteract = true;
    }
}
