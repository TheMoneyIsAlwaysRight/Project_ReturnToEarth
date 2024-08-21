using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTokenTest : MonoBehaviour
{
    [SerializeField] int id;
    Item item;

    private void Start()
    {
        item = new Item(Manager.Data.ItemTables[id]);
    }


    public void SelectItem()
    {
        ItemManager.Instance.Clicked(item);
    }


}
