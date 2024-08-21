using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 기획서에 적힌 (상호작용 오브젝트 기획서 책갈피 1)
/// 레이어가 있어서 투명도가 조절되는 상호작용 오브젝트는 이 클래스를 컴포넌트로 사용.
/// </summary>
public class LayerInteractController : CameraInteractController
{
    [Space(Define.INSPECTOR_SPACE)]
    [SerializeField] List<SpriteRenderer> layerObjects;

    private int interactCount = 0;
    private float fadeSpeed = 0.05f; //투명도 조절 속도
    private float fadePer = 0.1f; //투명도 조절 비율
    private bool isLast = false; //지금 마지막 오브젝트를 보고있는지

    Clue_Object myClue;

    protected override void Awake()
    {
        base.Awake();
        //레이어 첫번째 빼고는 비활성화, 알파 0 설정.
        //layerObjects[0].gameObject.SetActive(true);
        //for (int i=1;i< layerObjects.Count;i++)
        //{
        //    Color color = layerObjects[i].color;
        //    color.a = 0;
        //    layerObjects[i].color = color;
        //    layerObjects[i].gameObject.SetActive(false);
        //}
        myClue = GetComponent<Clue_Object>();
    }

    protected override void Start()
    {
        base.Start();
        gameScene.AddInteractObject(layerObjects[layerObjects.Count - 1].gameObject);
    }

    public override void PuzzleOnInteract()
    {
        if (interactCount > layerObjects.Count)
            return;

        StartCoroutine(CoInteract());
    }

    IEnumerator CoInteract()
    {
        yield return ActiveChangeObject(layerObjects[interactCount]);
        interactCount++;

        if (interactCount == layerObjects.Count - 1)
        {
            InteractObjectItem();
            myClue.IsClear = true;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 마지막 인터렉트 오브젝트에 인터렉트 관련 컴포넌트 넣어놔야함. 그래야 상호작용 가능.
    /// </summary>
    private void LastObjectInteract()
    {
        isLast = true;
        IInteractable lastInteract = layerObjects[layerObjects.Count - 1].gameObject.GetComponent<IInteractable>();
        if(lastInteract != null)
            lastInteract.Interact();
    }

    IEnumerator ActiveChangeObject(SpriteRenderer fadeOutObject)
    {
        Color fadeoutColor = fadeOutObject.color;
        while (true)
        {
            fadeoutColor.a -= fadePer;
            yield return new WaitForSecondsRealtime(fadeSpeed);
            fadeOutObject.color = fadeoutColor;
            if (fadeoutColor.a <= 0.01f) //투명도 0에 가까워지면
                break;
        }
        fadeOutObject.gameObject.SetActive(false);
    }

    IEnumerator ActiveChangeObject(SpriteRenderer fadeOutObject, SpriteRenderer fadeInObject)
    {
        fadeInObject.gameObject.SetActive(true);
        Color fadeoutColor = fadeOutObject.color;
        Color fadeinColor = fadeInObject.color;
        while(true)
        {
            fadeoutColor.a -= fadePer;
            fadeinColor.a += fadePer;
            yield return new WaitForSecondsRealtime(fadeSpeed);
            fadeOutObject.color = fadeoutColor;
            fadeInObject.color = fadeinColor;
            if (fadeoutColor.a <= 0.01f) //투명도 0에 가까워지면
                break;
        }
        fadeOutObject.gameObject.SetActive(false);
    }

    

    public override void Load()
    {
        InteractObjectItem(true);
        gameObject.SetActive(false);
    }

}
