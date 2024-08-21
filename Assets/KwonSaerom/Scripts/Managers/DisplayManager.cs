using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] int setWidth = 720;
    [SerializeField] int setHeight = 1600;

    private static DisplayManager instance;
    public static DisplayManager Instance { get { return instance; } }

    private Rect safeArea;
    private Vector2 minArea;
    private Vector2 maxArea;
    private int deviceWidth;
    private float deviceHeight;
    private bool isAndroid;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        isAndroid = Application.platform == RuntimePlatform.Android;
        SizeSet();
        SetResolution(); // 초기에 게임 해상도 고정
    }

    private void SizeSet()
    {
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;

        safeArea = Screen.safeArea;
        minArea = safeArea.position;
        maxArea = minArea + safeArea.size;


        if(deviceHeight < maxArea.y)
        {
            maxArea.y = (int)((maxArea.y / maxArea.x) * setWidth);
        }
    }

    public void SetCanvas(CanvasScaler canvasScaler)
    {
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(setWidth, setHeight);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        Canvas canvas = canvasScaler.GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.WorldSpace)
            return;

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 1f;
        canvas.sortingLayerName = "UI";
    }

    private void SetUpCanvasScaler()
    {
        CanvasScaler[] canvasScalers = FindObjectsOfType<CanvasScaler>(true);
        for(int i=0;i<canvasScalers.Length;i++)
        {
            SetCanvas(canvasScalers[i]);
        }
    }

    private void SetResolution()
    {
        SetUpCanvasScaler();

        Screen.SetResolution(setWidth, (int)((deviceHeight / deviceWidth) * setWidth), false); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / (deviceWidth / maxArea.y); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, maxArea.y / deviceHeight); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = (deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

    /* 해상도 설정하는 함수 -> 오래됨버전 */
    private void SetOldResolution()
    {
        SetUpCanvasScaler();

        Screen.SetResolution(setWidth, (int)((deviceHeight / deviceWidth) * setWidth),false); // SetResolution 함수 제대로 사용하기

        SizeSet();

        if ((float)setWidth / setHeight < deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / (deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, maxArea.y / deviceHeight); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = (deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
}
