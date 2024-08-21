using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Nonogram_Tile : MonoBehaviour
{
    [Header("비워놓는 칸?")]
    [SerializeField] public bool isBlank;
    [Header("문제 칸?")]
    [SerializeField] public bool isQuiz;
    [Space(5f)]
    [Header("문제 해답")]
    [SerializeField] public bool isKey;

    public enum State
    {
        Check,
        Blank,
        X
    }
    public State curState;

    bool isChecked;

    public bool IsChecked { get { return isChecked; } }
    UnityAction OnClicked;

    private void Start()
    {
        OnClicked += GetComponentInParent<Nonogram_TileManager>().CheckClearGame;
    }

    public void ChangeState()
    {
        switch (curState)
        {

            case State.Blank:
                curState = State.Check;
                isChecked = true;
                ChangeBlack();
                break;
            case State.Check:
                curState = State.X;
                isChecked = false;
                GetComponentInChildren<TMP_Text>().text = "X";
                ChangeWhite();
                break;

            case State.X:
                curState = State.Blank;
                isChecked = false;
                GetComponentInChildren<TMP_Text>().text = "";
                ChangeWhite();
                break;
        }
        OnClicked?.Invoke();
    }



    /// <summary>
    /// Method : 현재 타일의 색을 흑색으로 바꾼다.
    /// </summary>
    void ChangeBlack()
    {
        Color newColor = new Color();
        newColor.r = 0f;
        newColor.g = 0f;
        newColor.b = 0f;
        newColor.a = 255f;
        Debug.Log("ChangeBlack");

        gameObject.GetComponent<Image>().color = newColor;
    }
    /// <summary>
    /// Method : 현재 타일의 색을 흰색으로 바꾼다.
    /// </summary>
    void ChangeWhite()
    {
        Color newColor = new Color();
        newColor.r = 255f;
        newColor.g = 255f;
        newColor.b = 255f;
        newColor.a = 255f;
        gameObject.GetComponent<Image>().color = newColor;
        Debug.Log("ChangeWhite");
    }


}
