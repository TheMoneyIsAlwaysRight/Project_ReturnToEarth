using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class : 선 잇기 퍼즐 실행 시, 전달받은 좌표로 시작 정점들을 할당 클래스
public class DotLink_LevelGenerator : MonoBehaviour
{
    [SerializeField] DotLink_LogicManager logicManager;
    [SerializeField] DotLink_ColorData colordata;

    [SerializeField] int[] red = new int[4];
    [SerializeField] int[] blue = new int[4];
    [SerializeField] int[] green = new int[4];
    [SerializeField] int[] yellow = new int[4];

    //Method : 각 정점들마다 SetStartDot를 호출하는 레벨 제작 함수.
    [ContextMenu("generate!")]
    public void Generate()
    {
        SetStartDot(red, DotLink_Dot.State.Red);
        SetStartDot(blue, DotLink_Dot.State.Blue);
        SetStartDot(green, DotLink_Dot.State.Green);
        SetStartDot(yellow, DotLink_Dot.State.Yellow);
    }
    //Method : 저장된 좌표 및 색깔을 판단하여, 각 정점들을 이동 및 색깔 변경
    public void SetStartDot(int[] array,DotLink_Dot.State state)
    {
        logicManager.dotDic[$"{array[0]},{array[1]}"].startDot = true;
        logicManager.dotDic[$"{array[0]},{array[1]}"].ChangeState(state);

        ChangeColor(logicManager.dotDic[$"{array[0]},{array[1]}"], state);


        logicManager.dotDic[$"{array[2]},{array[3]}"].startDot = true;
        logicManager.dotDic[$"{array[2]},{array[3]}"].ChangeState(state);

        ChangeColor(logicManager.dotDic[$"{array[2]},{array[3]}"], state);
    }

    //Method : 현재 드래그 중인 정점에 따라, 색깔을 반환
    void ChangeColor(DotLink_Dot dot, DotLink_Dot.State state)
    {
        switch(state)
        {
            case DotLink_Dot.State.Red:
                dot.gameObject.GetComponentInChildren<Image>().color = colordata.redColor;
                break;
            case DotLink_Dot.State.Blue:
                dot.gameObject.GetComponentInChildren<Image>().color = colordata.blueColor;
                break;
            case DotLink_Dot.State.Green:
                dot.gameObject.GetComponentInChildren<Image>().color = colordata.greenColor;
                break;
            case DotLink_Dot.State.Yellow:
                dot.gameObject.GetComponentInChildren<Image>().color = colordata.yellowColor;
                break;
            default:
                break;
        }
    }

}
