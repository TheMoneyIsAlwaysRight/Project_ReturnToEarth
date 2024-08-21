using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffToken : MonoBehaviour
{
    [SerializeField] Image checkImage;
    public bool AnswerCheck { get; set; } = false;
    public FindDifference_Puzzle CurPuzzle { get; set; }

    private void Start()
    {
        checkImage.gameObject.SetActive(false);
    }

    public void OnClickedAnswer()
    {
        if (AnswerCheck) //이미 맞췄으면 리턴.
            return;

        AnswerCheck = true;
        checkImage.gameObject.SetActive(true);
        CurPuzzle.CheckPuzzleState();
    }
}
