using TMPro;
using UnityEngine;

public class RoterData : MonoBehaviour
{
    [SerializeField] int curNumber;
    [SerializeField] TMP_Text text;
    [SerializeField] RoterManager roterManager;


    private void Start()
    {
        roterManager = GetComponentInParent<RoterManager>();
    }
    public int CurNumber
    {
        get { return curNumber; }
        set
        {
            curNumber = value;
            if(curNumber > 9)
            {
                curNumber = 0;
            }
            else if(curNumber < 0)
            {
                curNumber = 9;
            }

            roterManager.CheckRoters();
            text.text = curNumber.ToString();
        }
    }

}
