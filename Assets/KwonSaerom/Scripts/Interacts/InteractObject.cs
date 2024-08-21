using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour, IInteractable
{
    [Header("이 오브젝트의 작동 전 풀어야할 오브젝트가 있으면 ID 입력.")]
    [SerializeField] string lastObjectKey;

    protected int interactId;
    public int InteractID { get { return interactId; } }

    protected InteractionObject info;
    protected InteractController inter;
    protected UI_Log logUI;
    protected InGameScene gameScene;
    private List<InteractObject> lastObjects;

    protected virtual void Awake()
    {
        interactId = int.Parse(gameObject.name.Substring(0, 6));
        info = Manager.Data.InteractionObjects[interactId];
        gameScene = Manager.Scene.GetCurScene() as InGameScene;
    }

    protected virtual void Start()
    {
        inter = Manager.Game.Inter;
        logUI = Manager.Resource.Load<UI_Log>("UI_Log");
    }


    public abstract void Interact();
    public abstract void Load();

    /// <summary>
    /// 해당 상호작용 오브젝트가 해결되었을때 활성화되는 오브젝트 검사
    /// </summary>
    protected void InteractObjectItem(bool isLoad = false)
    {
        // 얘로인해서 활성화 되는 아이템이 있다면
        if (info.RelationID != null)
        {
            foreach (int relation in info.RelationID)
            {
                int code = relation / 1000; // 107 : 활성화 아이템, 106 : 얻어야하는 아이템
                switch (code)
                {
                    case 107:
                        ActiveObject(relation);
                        break;
                    case 106:
                        if (isLoad == true) //로드중이면 아이템 먹으면안됨
                            return;
                        GetItem(relation);
                        break;
                    default:
                        break;
                }
            }
        }
        Clue_Object clue = GetComponent<Clue_Object>();
        if (clue != null)
        {
            Manager.Game.InGameUserLog.ObjectClearSequence.Add(interactId);
            clue.IsClear = true;
        }

        ClearConnectObject connectUnActive = GetComponent<ClearConnectObject>();
        if (connectUnActive != null)
            connectUnActive.UnActive();
    }


    protected bool IsPassLastObject()
    {
        if (lastObjectKey != "" && lastObjectKey != "0")
        {
            string[] keyList = lastObjectKey.Split(",");
            // 오브젝트들 초기화
            if (lastObjects == null)
            {
                lastObjects = new List<InteractObject>();
                for (int i = 0; i < keyList.Length; i++)
                    lastObjects.Add(null);
            }

            // 오브젝트별로 검사하기
            for (int i = 0; i < keyList.Length; i++)
            {
                int key = int.Parse(keyList[i]);
                if (lastObjects[i] == null)
                    lastObjects[i] = gameScene.FindInteractObject(key);

                //lastObject가 null인경우 : 아직 start로 초기화가 안되었다(활성화 안됨) : 즉, 문제를 풀지못했다.
                if (lastObjects[i] == null || lastObjects[i].gameObject.GetComponent<Clue_Object>().IsClear == false)
                {
                    UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
                    if (Manager.Data.TextBundleTables.ContainsKey(info.interactText))
                    {
                        TextBundleTable tt = Manager.Data.TextBundleTables[info.interactText];
                        popup.SetLog(tt.ID);

                    }
                    else
                        popup.SetLog(102243); //"앞의 퍼즐들을 해결하지 못했어."
                    return false;
                }
            }
        }
        return true;
    }

    private void ActiveObject(int activeID)
    {
        List<InteractObject> interactables = gameScene.InteractObjects;
        InteractObject targetItem = null;

        foreach (InteractObject interactable in interactables)
        {
            if (interactable.InteractID == activeID)
            {
                targetItem = interactable;
            }
        }

        if (targetItem != null)
        {
            Clue_Object clue = targetItem.GetComponent<Clue_Object>();
            if (clue != null && clue.IsClear == true)
                return;
            targetItem.gameObject.SetActive(true);
            inter.InteractChange(targetItem);
        }
    }

    private void GetItem(int itemID)
    {
        Inventory inventory = Manager.Game.Inven;
        // 1. 인벤토리 저장 코드 입력

        //아이템 생성
        ItemTable itemData = Manager.Data.ItemTables[itemID];
        Item item = new Item(itemData);

        inventory.AddItem(item);
        inventory.BoxBundleCreate(); // 찬규 추가 : 인벤토리 박스 생성 및 삭제
        inventory.SetInven();
        gameObject.SetActive(false);
        Close();
    }

    public virtual void Close()
    {

    }

}
