using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinusItemUseInteract : InteractObject
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] int[] brokenCount;
    [SerializeField] SpriteRenderer interactRenderer;

    private bool isEquip = false;
    private int nowCount;


    private void Update()
    {
        if(isEquip)
        {
            IInteractable interactable = inter.NowInteract;
            if (interactable as ContinusItemUseInteract == null)
                isEquip = false;
        }

    }
    public override void Interact()
    {
        if(isEquip == false)
        {
            Item equipItem = ItemManager.Instance.GetEquipItem(); //GetEquipItem()는 firstClickItem을 불러옴. (그게장착)
            if (equipItem == null || equipItem.ItemData.ID != info.RequiredID[0])
            {
                UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
                popup.SetLog(info.interactText);
                return;
            }
            isEquip = true;
        }

        nowCount++;
        for(int i=0;i<brokenCount.Length;i++)
        {
            if (nowCount == brokenCount[i])
                interactRenderer.sprite = sprites[i];
        }

        //다 부서지면
        if(nowCount == brokenCount[brokenCount.Length-1])
        {
            InteractObjectItem();
            gameObject.SetActive(false);
        }
    }

    public override void Load()
    {
        InteractObjectItem(true);
        gameObject.SetActive(false);
    }
}
