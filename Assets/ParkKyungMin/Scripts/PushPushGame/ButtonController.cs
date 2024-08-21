using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    // 버튼 인덱스 저장
    [SerializeField] int buttonIndex;
    [SerializeField] Button button;
    [SerializeField] PushGameController gameController;

    void Start()
    {
        // Button 컴포넌트를 가져옴
        button = GetComponent<Button>();
        // GameController 스크립트를 가진 오브젝트를 찾음
        gameController = FindObjectOfType<PushGameController>();
        // 버튼 클릭 시 호출될 함수 등록
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // GameController의 OnPlayerButtonPress 함수 호출
        gameController.OnPlayerButtonPress(buttonIndex);
    }

    public void SetButtonIndex(int index)
    {
        buttonIndex = index;
    }

}
