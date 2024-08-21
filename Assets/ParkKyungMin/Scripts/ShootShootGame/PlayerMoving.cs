using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
    [HideInInspector] public float maxTouchY; // 터치 가능한 최대 Y값
}

public class PlayerMoving : MonoBehaviour
{
    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true; // 플레이어 제어 활성화

    public static PlayerMoving instance;

    // 스크립트 인스턴스 초기화
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders(); // 경계 값 초기화
    }

    private void Update()
    {
        // 제어가 활성화된 경우
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            // 마우스 클릭 시 이동
            if (Input.GetMouseButton(0))
            {
                if (!IsPointerOverUIObject())
                {
                    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    if (mousePosition.y < borders.maxTouchY)
                    {
                        mousePosition.z = transform.position.z;
                        transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
                    }
                }
            }
#endif

#if UNITY_IOS || UNITY_ANDROID
            // 터치 입력 시 이동
            if (Input.touchCount == 1) // 터치가 하나인 경우
            {
                Touch touch = Input.touches[0];

                // 터치 위치가 UI 요소 위가 아닌지 확인
                if (!IsPointerOverUIObject(touch))
                {
                    Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position); // 터치 위치를 월드 좌표로 변환
                    if (touchPosition.y < borders.maxTouchY)
                    {
                        touchPosition.z = transform.position.z;
                        transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
                    }
                }
            }
#endif
            // 플레이어 위치를 경계 내로 제한
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY), 0);
        }
    }

    void ResizeBorders()
    {
        // 뷰포트 경계값 계산
        float topExclusion = 0.25f; 

        borders.minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(new Vector3(1, 1 - topExclusion, 0)).y - borders.maxYOffset;
        borders.maxTouchY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1 - topExclusion, 0)).y; // 터치 가능한 최대 Y값 설정
    }


    private bool IsPointerOverUIObject()    // 마우스 포인터가 UI 요소 위에 있는지 확인
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private bool IsPointerOverUIObject(Touch touch)     // 터치 위치가 UI 요소 위에 있는지 확인
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
