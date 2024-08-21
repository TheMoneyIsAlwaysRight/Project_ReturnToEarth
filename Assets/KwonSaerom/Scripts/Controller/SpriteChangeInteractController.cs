using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangeInteractController : InteractObject
{
    [Space(Define.INSPECTOR_SPACE)]
    [SerializeField] Sprite changeSprite;
    // 서랍 문을 열면 나오는 상호작용 오브젝트의 ID
    [SerializeField] int useItemID;

    private SpriteRenderer targetBackground;
    private bool isInteract = false;

    public override void Load()
    {
        gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        //배경 구하기
        SpriteRenderer[] spriteRenders = gameObject.transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer>();
        LayerMask layer = LayerMask.GetMask("Background");
        foreach (SpriteRenderer spriteRender in spriteRenders)
        {
            if (Extension.Contain(layer, spriteRender.gameObject.layer))
            {
                targetBackground = spriteRender;
            }
        }
    }


    public override void Interact()
    {
        if (isInteract) //이미 상호작용했으면 return.
            return;
         
        Item equipItem = ItemManager.Instance.GetEquipItem(); //GetEquipItem()는 firstClickItem을 불러옴. (그게장착)
        if (equipItem == null || equipItem.ItemData.ID != useItemID)
        {
            Close();
            return;
        }

        if (equipItem.ItemData.Permanent == false)
        {
            Manager.Game.Inven.RemoveItem(equipItem);
            Manager.Game.Inven.SetInven();
        }

        GetComponent<Clue_Object>().IsClear = true;
        targetBackground.sprite = changeSprite;
        isInteract = true;
        Close();
    }


    public override void Close()
    {
        base.Close();
        InGameScene gameScene = Manager.Scene.GetCurScene() as InGameScene;
        Manager.Game.Inter.CloseInteract();
    }
}
