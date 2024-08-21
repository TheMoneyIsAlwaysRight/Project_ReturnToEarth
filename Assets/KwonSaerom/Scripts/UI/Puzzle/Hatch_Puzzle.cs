using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;


public class Hatch_Puzzle : BasePuzzle
{
    [SerializeField] Scrollbar scroll; // 스와이프 감지
    [SerializeField] RectTransform hatchTransform;
    [SerializeField] TMP_Text answerText;

    private float scrollInitValue = 0.5f;
    private bool onSpin = false;

    private List<int> answerNum = new List<int>();
    private List<bool> answerBool = new List<bool>();
    private List<bool> input = new List<bool>();
    private ScrollRect scrollRect;
    private Coroutine Cohatch;

    private void Awake()
    {
        scrollRect = scroll.gameObject.GetComponentInParent<ScrollRect>();
        scroll.value = scrollInitValue;

        for (int i=0;i<3;i++)
        {
            answerNum.Add(Random.Range(1, 3));
            for(int j = 0; j < answerNum[i] ; j++)
            {
                answerBool.Add(i == 1); // index 1인거만 오른쪽으로 돌아감.
            }
        }
        answerText.text = $"<- {answerNum[0]} , -> {answerNum[1]} , <- {answerNum[2]}";
    }

    private void OnEnable()
    {
        // 입력값 초기화
        input.Clear();
        scroll.value = scrollInitValue;

        scrollRect.horizontal = true;
        onSpin = false;
        hatchTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnDisable()
    {
        if(Cohatch != null)
        {
            StopCoroutine(Cohatch);
            Cohatch = null;
        }
    }


    private void Update()
    {
        if (onSpin == false && (scroll.value > 0.55f || scroll.value < 0.45f))
        {
            Cohatch = StartCoroutine(SpinHatch());
        }
    }

    private void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        if (input.Count != answerBool.Count)
            return;

        bool isComplete = true;

        for (int i = 0; i < input.Count; i++)
        {
            if (input[i] != answerBool[i])
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
            IsPuzzleClear = true;

    }

    IEnumerator SpinHatch()
    {
        bool isRight = scroll.value < 0.55f; //돌아가는 방향
        input.Add(isRight);

        onSpin = true;
        scrollRect.horizontal = false;
        scroll.value = scrollInitValue;

        float goalSpin = isRight ? -360 : 360;
        float time = 0;
        float spinTime = 1.5f;
        while(time < spinTime)
        {
            time += Time.deltaTime;
            float tmp = Mathf.Lerp(0, goalSpin, time / spinTime);
            hatchTransform.rotation = Quaternion.Euler(0, 0, tmp);
            yield return null;
        }

        CheckPuzzleState();

        yield return new WaitForSeconds(0.3f);
        onSpin = false;
        scrollRect.horizontal = true;
    }
}
