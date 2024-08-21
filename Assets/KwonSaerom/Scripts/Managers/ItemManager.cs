using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] UI_ItemInfo itemInfoUI;
    [SerializeField] InventoryBox clickedBox;       // 찬규 추가 : 현재 클릭한 박스
    private Item firstClickItem;
    private Item secondClickItem;

    public Inventory Inventory { get { return inventory; } set { inventory = value; } }             // 찬규 추가 : get set 프로퍼티
    public InventoryBox ClickedBox { get { return clickedBox; } set { clickedBox = value; } }       // 찬규 추가 : get set 프로퍼티

    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (itemInfoUI == null)
            itemInfoUI = GetComponentInChildren<UI_ItemInfo>();
        itemInfoUI.gameObject.SetActive(false);
    }

    public Item GetEquipItem()
    {
        return firstClickItem;
    }

    public void Clicked(Item item)
    {
        if (firstClickItem == null)
            firstClickItem = item;
        else
        {
            secondClickItem = item;
            ItemInteract();
        }
    }

    public void ClickedReset()
    {
        if (itemInfoUI.gameObject.activeSelf)
            itemInfoUI.gameObject.SetActive(false);

        if (ClickedBox != null)
            ClickedBox.IsClicked = false;  // 찬규 추가 : 선택한 박스의 색 변경을 위함
        ClickedBox = null;  // 찬규 추가 : 선택한 박스의 색 변경을 위함
        AllBoxIsClickedSet();   // 찬규 추가 : 선택한 박스의 색 변경을 위함
        BoxColor();         // 찬규 추가 : 선택한 박스의 색 변경을 위함

        firstClickItem = null;
        secondClickItem = null;
    }

    //아이템이 상호작용하는 함수. -> UI를 띄우던가(이미지), 분해, 조합, 사용 등
    private void ItemInteract()
    {
        if (firstClickItem == secondClickItem)
            ZoomInItem();
        else
            MergeItems();
    }


    private void ZoomInItem()
    {
        itemInfoUI.gameObject.SetActive(true);
        itemInfoUI.SetItem(firstClickItem);
    }

    private void MergeItems()
    {
        AllBoxIsMergedSet(true);    // 찬규 추가 : 선택한 박스의 색 변경을 위함

        // 두개가 서로 조합할 수 있는 아이템인지 검사하기. 
        Merge(firstClickItem, secondClickItem);

        AllBoxIsClickedSet();   // 찬규 추가 : 선택한 박스의 색 변경을 위함
        BoxColor();         // 찬규 추가 : 선택한 박스의 색 변경을 위함
    }


    public void BreakItem(Item item)
    {
        // 분해
        int[] breakItems = item.ItemData.RelationItem;

        // 분해 ID값이 null이면 -> 그냥 UI 꺼짐./ null이 아닐때 동작한다.
        if (breakItems != null && breakItems.Length > 0)
        {
            int index = inventory.CheckIndex(item);

            // 1. 지금 해당 Item 을 인벤토리에서 Remove하고
            inventory.RemoveItem(item);

            // 2. 분해된 아이템을 생성한다.
            for (int i = breakItems.Length - 1; i >= 0; i--)
            {
                Item tokenItem = new Item(Manager.Data.ItemTables[breakItems[i]]);
                // 3. 이 아이템들을 인벤토리에 Add한다.
                inventory.InsertItem(tokenItem, index);
            }
        }

        inventory.BoxBundleCreate();
        inventory.SetInven();
        //하고 UI를 닫는다.
        ClickedReset();
    }

    public void Merge(Item item, Item anotherItem)
    {
        int[] requiredItems = item.ItemData.RequiredItem;
        int[] results = item.ItemData.Result;
        int index = -1;
        if (requiredItems == null || results == null)
        {
            firstClickItem = secondClickItem;
            secondClickItem = null;
            AllBoxIsMergedSet(false);       // 찬규 추가 : 선택한 박스의 색 변경을 위함
            return;
        }
        for (int i = 0; i < requiredItems.Length; i++)
        {
            if (requiredItems[i] == anotherItem.ItemData.ID)
            {
                index = i;
                break;
            }
        }

        //조합 불가능
        if(index == -1)
        {
            firstClickItem = secondClickItem;
            secondClickItem = null;
            return;
        }

        //조합 가능하니, 조합한 결과값을 인벤토리에 넣어준다.
        // ***** 찬규 추가 *****
        int tmpIndex = 0;
        int itemIndex = inventory.CheckIndex(item);
        int anotherItemIndex = inventory.CheckIndex(anotherItem);

        if (itemIndex < anotherItemIndex)
            tmpIndex = itemIndex;
        else
            tmpIndex = anotherItemIndex;
        // ********************

        // 1. 지금 해당 Item을 인벤토리에서 Remove하고
        inventory.RemoveItem(item);
        inventory.RemoveItem(anotherItem);

        // 2. 조합한 아이템을 생성한다.
        Item result = new Item(Manager.Data.ItemTables[results[index]]);
        Debug.Log(result);

        // 3. 이 아이템을 인벤토리에 Add 한다.
        if (inventory.ItemList.Count <= 0)
            inventory.AddItem(result);
        else
            inventory.InsertItem(result, tmpIndex);

        inventory.BoxBundleCreate();
        ClickedReset();
        inventory.SetInven();
    }


    // ======================== 찬규 추가 =================================
    // 모든 박스의 isClicked를 false 로 하는 함수
    public void AllBoxIsClickedSet()
    {
        int boxesCount = Instance.Inventory.InventoryBoxes.Count;
        for (int i = 0; i < boxesCount; i++)
        {
            Instance.Inventory.InventoryBoxes[i].IsClicked = false;
        }
    }

    public void AllBoxIsMergedSet(bool a)
    {
        int boxesCount = Instance.Inventory.InventoryBoxes.Count;
        for (int i = 0; i < boxesCount; i++)
        {
            Instance.Inventory.InventoryBoxes[i].IsMerged = a;
        }
    }

    // 박스 클릭 시 배경색 바꿔 줄 함수
    public void BoxColor()
    {
        int boxesCount = Instance.Inventory.InventoryBoxes.Count;
        for (int i = 0; i < boxesCount; i++)
        {
            if (Instance.Inventory.InventoryBoxes[i].IsClicked)
            {
                Instance.Inventory.InventoryBoxes[i].BG.color = Extension.HexColor("#F2FF88");
            }
            else
            {
                Instance.Inventory.InventoryBoxes[i].BG.color = Extension.HexColor("#FFFFFF");
            }
        }
    }
    // ===================================================================
}
