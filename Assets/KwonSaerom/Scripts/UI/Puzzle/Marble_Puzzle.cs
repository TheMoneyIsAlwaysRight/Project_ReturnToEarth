using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marble_Puzzle : BasePuzzle
{
    public enum MarbleState
    {
        Earth,
        Jupiter,
        Mars,
        Mercury,
        Neptune,
        Saturn,
        Sun,
        Uranus,
        Venus
    }

    [Serializable]
    public class Marble
    {
        public MarbleState state;
        public Sprite sprite;
    }

    [SerializeField] List<Marble> sprites;
    [SerializeField] List<MarbleState> answer;
    [SerializeField] List<GameObject> tokens;

    private List<bool> isCheck = new List<bool>();
    private List<MarbleState> inputs = new List<MarbleState>();


    private void Awake()
    {
        //isCheck 초기화
        for (int i = 0; i < sprites.Count; i++)
            isCheck.Add(false);

        for(int i=0;i<tokens.Count;i++)
        {
            int random = UnityEngine.Random.Range(0, sprites.Count);
            while(true)
            {
                if (isCheck[random] == false)
                {
                    tokens[i].GetComponentInChildren<Button>().onClick.AddListener(() => OnClickedButton(sprites[random].state));
                    tokens[i].GetComponentInChildren<Image>().sprite = sprites[random].sprite;
                    isCheck[random] = true;
                    break;
                }
                random = UnityEngine.Random.Range(0, sprites.Count);
            }
        }
    }

    private void OnEnable()
    {
        inputs.Clear();
    }

    private void CheckPuzzleState()
    {
        if (IsPuzzleClear)
            return;

        if (answer.Count != inputs.Count)
            return;

        bool isComplete = true;

        for (int i = 0; i < answer.Count; i++)
        {
            if (answer[i] != inputs[i])
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
            IsPuzzleClear = true;

    }

    public void OnClickedButton(MarbleState input)
    {
        inputs.Add(input);
        Debug.Log(input);
        CheckPuzzleState();
    }

}
