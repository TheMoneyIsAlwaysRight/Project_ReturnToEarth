using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] RectTransform boxBundle;       // 생성 될 인벤박스를 4개씩 묶어놓은 묶음
    [SerializeField] RectTransform content;         // 박스 번들이 생성 될 컨텐트

    [SerializeField] Stack<RectTransform> bundles;  // 생성 된 박스 번들을 관리할 스택
    [SerializeField] List<InventoryBox> inventoryBoxes; // 박스 번들 내부의 인벤박스 리스트
    [SerializeField] List<Item> itemList; // 현재 아이템 리스트

    [SerializeField] int itemCount;
    [SerializeField] int bundleCount;

    public List<InventoryBox> InventoryBoxes { get { return inventoryBoxes; } set { inventoryBoxes = value; } }
    public List<Item> ItemList { get { return itemList; } set { itemList = value; } }

    public int ItemCount { get { return itemCount; } set { itemCount = value; } }

    // ===========캐시===========

    float _contentSize = -1;
    public float contentSize
    {
        get
        {
            if (_contentSize == -1)
            {
                _contentSize = content.sizeDelta.x;
            }
            return _contentSize;
        }
    }
    // ===========================

    private void Awake()
    {
        CreateList();
        BoxBundleCreate();
        Manager.Game.Inven = this; //권새롬 추가 : 게임매니저에 인벤토리 할당   
    }


    public void BoxBundleCreate()
    {
        itemCount = itemList.Count;

        if (itemCount < 0)
            itemCount = 0;

        if (itemCount <= 4)
        {
            if (bundleCount == 0)
            {
                bundleCount++;
                content.sizeDelta = new Vector2(contentSize * bundleCount, content.sizeDelta.y);

                RectTransform rt = Instantiate(boxBundle, content.transform);
                InventoryBox[] _boxes = rt.GetComponentsInChildren<InventoryBox>();
                foreach (InventoryBox _box in _boxes)
                {
                    inventoryBoxes.Add(_box);
                }
                bundles.Push(rt);
            }
            else if (bundleCount == 2)
            {
                bundleCount--;
                content.sizeDelta = new Vector2(contentSize * bundleCount, content.sizeDelta.y);

                RectTransform rt = bundles.Pop();   // 박스 번들 스택 팝

                inventoryBoxes.RemoveAt(4);
                inventoryBoxes.RemoveAt(4);
                inventoryBoxes.RemoveAt(4);
                inventoryBoxes.RemoveAt(4);

                Destroy(rt.gameObject);
            }
        }
        else if (4 < itemCount && itemCount <= 8)
        {
            if (bundleCount == 1)
            {
                bundleCount++;
                content.sizeDelta = new Vector2(contentSize * bundleCount, content.sizeDelta.y);

                RectTransform rt = Instantiate(boxBundle, content.transform);
                InventoryBox[] _boxes = rt.GetComponentsInChildren<InventoryBox>();
                foreach (InventoryBox _box in _boxes)
                {
                    inventoryBoxes.Add(_box);
                }
                bundles.Push(rt);
            }
            else if (bundleCount == 3)
            {
                bundleCount--;
                content.sizeDelta = new Vector2(contentSize * bundleCount, content.sizeDelta.y);

                RectTransform rt = bundles.Pop();

                inventoryBoxes.RemoveAt(8);
                inventoryBoxes.RemoveAt(8);
                inventoryBoxes.RemoveAt(8);
                inventoryBoxes.RemoveAt(8);

                Destroy(rt.gameObject);
            }
        }
        else if (8 < itemCount && itemCount <= 12)
        {
            if (bundleCount == 2)
            {
                bundleCount++;
                content.sizeDelta = new Vector2(contentSize * bundleCount, content.sizeDelta.y);

                RectTransform rt = Instantiate(boxBundle, content.transform);
                InventoryBox[] _boxes = rt.GetComponentsInChildren<InventoryBox>();
                foreach (InventoryBox _box in _boxes)
                {
                    inventoryBoxes.Add(_box);
                }
                bundles.Push(rt);
            }
        }
    }

    public void CreateList()
    {
        inventoryBoxes = new List<InventoryBox>();
        bundles = new Stack<RectTransform>();
        itemList = new List<Item>();
        itemCount = 0;
        bundleCount = 0;
    }

    // 아이템 리스트에 아이템을 추가하는 함수
    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    // 아이템 리스트에 아이템을 중간에 넣는 함수
    public void InsertItem(Item item, int index)
    {
        itemList.Insert(index, item);
    }

    // 해당 아이템을 리스트에서 제거하는 함수
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }

    //권새롬 추가 : key값으로 아이템을 지우는 함수
    public void RemoveItem(int key)
    {
        for(int i=0;i<itemList.Count;i++)
        {
            if (itemList[i].ItemData.ID == key)
            {
                RemoveItem(itemList[i]);
                return;
            }
        }
        Debug.Log("해당하는 아이템이 없다. : 삭제불가");
    }
    //권새롬 추가 : 해당 key값을 가진 아이템이 인벤토리에 있는지 알려주는 함수
    public bool IsGetItem(int key)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ItemData.ID == key)
            {
                return true;
            }
        }
        return false;
    }
    //-----------------------------------------------

    // 해당 아이템이 몇 번째 인덱스인지 구하는 함수
    public int CheckIndex(Item item)
    {
        int result = 0;

        result = itemList.IndexOf(item);

        return result;
    }

    // 인벤토리 싹 비우는 함수
    public void ResetInven()
    {
        for (int i = 0; i < inventoryBoxes.Count; i++)
        {
            inventoryBoxes[i].ResetBox();
        }
    }

    // 아이템 리스트를 토대로 인벤토리 박스를 세팅하는 함수
    public void SetInven()
    {
        ResetInven();

        for (int i = 0; i < itemList.Count; i++)
        {
            inventoryBoxes[i].SetItem(itemList[i]);
        }

        itemCount = itemList.Count;
    }
}
