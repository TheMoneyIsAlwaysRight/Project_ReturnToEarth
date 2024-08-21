using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 제작 : 찬규 
/// 캐릭터들의 대사를 관리하는 스크립트
/// </summary>
public class CharacterScripts : MonoBehaviour
{
    [SerializeField] TMP_Text script;           // 텍스트 출력
    [SerializeField] TMP_Text stageName;        // 스테이지 이름
    [SerializeField] List<int> scriptBundleNum;     // 대사번들의 ID값
    [SerializeField] List<string> texts;        // 출력 할 대사 모음집

    [SerializeField] float textDelay;           // 출력 할 대사 속도
    [SerializeField] bool check;              // true == "Front" or false == "Back"

    public TMP_Text Script { get { return script; } set { script = value; } }
    public List<int> ScriptBundleNum { get { return scriptBundleNum; } set { scriptBundleNum = value; } }

    [SerializeField] int textNum;                                // 현재 출력한 대사의 번호
    bool isNext;                                // 다음 대사로 넘어가도 되는지 체크
    bool isClick;                               // 연속 클릭 방지

    /// <summary>
    /// 대사창을 실행시키는 함수
    /// </summary>
    /// <param name="check"></FrontStory인지 BackStory인지>
    public void Execute(bool check)
    {
        stageName.text = Manager.Data.StageLoad(Manager.Scene.GetCurScene().ID).Name;

        Debug.Log("대사창 실행 함수 진입");

        scriptBundleNum.Clear();
        texts.Clear();

        Debug.Log("리스트 클리어");

        textNum = 0;
        this.check = check;

        Debug.Log("대사창 불리언 값 초기화");

        PullTextBundle();

        for (int i = 0; i < scriptBundleNum.Count; i++)
        {
            ScriptBundleLoad(scriptBundleNum[i]);
        }

        PrintScript();      // 제일 처음 스크립트 출력
    }

    public void PullTextBundle()
    {
        Debug.Log("텍스트번들 당겨오기 함수 실행");

        int stageNum = Manager.Scene.GetCurScene().ID;
        int uiNum;

        Debug.Log("씬에서 스테이지 id뽑아오기");

        if (check == true)
        {
            uiNum = Manager.Data.StageLoad(stageNum).FrontStory;
        }
        else
        {
            uiNum = Manager.Data.StageLoad(stageNum).BackStory;
        }

        

        if (check == true)  // FrontStory
        {
            while (true)
            {
                scriptBundleNum.Add(Manager.Data.UILoad(uiNum).Text);
                if (Manager.Data.UILoad(uiNum).UI == null)
                    break;
                uiNum = Manager.Data.UILoad(uiNum).UI[0];
            }
        }
        else       // BackStory
        {
            do
            {
                scriptBundleNum.Add(Manager.Data.UILoad(uiNum).Text);
                uiNum = Manager.Data.UILoad(uiNum).UI[0];
                if (Manager.Data.UILoad(uiNum).UI == null)
                    break;
            } while (Manager.Data.UILoad(uiNum).UI[0] != 104016 || Manager.Data.UILoad(uiNum).UI != null);
        }
    }

    Coroutine tmpRoutine;
    /// <summary>
    /// 스크립트 출력하는 함수
    /// </summary>
    public void PrintScript()
    {
        if (textNum == texts.Count)
        {
            CloseScripts();
        }

        if (isClick == false)
        {
            script.text = "";
            if (isNext == false)
            {
                isClick = true;
                tmpRoutine = StartCoroutine(Print());
                isClick = false;
            }
            else
            {
                isClick = true;
                isNext = false;
                StopCoroutine(tmpRoutine);
                script.text = texts[textNum];
                textNum++;
                isClick = false;
            }
        }
    }

    IEnumerator Print()
    {
        isNext = true;
        for (int i = 0; i < texts[textNum].Length; i++)
        {
            string t_letter = texts[textNum][i].ToString();
            script.text += t_letter;

            yield return new WaitForSecondsRealtime(textDelay);
        }
        script.text = texts[textNum];
        textNum++;
        isNext = false;
    }

    public void CloseScripts()
    {
        if (check == true)
        {
            Debug.Log("프린트 스크립트에서 대사창 닫음");
            Manager.UI.ClosePopUpUI();
            return;
        }
        else  // check == false 
        {
            Manager.Game.ShowClearUI();
            return;
        }
    }

    /// <summary>
    /// 텍스트번들에서 텍스트 뽑아내는 함수
    /// </summary>
    /// <param name="scriptBundleNum"></param>
    public void ScriptBundleLoad(int scriptBundleNum)
    {
        //권새롬 추가 -> 번들 key가 0이 찍힐때 return시킴
        if (scriptBundleNum == 0)
            return;
        //------------------------------------------

        int[] texts = Manager.Data.TextBundleLoad(scriptBundleNum).TEXT;

        for (int i = 0; i < texts.Length; i++)
        {
            this.texts.Add(ScriptLoad(texts[i]));
        }
    }

    /// <summary>
    /// 텍스트테이블에서 대사 뽑아내는 함수
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public string ScriptLoad(int ID)
    {
        string result;

        if (Manager.Game.LanguageSet == Define.LanguageSet.KR)
        {
            result = Manager.Data.TextLoad(ID).Text_KR;
        }
        else
        {
            result = Manager.Data.TextLoad(ID).Text_EN;
        }

        return result;
    }
}


