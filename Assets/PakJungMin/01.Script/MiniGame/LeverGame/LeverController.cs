using UnityEngine;
using UnityEngine.EventSystems;
using static LeverGame;

public class LeverController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Lever lever;
    private float prevleverPos;
    private const float UP_DOWN_DISTANCE = 100f;

    private void Awake()
    {
        lever = GetComponentInParent<Lever>();

        if (lever.CurState != LeverState.Middle)
            lever.CurState = LeverState.Middle;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevleverPos = eventData.position.y;
    }

    //레버를 조작중(드래그 중)
    public void OnDrag(PointerEventData eventData)
    {
        float curleverPos = eventData.position.y;
        if (prevleverPos + UP_DOWN_DISTANCE <= curleverPos) // 아래에서 위로 레버를 끌어올릴때 
        {
            switch (lever.CurState)
            {
                case LeverState.Down:
                    lever.CurState = LeverState.Middle;
                    prevleverPos = curleverPos;
                    break;
                case LeverState.Middle:
                    lever.CurState = LeverState.Up;
                    prevleverPos = curleverPos;
                    break;
                default:
                    break;
            }
        }
        else if (prevleverPos - UP_DOWN_DISTANCE >= curleverPos) // 위에서 아래로 레버를 끌어내릴때
        {
            switch (lever.CurState)
            {
                case LeverState.Up:
                    lever.CurState = LeverState.Middle;
                    prevleverPos = curleverPos;
                    break;
                case LeverState.Middle:
                    lever.CurState = LeverState.Down;
                    prevleverPos = curleverPos;
                    break;
            }
        }
    }


}
