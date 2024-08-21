using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBox : MonoBehaviour
{
    [SerializeField] Image uiIcon;
    [SerializeField] Image bg;

    [SerializeField] Sprite tmpSprite;

    bool isClicked = false;     // 현재 클릭 된 상태인지 아닌지
    bool isMerged = false;

    public bool IsClicked { get { return isClicked; } set { isClicked = value; } }
    public bool IsMerged { get { return isMerged; } set { isMerged = value; } }
    public Image BG { get { return bg; } set { bg = value; } }

    Item item = null; //권새롬 수정. -> 초기에 null로 초기화
    public Item Item { get { return item; } set { item = value; } }

    // 박스에 아이템 세팅
    public void SetItem(Item item)
    {
        this.item = item;
        // 어드레서블로 이미지 바꾸기
        ItemTable _data = item.ItemData;
        uiIcon.sprite = Manager.Resource.Load<Sprite>(_data.Resource.ToString());
    }

    // 박스 클릭 시 불러올 함수
    public void ClickBox()
    {
        if (item == null)
            return;


        // 아이템의 클릭함수 추가
        ItemManager.Instance.Clicked(item);

        if (isMerged)
        {
            Debug.Log("머지했음");
            ItemManager.Instance.AllBoxIsMergedSet(false);
            return;
        }

        // 인벤박스의 색 변경을 위한 기능
        ItemManager.Instance.ClickedBox = this;
        ItemManager.Instance.AllBoxIsClickedSet();
        ItemManager.Instance.ClickedBox.isClicked = true;
        ItemManager.Instance.BoxColor();
    }

    /* 인벤토리의 배경색이 바뀌는 경우
     * 1. 그냥 클릭 했을 때 :        흰 -> 초    완료
     * 2. 더블 클릭 했을 때 :        초록 유지   완료
     * 3. 다른 칸 클릭 했을 때 :     초 -> 흰    완료
     * =============================================
     * 4. 다른 곳을 클릭 했을 때 :   초 -> 흰    완료
     * =============================================
     * 5. 조합 할 때 :              초 -> 흰
     * 6. 분해 할 때 :              초 -> 흰
     */

    // 클릭 할 때 마다 모든 인벤박스의 색을 하얀색으로 만들어주고 
    // 그 후에 아이템매니저에 클릭된 박스만 배경색을 초록색으로 해준다?

    // 박스 초기화 함수
    public void ResetBox()
    {
        item = null;
        uiIcon.sprite = tmpSprite;
    }
}
