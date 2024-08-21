using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Equation_LogicManager : MonoBehaviour
{
    [SerializeField] TMP_Text text_count_Solution; //정답 개수 뷰 텍스트 
    [SerializeField] TMP_Text text_sum_Solution; //정답 합계 뷰 텍스트


    [Header("정답 계수")]
    [SerializeField] int countSolution;
    [Header("정답 합계")]
    [SerializeField] int sumSolution;

    public List<Equation_Number> numberList;

    private void Awake()
    {
        numberList = new List<Equation_Number>();
        text_count_Solution.text = countSolution.ToString();
        text_sum_Solution.text = sumSolution.ToString();
    }

    public void CheckAnswer()
    {
        int result_count = 0;
        int result_sum = 0;
        for (int x = 0; x < numberList.Count; x++)
        {
            if (numberList[x].count == 0) { continue; }
            result_count += numberList[x].count;
            result_sum += numberList[x].number * numberList[x].count;
        }
        Debug.Log($"count : {result_count},sum : {result_sum}");
        if (countSolution == result_count && result_sum == sumSolution)
        {
            GetComponentInParent<Equation_Puzzle>().IsPuzzleClear = true;
        }

    }
}
