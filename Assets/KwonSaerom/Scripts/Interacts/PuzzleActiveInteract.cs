using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Analytics;
using static Define;

public class PuzzleActiveInteract : CameraInteractController
{
    [Space(Define.INSPECTOR_SPACE)]
    [SerializeField] protected string puzzleNameKey;
    [SerializeField] bool initActive;

    [Header("상호작용 후 없어질건지 여부 : True면 없어짐")]
    [SerializeField] bool isInteractAfterNoneActive;
    [Header("카메라로 확대를 막을건지 여부.")] // 나중에 확대되지 않는 퍼즐이 나와서 이렇게 함.
    [SerializeField] bool isNotZoomIn;

    private GameObject puzzle;
    protected Clue_Object clue_Object;
    protected bool isComplete = false;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(initActive);
    }

    protected override void Start()
    {
        base.Start();
        clue_Object = GetComponent<Clue_Object>();
        if(PuzzleManager.Inst.puzzleDic.ContainsKey(puzzleNameKey))
        {
            puzzle = PuzzleManager.Inst.puzzleDic[puzzleNameKey];
            puzzle.GetComponent<BasePuzzle>().PuzzleObject = this;
        }
    }

    public override void Interact()
    {
        if (isNotZoomIn)
        {
            PuzzleOnInteract();
            return;
        }

        base.Interact();
    }


    public override void PuzzleOnInteract()
    {
        if (IsPassLastObject() == false)
            return;

        //박정민 코드 참고쓰---------------------------------------------------------------
        if (!isComplete)
        {
            PuzzleManager.Inst.PlayPuzzle(puzzleNameKey);
        }
        //------------------------------------------------------------------------
    }

    public virtual void CompletePuzzle()
    {
        isComplete = true;
        clue_Object.IsClear = true;
        clue_Object.OnCheckFlow?.Invoke();
        InteractObjectItem();
        if (isInteractAfterNoneActive)
            gameObject.SetActive(false);
    }

    public override void Load()
    {
        isComplete = true;
        InteractObjectItem(true);
        gameObject.SetActive(!isInteractAfterNoneActive);
    }
}
