using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Density_LogicManager : MonoBehaviour
{
    [SerializeField] List<int> powerActiveKey = new List<int>();
    public Density_Puzzle puzzle;
    public Density_Button[] switchList; //스위치들이 인덱스에 정확하게 매칭시키기 위해 배열 사용.

    public int[] solution = new int[6];
    private InGameScene gameScene;

    private void Awake()
    {
        switchList = new Density_Button[6];
        gameScene = Manager.Scene.GetCurScene() as InGameScene;
    }

    public void CheckAnswer()
    {
        for (int x = 0; x < switchList.Length; x++)
        {
            if (switchList[x].value != solution[x])
            {
                Debug.Log("정답이 아닙니다.");
                return;
            }
        }
        puzzle.IsPuzzleClear = true;

        //권새롬 추가 --> 오브젝트 사라지고 생기는게 있음
        gameScene.FindInteractObject(powerActiveKey[0]).GetComponent<ConditionUnActiveInteract>().Count += 1;
    }
}
