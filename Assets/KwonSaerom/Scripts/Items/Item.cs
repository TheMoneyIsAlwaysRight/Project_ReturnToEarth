using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
/// <summary>
/// use(아이템 사용) 
/// </summary>
public class Item
{
    private ItemTable itemData; // id 를 조회해 ItemTable 데이터를 할당해준다.

    public ItemTable ItemData { get { return itemData; } }

    public Item(ItemTable itemData)
    {
        this.itemData = itemData;
    }
}