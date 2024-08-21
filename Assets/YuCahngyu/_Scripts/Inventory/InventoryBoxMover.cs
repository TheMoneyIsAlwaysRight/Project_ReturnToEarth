using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBoxMover : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] RectTransform box;         // 생성될 박스
    [SerializeField] RectTransform content;        // 박스가 생성 될 공간

    [SerializeField, Range(1, 12)] int boxCount;    // 생성 될 박스의 개수

    [SerializeField] private float offset;

    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();  // 혹시 필요할지도 모르는 유니티 이벤트

    LinkedList<RectTransform> boxList;              // 박스의 연결리스트    

    protected float diffPos = 0;                    // 포지션의 거리차이
    protected int curItemNum = 0;                   // 현재 아이템칸의 넘버

    //===============캐시================
    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null) _rectTransform = content.GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    private float _boxScale = -1;
    public float boxScale                       // 아이템 박스의 스케일
    {
        get
        {
            if (box != null && _boxScale == -1)
            {
                _boxScale = box.sizeDelta.x;
            }
            return _boxScale;
        }
    }

    private float anchoredPosition
    {
        get
        {
            return rectTransform.anchoredPosition.x;
        }
    }


    //===============캐시================


    private void Start()
    {
        boxList = new LinkedList<RectTransform>();      // 연결리스트 초기화
        inventory.CreateList();
        CreateBox();
    }

    private void Update()
    {
        CheckMove();
    }

    public void CreateBox()         // 박스 생성
    {
        for (int i = 0; i < boxCount; i++)
        {
            RectTransform _box = Instantiate(box, content.transform);
            _box.name = "box - " + i.ToString();
            _box.anchoredPosition = new Vector2(87.5f + boxScale * i, 0);
            boxList.AddLast(_box);
            InventoryBox _invenBox = _box.GetComponentInChildren<InventoryBox>();
            inventory.InventoryBoxes.Add(_invenBox);
        }
    }

    public void CheckMove()         // 박스 이동
    {
        if (boxList.First == null)
        {
            return;
        }

        while (anchoredPosition - diffPos < -boxScale * 2)
        {
            diffPos -= boxScale;

            var item = boxList.First.Value;
            boxList.RemoveFirst();
            boxList.AddLast(item);

            var pos = boxScale * boxCount + boxScale * curItemNum;
            item.anchoredPosition = new Vector2(pos, item.anchoredPosition.y);

            onUpdateItem.Invoke(curItemNum + boxCount, item.gameObject);

            curItemNum++;
        }

        while (anchoredPosition - diffPos > 0)
        {
            diffPos += boxScale;

            var item = boxList.Last.Value;
            boxList.RemoveLast();
            boxList.AddFirst(item);

            curItemNum--;

            var pos = boxScale * curItemNum;
            item.anchoredPosition = new Vector2(pos, item.anchoredPosition.y);
            onUpdateItem.Invoke(curItemNum, item.gameObject);
        }
    }
}

[System.Serializable]
public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
