using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docking_Puzzle : BasePuzzle
{
    [SerializeField] string lastObjectKey;

    private List<InteractObject> lastObjects;
    private InGameScene gameScene;
    private UI_Log logUI;
    private InteractionObject info;

    private void Awake()
    {
        gameScene = Manager.Scene.GetCurScene() as InGameScene;
    }

    private void Start()
    {
        logUI = Manager.Resource.Load<UI_Log>("UI_Log");
        info = Manager.Data.InteractionObjects[PuzzleObject.InteractID];
    }

    public void UnDocking()
    {
        if (IsPassLastObject() == false)
            return;

        IsPuzzleClear = true;
    }

    private bool IsPassLastObject()
    {
        if (lastObjectKey != "" && lastObjectKey != "0")
        {
            string[] keyList = lastObjectKey.Split(",");
            // 오브젝트들 초기화
            if (lastObjects == null)
            {
                lastObjects = new List<InteractObject>();
                for (int i = 0; i < keyList.Length; i++)
                    lastObjects.Add(null);
            }

            // 오브젝트별로 검사하기
            for (int i = 0; i < keyList.Length; i++)
            {
                int key = int.Parse(keyList[i]);
                if (lastObjects[i] == null)
                    lastObjects[i] = gameScene.FindInteractObject(key);

                //lastObject가 null인경우 : 아직 start로 초기화가 안되었다(활성화 안됨) : 즉, 문제를 풀지못했다.
                if (lastObjects[i] == null || lastObjects[i].gameObject.GetComponent<Clue_Object>().IsClear == false)
                {
                    UI_Log popup = Manager.UI.ShowPopUpUI(logUI);
                    if (Manager.Data.TextBundleTables.ContainsKey(info.interactText))
                    {
                        TextBundleTable tt = Manager.Data.TextBundleTables[info.interactText];
                        popup.SetLog(tt.ID);

                    }
                    else
                        popup.SetLog(102243); //"앞의 퍼즐들을 해결하지 못했어."
                    return false;
                }
            }
        }
        return true;
    }
}
