using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoterManager : MonoBehaviour
{
    List<RoterData> roters;
    [SerializeField] int[] answer; //자물쇠 정답.

    bool isUnlocked;
    private void OnEnable()
    {
        answer = new int[3]; //회전자의 개수만큼 배열 생성

        for (int x = 0; x < answer.Length; x++)
        {
            answer[x] = Random.Range(0, 10);
            Debug.Log($"x번 요소의 답 : {answer[x]}");
        }
        roters = GetComponentsInChildren<RoterData>().ToList<RoterData>();

    }

    public void CheckRoters()
    {
        isUnlocked = true;

        for (int x = 0; x < answer.Length; x++)
        {
            if (answer[x] != roters[x].CurNumber)
            {
                isUnlocked = false;
                continue;
            }
        }


        if (isUnlocked)
        {
            Debug.Log("문이 열렸습니다.");
            transform.parent.gameObject.GetComponent<LockerGame>().IsPuzzleClear = true;
            return;
        }
    }




}
