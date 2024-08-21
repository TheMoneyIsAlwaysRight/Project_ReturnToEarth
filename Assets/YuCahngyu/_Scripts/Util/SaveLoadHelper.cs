using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 제작 : 찬규 
/// 스테이지에서 나오거나 해당 스테이지를 클리어 했을 때 저장 할 세이브데이터를 만들고
/// 스테이지를 다시 들어갈때 기존의 진행상황을 로드해오는 정적 클래스
/// </summary>
public static class SaveLoadHelper
{
#if UNITY_EDITOR
    private static string path => Path.Combine(Application.dataPath, $"Resources/Data");
#else
    private static string path => Path.Combine(Application.persistentDataPath, $"Resources/Data");
#endif

    // 저장 해야 할 테이블들을 저장하는 함수
    public static void SaveProcess()
    {
        Manager.Data.SaveTable("StageProcessTable");
        Manager.Data.SaveTable("InventoryTable");
        // 계속 추가하면 됩니다        
    }

    /// <summary>
    /// 변동데이터 초기값 복사 함수
    /// </summary>
    /// <param name="fileName"></param>
    public static void BasicJsonCreate(string fileName)
    {
        if (Directory.Exists($"{path}/BasicJson") == false)
        {
            Directory.CreateDirectory($"{path}/BasicJson");
        }

        switch (fileName)
        {
            case "UserInfoTable":
                List<UserInfoTable> _userInfoTable = new List<UserInfoTable>();
                string userInfoTable = "";

                foreach (UserInfoTable i in Manager.Data.UserInfoTables.Values)
                {
                    _userInfoTable.Add(i);
                }

                for (int i = 0; i < _userInfoTable.Count; i++)
                {
                    if (i == 0)
                    {
                        string _json = JsonUtility.ToJson(_userInfoTable[i], true);
                        userInfoTable += _json;
                    }
                    else
                    {
                        string _json = "," + JsonUtility.ToJson(_userInfoTable[i], true);
                        userInfoTable += _json;
                    }
                }
                userInfoTable = "[" + userInfoTable + "]";
                File.WriteAllText($"{path}/BasicJson/{fileName}.json", userInfoTable);
                Debug.Log("저장 성공");
                break;
            case "UserItemTable":
                List<UserItemTable> _userItemTable = new List<UserItemTable>();
                string userItemTable = "";

                foreach (UserItemTable i in Manager.Data.UserItemTables.Values)
                {
                    _userItemTable.Add(i);
                }

                for (int i = 0; i < _userItemTable.Count; i++)
                {
                    if (i == 0)
                    {
                        string _json = JsonUtility.ToJson(_userItemTable[i], true);
                        userItemTable += _json;
                    }
                    else
                    {
                        string _json = "," + JsonUtility.ToJson(_userItemTable[i], true);
                        userItemTable += _json;
                    }
                }
                userItemTable = "[" + userItemTable + "]";
                File.WriteAllText($"{path}/BasicJson/{fileName}.json", userItemTable);
                Debug.Log("저장 성공");
                break;
            case "InventoryTable":
                List<InventoryTable> _inventoryTable = new List<InventoryTable>();
                string inventoryTable = "";

                foreach (InventoryTable i in Manager.Data.InventoryTables.Values)
                {
                    _inventoryTable.Add(i);
                }

                for (int i = 0; i < _inventoryTable.Count; i++)
                {
                    if (i == 0)
                    {
                        string _json = JsonUtility.ToJson(_inventoryTable[i], true);
                        inventoryTable += _json;
                    }
                    else
                    {
                        string _json = "," + JsonUtility.ToJson(_inventoryTable[i], true);
                        inventoryTable += _json;
                    }
                }
                inventoryTable = "[" + inventoryTable + "]";
                File.WriteAllText($"{path}/BasicJson/{fileName}.json", inventoryTable);
                Debug.Log("저장 성공");
                break;
            case "StageProcessTable":
                List<StageProcessTable> _stageProcessTable = new List<StageProcessTable>();
                string stageProcessTable = "";

                foreach (StageProcessTable i in Manager.Data.StageProcessTables.Values)
                {
                    _stageProcessTable.Add(i);
                }

                for (int i = 0; i < _stageProcessTable.Count; i++)
                {
                    if (i == 0)
                    {
                        string _json = JsonUtility.ToJson(_stageProcessTable[i], true);
                        stageProcessTable += _json;
                    }
                    else
                    {
                        string _json = "," + JsonUtility.ToJson(_stageProcessTable[i], true);
                        stageProcessTable += _json;
                    }
                }
                stageProcessTable = "[" + stageProcessTable + "]";
                File.WriteAllText($"{path}/BasicJson/{fileName}.json", stageProcessTable);
                Debug.Log("저장 성공");
                break;
            case "UserLogTable":
                List<UserLogTable> _userLogTable = new List<UserLogTable>();
                string userLogTable = "";

                foreach (UserLogTable i in Manager.Data.UserLogTables.Values)
                {
                    _userLogTable.Add(i);
                }

                for (int i = 0; i < _userLogTable.Count; i++)
                {
                    if (i == 0)
                    {
                        string _json = JsonUtility.ToJson(_userLogTable[i], true);
                        userLogTable += _json;
                    }
                    else
                    {
                        string _json = "," + JsonUtility.ToJson(_userLogTable[i], true);
                        userLogTable += _json;
                    }
                }
                userLogTable = "[" + userLogTable + "]";
                File.WriteAllText($"{path}/BasicJson/{fileName}.json", userLogTable);
                Debug.Log("저장 성공");
                break;
        }
    }

    // 진행상황을 로드해올 함수
    public static void LoadClue(int curSceneID)    // 괄호에 들어가는 인수는 각 기믹(단서)의 우선도
    {
        // 뭘 로드 해 와야 할까?
        // StageProcessTable과 InventoryTable의 데이터들을 로드해야함

        // 모든 기믹을 체크해서 그게 클리어가 됐는지 안됐는지를 체크해야함
        // 또한 모든 아이템을 체크해서 그게 사용이 됐는지 안됐는지 체크해야함

        // 여기서는 기믹 하나를 체크하는 기능을 만든 다음에 각 스테이지별로 기믹 수를 체크해서
        // 반복사용 하는 방법으로 구현을 해보자

        // 기믹 하나를 체크하려면 어찌 해야할까?
        // 기믹이 들고있는 priority(우선도)
        // 이게 Manager.Data.StageProcessLoad(Manager.Scene.curScene.ID).isUse[priority] 를 체크
        // 그러면 그 씬에 있는 기믹을 하나씩 불러와야 함

        //for (int i = 0; i < Manager.Data.StageProcessLoad(curSceneID).IsUse.Length; i++)
        //{
        //    if (Manager.Data.StageProcessLoad(curSceneID).IsUse[i] == false)
        //    {
        //        continue;
        //    }
        //    else     // true 인 경우 (해당 기믹을 풀었을 경우)
        //    {
        //        // 해당 기믹의 상호작용오브젝트 ID를 불러와서 상호작용 ID로 RelationID[] 배열을 불러온다
        //    }
        //}
    }
}
