using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Alphabat_Number_RoterManager : MonoBehaviour
{
    [SerializeField] List<Alphabet_Number_RoterData> roters;

    public List<Alphabet_Number_RoterData.State> correct;
    public Alphabet_Number_Puzzle puzzle;

    bool isUnlocked;

    public void CheckAnswers()
    {
        isUnlocked = true;

        for (int x = 0; x < correct.Count; x++)
        {
            if (correct[x] != roters[x].curState)
            {
                isUnlocked = false;
                continue;
            }
        }
        if (isUnlocked)
        {
            puzzle.IsPuzzleClear = true;
            return;
        }
    }




}
