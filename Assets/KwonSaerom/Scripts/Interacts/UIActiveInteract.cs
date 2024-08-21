using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 단순 UI가 띄어지는 상호작용 오브젝트.
/// Puzzle이 띄어지는것과 다름. Puzzle을 띄우고싶으면 -> PuzzleActiveInteract를 사용할것.
/// </summary>
public class UIActiveInteract : CameraInteractController
{
    [Space(Define.INSPECTOR_SPACE)]
    [SerializeField] string UIKey;
    [SerializeField] bool initActive;
    PopUpUI UIPrefab;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(initActive);
    }

    protected override void Start()
    {
        base.Start();
        UIPrefab = Manager.Resource.Load<PopUpUI>(UIKey);
    }

    public override void PuzzleOnInteract()
    {
        if(UIPrefab != null)
            Manager.UI.ShowPopUpUI(UIPrefab);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public override void Load()
    {

    }
}
