using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 클리어 할 때 사라지는 오브젝트가 있으면 컴포넌트로 넣기.
/// </summary>
public class ClearConnectObject : MonoBehaviour
{
    [SerializeField] List<int> unActiveKeys;
    private InGameScene gameScene;

    private void Start()
    {
        gameScene = Manager.Scene.GetCurScene() as InGameScene;
    }

    public void UnActive()
    {
        for(int i=0;i<unActiveKeys.Count;i++)
        {
            GameObject go = gameScene.FindInteractObject(unActiveKeys[i]).gameObject;
            go.SetActive(false);

            Clue_Object clue = go.GetComponent<Clue_Object>();
            if(clue != null)
                clue.IsClear = true;
        }
    }
}
