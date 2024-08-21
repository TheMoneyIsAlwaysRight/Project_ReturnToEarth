using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : BaseUI
{
    private Item item;
    enum GameObjects
    {
        ItemImage,
        InteractBtn,
        ItemName,
        ItemInfo
    }

    public override void LocalUpdate()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        GetUI<Button>(GameObjects.InteractBtn.ToString()).onClick.AddListener(() =>
        {
            ItemManager.Instance.BreakItem(item);
        });
    }

    public void SetItem(Item item)
    {
        this.item = item;
        SetInfo();
    }

    private void SetInfo()
    {
        UITable uiTable = Manager.Data.UITables[item.ItemData.UI];
        TextBundleTable textBundleTable = Manager.Data.TextBundleTables[uiTable.Text];
        
        Sprite itemSprite = Manager.Resource.Load<Sprite>(uiTable.Img[0].ToString());
        GetUI<Image>(GameObjects.ItemImage.ToString()).sprite = itemSprite;

        GetUI<TMP_Text>(GameObjects.ItemName.ToString()).text = LanguageSetting.GetLocaleText(textBundleTable.TEXT[0]);
        GetUI<TMP_Text>(GameObjects.ItemInfo.ToString()).text = LanguageSetting.GetLocaleText(textBundleTable.TEXT[1]);

    }

}
