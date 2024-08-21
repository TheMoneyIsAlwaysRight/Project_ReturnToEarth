using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 상호작용하면 인벤토리에 저장되는 인터렉트 컨트롤러
/// </summary>
public class ItemInteractController : InteractObject
{
    [Space(Define.INSPECTOR_SPACE)]
    // 아이템 테이블에 있는 아이템 ID. 상호작용 오브젝트 ID와는 다르다.
    [SerializeField] bool initActive; // 초반에 이 아이템이 활성화 되어있는지
    private bool isGet = false;
    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(initActive);
    }

    public override void Interact()
    {
        if (isGet)
            return;

        if (IsPassLastObject() == false)
            return;

        isGet = true;
        InteractObjectItem();

        //권새롬 추가 --> 튜토리얼시에는 로그가 떠야함
        GameTutorial tuto = GetComponent<GameTutorial>();
        if (tuto != null)
        {
            GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
            tuto.TutorialLog();
        }
        gameObject.SetActive(false);
        Close();
    }

    public override void Load()
    {
        isGet = true;
        InteractObjectItem(true);
        gameObject.SetActive(false);
    }
}
