using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ItemUsePuzzleActive : PuzzleActiveInteract
{
    public override void PuzzleOnInteract()
    {
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


        //박정민 코드 참고쓰---------------------------------------------------------------
        if (!isComplete)
        {
            PuzzleManager.Inst.PlayPuzzle(puzzleNameKey);
            clue_Object.IsClear = false;
        }
        //------------------------------------------------------------------------
    }
}
