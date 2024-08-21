using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SceneController : MonoBehaviour
{
    [SerializeField] List<GameObject> scenes;
    [SerializeField] float width;

    StageTable nowStageTable;
    CameraController nowCamera;
    InGameScene gameScene;
    Image blackImage;
    Color color;

    // 화면 전환 속도와 관련된 변수들
    float moveSpeed;
    float blackInSpeed = 0.13f;
    float blackOutSpeed = 0.13f;
    float blackStay = 0.27f;

    private void Awake()
    {
        blackImage = GetComponentInChildren<Image>();
        moveSpeed = blackInSpeed + blackOutSpeed + blackStay;
        nowCamera = Manager.Game.Camera;
        color = blackImage.color;
        color.a = 0;
        blackImage.color = color;
    }

    private void Start()
    {
        gameScene = Manager.Scene.GetCurScene<InGameScene>();
    }


    public void MoveLeft()
    {
        StartCoroutine(CoMove(false));
    }

    public void MoveRight()
    {
        StartCoroutine(CoMove(true));
    }

    public void SetNowStageTable(int id)
    {
        nowStageTable = Manager.Data.StageLoad(id);
    }

    IEnumerator CoMove(bool isRight)
    {
        Manager.Game.InGameUserLog.ScreenChangeCount++;
        HintManager.Inst.hint_indicator.SetActive(false);

        gameScene.SetSceneButton(false);

        List<float> startPos = new List<float>(); // 1,2,3,4 씬들의 출발지점
        List<float> endPos = new List<float>(); // 1,2,3,4 씬들의 도착지점
        foreach (GameObject scene in scenes)
        {
            startPos.Add(scene.transform.position.x);
            float endPosTmp = isRight ? scene.transform.position.x - width : scene.transform.position.x + width;
            endPos.Add(endPosTmp);
        }

        float time = 0;
        float blackTime = 0;
        while (time <= moveSpeed)
        {
            time += Time.deltaTime;
            blackTime += Time.deltaTime;

            for (int i = 0; i < scenes.Count; i++)
            {
                float tmp = Mathf.Lerp(startPos[i], endPos[i], time / moveSpeed);
                Vector3 pos = scenes[i].transform.position;
                scenes[i].transform.position = new Vector3(tmp, pos.y, pos.z);
            }

            //블랙 화면
            if (time <= blackInSpeed)
            {
                float blackTmp = Mathf.Lerp(0, 1, blackTime / blackInSpeed);
                color.a = blackTmp;
            }
            else if(time <= moveSpeed - blackOutSpeed)
            {
                if (blackTime != 0)
                    blackTime = 0;
                color.a = 1;
            }
            else
            {
                float blackTmp = Mathf.Lerp(1, 0, blackTime / blackOutSpeed);
                color.a = blackTmp;
            }

            blackImage.color = color;
            yield return null;
        }


        // 씬의 이동. (계속 뺑뺑 돌아야하기 때문)
        float cameraPosX = Manager.Game.Camera.transform.position.x;
        int count = scenes.Count;
        float maxDistance = -1f;
        int maxIndex = 0;
        for (int i = 0; i < endPos.Count; i++)
        {
            float tmpDistance = Mathf.Abs(cameraPosX - endPos[i]);
            if (maxDistance <= tmpDistance)
            {
                maxDistance = tmpDistance;
                maxIndex = i;
            }
        }
        if (isRight)
            scenes[maxIndex].transform.Translate(Vector3.right * width * count);
        else if (!isRight)
            scenes[maxIndex].transform.Translate(Vector3.left * width * count);

        gameScene.SetSceneButton(true);

        if (HintManager.Inst.is_Hint_Activated && HintManager.Inst.CheckView())
        {
            HintManager.Inst.SetHintPos();
        }
        
    }
    //배경 로드(테이블값을 가져와서)
    public IEnumerator LoadBackgound()
    {
        int[] UIs = nowStageTable.UI;
        for (int i = 0; i < UIs.Length; i++)
        {
            Sprite sprite = Manager.Resource.Load<Sprite>(UIs[i].ToString());
            scenes[i].GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            yield return null;
        }
    }


    //오브젝트들을 로드
    public IEnumerator LoadObjects()
    {
        int[] Objects = nowStageTable.Object;
        int[] Prioritys = nowStageTable.Priority;
        for (int i = 0; i < Objects.Length; i++)
        {
            InteractionObject interactionInfo = Manager.Data.InteractionObjectLoad(Objects[i]);
            GameObject loadData = Manager.Resource.Load<GameObject>(interactionInfo.ID.ToString());
            if (loadData == null)
                continue;
            GameObject insGameObject = Instantiate(loadData, scenes[interactionInfo.StageX].transform);
            insGameObject.transform.localPosition = new Vector2(0, 0.5f);

            Clue_Object iclue = insGameObject.GetComponent<Clue_Object>();


            if (iclue != null)
            {
                //정민 추가 -------------------------------------------------->
                //단서에 게임현황 검사 함수 등록.
                iclue.OnCheckFlow += GameFlowManager.Inst.CheckGameFlow;
                //단서 오브젝트에 우선 순위 할당.
                iclue.Prio = Prioritys[i];
                //단서 딕셔너리에 단서 추가.
                if (GameFlowManager.Inst.ClueDic.ContainsKey(iclue.Prio) == false)
                    GameFlowManager.Inst.ClueDic.Add(iclue.Prio, insGameObject);
                //----------------------------------------------------------->

                yield return null;
            }
            gameScene.AddInteractObject(insGameObject);
        }

        #region 중간저장
        /*
        //----> 추가 . 오브젝트 진행상황 로드 
        //오브젝트 모두 다 로드된 후에 진행상황 저장로드해야 유기적인 관계인 오브젝트들이 서로 참조할 수 있음.
        StageProcessTable stageProcessTable = Manager.Data.StageProcessTables[nowStageTable.StageProcessID];
        int[] objectID = stageProcessTable.ObjectID;
        bool[] objectIsClear = stageProcessTable.IsClear;

        if (objectID == null)
            yield break;

        for (int i = 0; i < objectID.Length; i++)
        {
            InteractObject interactObject = gameScene.FindInteractObject(objectID[i]);
            if (interactObject == null)
                continue;

            Clue_Object clue = interactObject.GetComponent<Clue_Object>();
            if (objectIsClear[i] == true)
            {
                if (clue != null)
                    interactObject.GetComponent<Clue_Object>().IsClear = objectIsClear[i];
                interactObject.Load();
            }
        }

        //타이머도 여기서 Set
        Manager.Game.Timer.SetTimer(stageProcessTable.Timer);
        //-------------------------------------
        */
        #endregion
    }

    public IEnumerator InventorySet()
    {
        int[] startItems = nowStageTable.Item;
        Inventory inven = Manager.Game.Inven;

        #region 중간저장
        /*
        int[] inventoryItems = Manager.Data.InventoryTables[nowStageTable.InventoryID].slot;
        if (inventoryItems != null && inventoryItems.Length != 0)
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                ItemTable itemdata = Manager.Data.ItemTables[inventoryItems[i]];
                Item item = new Item(itemdata);
                inven.AddItem(item);
                yield return null;
            }
        }
        */
        #endregion

        // 시작아이템을 줘야함.
        if (startItems != null)
        {
            for (int i = 0; i < startItems.Length; i++)
            {
                ItemTable itemdata = Manager.Data.ItemTables[startItems[i]];
                Item item = new Item(itemdata);
                inven.AddItem(item);
                yield return null;
            }
        }

        inven.BoxBundleCreate();
        inven.SetInven();
    }
}
