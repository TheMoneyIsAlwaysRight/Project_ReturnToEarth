using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PushGameController : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] int currentStep = 0;
    [SerializeField] float displayTime;
    [SerializeField] float waitTimeForPlayer; // 플레이어가 맞출 시간

    private List<int> sequence = new List<int>();
    private List<int> playerInput = new List<int>();

    [SerializeField] GameObject playUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject successUI;
    [SerializeField] GameObject clearUI;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1.2f);
        successUI.SetActive(false);
        
        // 다음 문제가 바로 나오지 않게 대기시간
        yield return new WaitForSeconds(1.0f);
        StartNewGame();
    }

    private void StartNewGame()
    {
        sequence.Clear();
        playerInput.Clear();
        currentStep++;

        if (currentStep >= 2)
        {
            Time.timeScale += 0.2f;
            Debug.Log(Time.timeScale);
        }

        if (currentStep > 10)
        {
            clearUI.SetActive(true); // 클리어 UI 활성화
            Manager.Chapter.RecordData(3); //정민 추가 : 스테이지 클리어 기록용.
            Time.timeScale = 0; // 게임 중지
            MinigameStory.Instance.StartClearStory(); //새롬 추가 : 뒤에스토리 나오게
            return;
        }
        GenerateSequence();
        StartCoroutine(DisplaySequence());
    }

    private void GenerateSequence()
    {
        int sequenceLength = 0;

        // Step에 따른 스퀀스 (난이도 조절)
        if (currentStep >= 1 && currentStep <= 2)
        {
            sequenceLength = 3;
        }
        else if (currentStep >= 3 && currentStep <= 5)
        {
            sequenceLength = 4;
        }
        else if (currentStep >= 6 && currentStep <= 8)
        {
            sequenceLength = 5;
        }
        else if (currentStep >= 9 && currentStep <= 10)
        {
            sequenceLength = 6;
        }

        for (int i = 0; i < sequenceLength; i++)
        {
            int randomButton = Random.Range(0, buttons.Length);
            sequence.Add(randomButton);
        }
    }

    IEnumerator DisplaySequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            buttons[sequence[i]].GetComponent<Image>().color = Color.gray;
            yield return new WaitForSeconds(displayTime);
            buttons[sequence[i]].GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(displayTime / 2);
        }
        playerInput.Clear();

        playUI.SetActive(true);

        yield return new WaitForSeconds(waitTimeForPlayer); // 플레이어가 맞출 시간

        playUI.SetActive(false);
    }

    public void OnPlayerButtonPress(int buttonIndex)
    {
        playerInput.Add(buttonIndex);

        // 모든 입력이 완료되었을 때만 체크
        if (playerInput.Count == sequence.Count)
        {
            CheckPlayerInput();
        }
    }

    void CheckPlayerInput()
    {
        bool isCorrect = true;
        for (int i = 0; i < sequence.Count; i++)
        {
            if (playerInput[i] != sequence[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("맞아따");
            successUI.SetActive(true);
            StartCoroutine(StartDelay());
        }
        else
        {
            Debug.Log("틀려따");
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

    }
}

