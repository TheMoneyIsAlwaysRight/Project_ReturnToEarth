using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScene : BaseScene
{
    [SerializeField] SceneController sceneController;
    [SerializeField] UI_Script scriptUI;
    [SerializeField] Button backBtn;
    [SerializeField] GameObject moveBtns;
    [SerializeField] AssetBundleManager assetbundleManager;
    [SerializeField] GameObject DevModeManager;

    private List<InteractObject> interactObjects; // 권새롬 추가 --> 상호작용 오브젝트들 다 저장해놓음.
    public List<InteractObject> InteractObjects { get { return interactObjects; } }
    public UI_Script ScriptUI { get { return scriptUI; } }

    [Header("개발자 모드 : 빌드 전 체크할 것")]
    [SerializeField] bool isTest = false;

    private void Awake()
    {
        SetBackButton(false);
        interactObjects = new List<InteractObject>();
        iD = Manager.Game.CurStageKey;
        if (isTest)
            StartCoroutine(LoadingRoutine());
    }

    private void Start()
    {
        Manager.Sound.StopBGM();
        Manager.Sound.PlayBGM("InGame");

        Debug.Log("팝업 생성 전");
        UI_Script ui_script = Manager.UI.ShowPopUpUI(scriptUI);
        Debug.Log("팝업 생성 후");
        ui_script.CharacterScripts.Execute(true);
        Debug.Log("대사창 실행 후");
    }

    public override IEnumerator LoadingRoutine()
    {
        #region addressable load
        //Manager.Resource.LoadAllAsync<Object>(iD.ToString(), (key,count,totalCount) =>
        //{
        //    Debug.Log($"{count}/{totalCount}");
        //});
        //yield return new WaitForSecondsRealtime(1f);

        //while(true)
        //{
        //    List<AsyncOperationHandle> handles = Manager.Resource.Handles;
        //    bool isDone = true;
        //    foreach(AsyncOperationHandle handle in handles)
        //    {
        //        if(handle.IsDone == false)
        //        {
        //            isDone = false;
        //            break;
        //        }
        //    }
        //    if (isDone == true)
        //        break;
        //    yield return null;
        //}
        #endregion

        if (isTest)
        {
            AssetBundleManager am = Instantiate(assetbundleManager);
            yield return am.DevMode();
        }

        //퍼즐 수정
        GameObject puzzleCanvasPrefab = Manager.Resource.Load<GameObject>("UI_Puzzle");
        GameObject puzzleCanvas = Instantiate(puzzleCanvasPrefab);
        puzzleCanvas.gameObject.SetActive(true);
        PuzzleManager.Inst.SetPuzzle(puzzleCanvas);

        sceneController.SetNowStageTable(iD);
        yield return sceneController.LoadBackgound();
        Debug.Log("배경 로드 완료");
        yield return sceneController.LoadObjects();
        Debug.Log("오브젝트,타이머 로드 완료");
        yield return sceneController.InventorySet();
        Debug.Log("인벤토리 로드 완료");

        //유저로그
        Manager.Game.InGameUserLog.StageID = Manager.Game.CurStageKey;

        // 유저로그 - 몇회차인지 체크
        BackendGameData.Instance.ChangeRoundStage(iD);
        BackendGameData.Instance.GameDataGet();
        int stageNum = iD % 100;   // 스테이지가 100개가 넘어가면 바꿔야함
        int stageRound = BackendGameData.userData.RoundStage[stageNum - 1];
        Manager.Game.InGameUserLog.Rounds = stageRound;
    }

    /// <summary>
    /// true 면 -> 뒤로가기 버튼이 활성화되고, 이동 버튼이 비활성화됨.
    /// </summary>
    /// <param name="isInteract"></param>
    public void SetBackButton(bool isInteract)
    {
        if (backBtn == null)
            return;
        backBtn.gameObject.SetActive(isInteract);
        moveBtns.SetActive(!isInteract);
    }

    public void SetButtons(bool isInteract)
    {
        if (backBtn == null)
            return;
        backBtn.gameObject.SetActive(isInteract);
        moveBtns.SetActive(isInteract);
    }

    public void SetSceneButton(bool isInteract)
    {
        //양쪽 화살표버튼 활성화 하고싶어하는데, back버튼이 활성화 되어있으면 활성화 하지 않는다.
        if (isInteract == true && backBtn.gameObject.activeSelf == true)
            return;
        moveBtns.SetActive(isInteract);
    }

    // 권새롬추가 : 상호작용 오브젝트 List 저장, Find 함수들
    public void AddInteractObject(GameObject inter)
    {
        InteractObject interactObject = inter.GetComponent<InteractObject>();
        if (interactObject != null)
            interactObjects.Add(interactObject);
    }

    public InteractObject FindInteractObject(int key)
    {
        for (int i = 0; i < interactObjects.Count; i++)
        {
            if (interactObjects[i].InteractID == key)
                return interactObjects[i];
        }
        Debug.Log("맞는 ID 값을 찾지못함.");
        return null;
    }
}
