using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_MainPower : PopUpUI
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<string> answers;
    [SerializeField] List<int> powerActiveKey = new List<int>();
    [SerializeField] Image image;
    [SerializeField] Button powerButton;

    private InGameScene gameScene;
    private InteractObject connectObject;
    private UI_Log logUI;

    protected override void Awake()
    {
        if (PuzzleManager.Inst.CorrectAnswers.ContainsKey((int)PuzzleAnswerKey.Chap03_2_Power))
        {
            List<object> answer = PuzzleManager.Inst.CorrectAnswers[(int)PuzzleAnswerKey.Chap03_2_Power];

            //1. string으로 변환하기
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < answer.Count; i++)
            {
                if((bool)answer[i])
                    sb.Append(1);
                else
                    sb.Append(0);
            }

            //2. answers중에 같은거 찾기
            string curAnswer = sb.ToString();
            
            for (int i = 0; i < answers.Count; i++)
            {
                if (curAnswer.Equals(answers[i]))
                {
                    image.sprite = sprites[i];
                    break;
                }
            }
        }
        else
            InitAnswer();
    }

    private void Start()
    {
        //버튼 power 관련.
        gameScene = Manager.Scene.GetCurScene() as InGameScene;
        logUI = Manager.Resource.Load<UI_Log>("UI_Log");

        if (gameScene != null)
        {
            connectObject = gameScene.FindInteractObject(powerActiveKey[0]);
        }
    }


    private void InitAnswer()
    {
        int random = Random.Range(0, sprites.Count);
        Debug.Log(random);
        image.sprite = sprites[random];

        List<object> answer = new List<object>();
        for(int i=0; i < answers[random].Length;i++)
            answer.Add(answers[random][i] == '1');
        PuzzleManager.Inst.CorrectAnswers.Add((int)PuzzleAnswerKey.Chap03_2_Power, answer);
    }

    public void OnClickedPowerButton()
    {
        if (connectObject.GetComponent<Clue_Object>().IsClear == false)
        {
            Close();
            Manager.Game.Camera.CurrentCamState = CameraController.CameraState.Base;
            UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
            popup.SetLog(102247);
            return;
        }

        //1,2 번 인덱스는 비활성화, 3,4번 인덱스는 활성화 시킴.
        gameScene.FindInteractObject(powerActiveKey[1]).gameObject.SetActive(false);
        gameScene.FindInteractObject(powerActiveKey[2]).gameObject.SetActive(false);
        gameScene.FindInteractObject(powerActiveKey[3]).gameObject.SetActive(true);
        gameScene.FindInteractObject(powerActiveKey[4]).gameObject.SetActive(true);
        gameScene.FindInteractObject(powerActiveKey[5]).GetComponent<Clue_Object>().IsClear = true;

        gameScene.FindInteractObject(107133).GetComponent<ConditionUnActiveInteract>().Count += 1;
        Close();
    }
}
