using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// 인벤토리에 있는 인벤토리를 사용해서 상호작용하는 상호작용 오브젝트 컨트롤러
/// 그냥 클릭만해서 아이템이 뿅 나오는 기능도 가능하다. => useItemID를 -1로 설정.
/// </summary>
public class ItemUseInteractController : InteractObject
{
    [SerializeField] bool initActive;
    private bool isInteract = false;

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(initActive);
    }

    public override void Interact()
    {
        if (isInteract) //이미 상호작용했으면 return.
            return;

        if (IsPassLastObject() == false)
            return;

        //요구되는 아이템 갯수가 많으면, 장착 아이템을 검사하는게 아니라 그 아이템들을 다 들고있는지 검사해야한다.
        
        if(info.RequiredID.Length > 1)
        {
            Inventory inventory = Manager.Game.Inven;
            for (int i=0;i< info.RequiredID.Length;i++)
            {
                if (inventory.IsGetItem(info.RequiredID[i]) == false)
                {
                    UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
                    popup.SetLog(info.interactText);
                    return;
                }
            }

            //모든 아이템이 있으면, 모두 삭제
            for (int i = 0; i < info.RequiredID.Length; i++)
            {
                ItemTable itemData = Manager.Data.ItemTables[info.RequiredID[i]];
                if (itemData.Permanent == false)
                {
                    Manager.Game.Inven.RemoveItem(info.RequiredID[i]);
                    Manager.Game.Inven.SetInven();
                    Manager.Game.Inven.BoxBundleCreate();
                }
            }
        }
        else if(info.RequiredID.Length == 1)
        {
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
                Manager.Game.Inven.BoxBundleCreate();
                ItemManager.Instance.ClickedReset();
            }
        }

        isInteract = true;

        //권새롬 추가 --> 튜토리얼시에는 로그가 떠야함
        GameTutorial tuto = GetComponent<GameTutorial>();
        if (tuto != null)
        {
            GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
            tuto.TutorialLog();
        }

        InteractObjectItem();
        gameObject.SetActive(false);
    }

    public override void Load()
    {
        InteractObjectItem(true);
        gameObject.SetActive(false);
        isInteract = true;
    }
}
