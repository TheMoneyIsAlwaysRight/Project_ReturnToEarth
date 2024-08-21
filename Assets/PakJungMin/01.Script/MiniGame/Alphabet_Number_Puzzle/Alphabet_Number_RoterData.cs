using UnityEngine;
using UnityEngine.UI;

public class Alphabet_Number_RoterData : MonoBehaviour
{
    public enum BtnState
    {
        Up,
        Down
    }
    public enum State
    {
        Word_1,
        Word_3,
        Word_7,
        Word_9,
        Word_A,
        Word_C,
        Word_E,
        Word_S,
        Word_P,
    }

    [SerializeField] Alphabat_Number_RoterManager roterManager;
    [SerializeField] Alphabet_Number_SpriteData spriteData;
    [SerializeField] Image thisImage;

    public State curState;
    private void Start()
    {
        roterManager = GetComponentInParent<Alphabat_Number_RoterManager>();
        thisImage.sprite = spriteData.sprites[(int)curState];
    }

    public void ChangeState(BtnState state)
    {
        switch (state)
        {
            case BtnState.Up:
                curState++;
                if (curState > State.Word_P)
                {
                    curState = State.Word_1;
                }
                break;
            case BtnState.Down:
                curState--;
                if (curState < State.Word_1)
                {
                    curState = State.Word_P;
                }
                break;
        }
        thisImage.sprite = spriteData.sprites[(int)curState];

        roterManager.CheckAnswers();
    }
}