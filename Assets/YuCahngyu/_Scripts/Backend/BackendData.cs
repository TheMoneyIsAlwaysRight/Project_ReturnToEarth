// 뒤끝 SDK namespace 추가
using BackEnd;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UserData
{
    public string UID;             // 해당 유저의 UID
    public List<int> ClearStage;    // 해당 유저가 클리어한 스테이지와 그 스테이지의 별 갯수
    public int OxygenTank;      // 해당 유저의 산소통 갯수
    public int HintItem;        // 해당 유저의 힌트아이템 갯수
    public List<int> RoundStage;    // 해당 유저가 각 스테이지를 몇번 했는지 알려주는 리스트

    // 데이터를 디버깅하기 위한 함수입니다.(Debug.Log(UserData);)
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"UID : {UID}");
        result.AppendLine($"ClearStage : {ClearStage}");
        result.AppendLine($"OxygenTank : {OxygenTank}");
        result.AppendLine($"HintItem : {HintItem}");
        result.AppendLine($"RoundStage : {RoundStage}");

        return result.ToString();
    }
}

public class BackendGameData
{
    private static BackendGameData _instance = null;

    public static BackendGameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameData();
            }

            return _instance;
        }
    }

    public static UserData userData;

    private string gameDataRowInDate = string.Empty;

    // 데이터 초기화 (서버에 초기 데이터값 넣기)
    public void GameDataInsert()
    {
        if (userData == null)
        {
            userData = new UserData();
        }

        Debug.Log("데이터를 초기화합니다.");
        userData.UID = BackendManager.Instance.PrintGuestID();
        userData.OxygenTank = 0;
        userData.HintItem = 0;


        // 스테이지 갯수만큼 리스트에 0 넣기
        userData.ClearStage = new List<int>();
        userData.RoundStage = new List<int>();
        int stageCnt = Manager.Data.StageData.tables.Length;    // 스테이지의 전체 갯수
        for (int i = 0; i < stageCnt; i++)
        {
            userData.ClearStage.Add(0);
            userData.RoundStage.Add(0);
        }

        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
        Param param = new Param();
        param.Add("UID", userData.UID);
        param.Add("OxygenTank", userData.OxygenTank);
        param.Add("HintItem", userData.HintItem);
        param.Add("ClearStage", userData.ClearStage);
        param.Add("RoundStage", userData.RoundStage);

        Debug.Log("게임 정보 데이터 삽입을 요청합니다.");
        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 삽입에 성공했습니다. : " + bro);

            //삽입한 게임 정보의 고유값입니다.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("게임 정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    public void BasicGameDataInsert()
    {
        if (userData == null)
        {
            userData = new UserData();
        }

        Debug.Log("데이터를 초기화합니다.");
        userData.UID = BackendManager.Instance.PrintGuestID();
        userData.OxygenTank = 0;
        userData.HintItem = 0;


        // 스테이지 갯수만큼 리스트에 0 넣기
        userData.ClearStage = new List<int>();
        int stageCnt = Manager.Data.StageData.tables.Length;    // 스테이지의 전체 갯수
        for (int i = 0; i < stageCnt; i++)
        {
            userData.ClearStage.Add(0);
        }

        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
        Param param = new Param();
        param.Add("UID", userData.UID);
        param.Add("OxygenTank", userData.OxygenTank);
        param.Add("HintItem", userData.HintItem);
        param.Add("ClearStage", userData.ClearStage);
        param.Add("RoundStage", userData.RoundStage);

        Debug.Log("게임 정보 데이터 삽입을 요청합니다.");
        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 삽입에 성공했습니다. : " + bro);

            //삽입한 게임 정보의 고유값입니다.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("게임 정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    public void BasicGameDataGet()
    {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);

            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.  

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
                GameDataInsert();       // 데이터가 존재하지 않으므로 서버에 데이터를 생성해준다
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임 정보의 고유값입니다.  

                userData = new UserData();

                userData.UID = gameDataJson[0]["UID"].ToString();
                userData.OxygenTank = int.Parse(gameDataJson[0]["OxygenTank"].ToString());
                userData.HintItem = int.Parse(gameDataJson[0]["HintItem"].ToString());

                userData.ClearStage = new List<int>();
                foreach (LitJson.JsonData star in gameDataJson[0]["ClearStage"])
                {
                    userData.ClearStage.Add(int.Parse(star.ToString()));
                }

                Debug.Log(userData.ToString());
            }
        }
        else
        {
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
            BackendLogin.Instance.DeleteGuestID();
            Manager.Scene.LoadScene("LoginScene");
        }
    }

    // 데이터 가져오기
    public void GameDataGet()
    {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);

            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.  

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
                GameDataInsert();       // 데이터가 존재하지 않으므로 서버에 데이터를 생성해준다
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임 정보의 고유값입니다.  

                userData = new UserData();

                userData.UID = gameDataJson[0]["UID"].ToString();
                userData.OxygenTank = int.Parse(gameDataJson[0]["OxygenTank"].ToString());
                userData.HintItem = int.Parse(gameDataJson[0]["HintItem"].ToString());

                userData.ClearStage = new List<int>();
                foreach (LitJson.JsonData star in gameDataJson[0]["ClearStage"])
                {
                    userData.ClearStage.Add(int.Parse(star.ToString()));
                }

                userData.RoundStage = new List<int>();
                try
                {
                    foreach (LitJson.JsonData star in gameDataJson[0]["RoundStage"])
                    {
                        userData.RoundStage.Add(int.Parse(star.ToString()));
                    }

                }
                catch
                {
                    RoundStageInsert();
                }

                Debug.Log(userData.ToString());
            }
        }
        else
        {
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
            BackendLogin.Instance.DeleteGuestID();
            Manager.Scene.LoadScene("LoginScene");
        }
    }

    public class JsonToList
    {
        public List<int> intList;
    }

    #region Function
    // 각 기능 함수
    // 예를 들면 유료아이템 갯수 변경이라던가?

    // 산소통 갯수 변경
    public void ChangeOxygen(int cnt)
    {
        GameDataGet();      // 변경 전 서버에서 데이터를 바로 들고 온다

        userData.OxygenTank += cnt;

        if (userData.OxygenTank < 0)
        {
            Debug.Log("이미 0개입니다.");
        }
        GameDataUpdate();   // 서버에 변경사항을 바로 연동해준다
    }

    // 힌트아이템 갯수 변경
    public void ChangeHint(int cnt)
    {
        GameDataGet();      // 변경 전 서버에서 데이터를 바로 들고 온다

        userData.HintItem += cnt;

        if (userData.HintItem < 0)
        {
            Debug.Log("이미 0개입니다.");
        }
        GameDataUpdate();   // 서버에 변경사항을 바로 연동해준다
    }

    // 클리어한 스테이지와 해당 스테이지의 별 갯수를 변경
    public void ChangeClearStage(int stageID, int starCount)
    {
        GameDataGet();      // 변경 전 서버에서 데이터를 바로 들고 온다

        int stageNum = stageID % 100;   // 스테이지가 100개가 넘어가면 바꿔야함

        userData.ClearStage[stageNum - 1] = starCount;

        GameDataUpdate();   // 서버에 변경사항을 바로 연동해준다
    }

    // 클리어한 스테이지와 해당 스테이지의 별 갯수를 변경
    public void ChangeRoundStage(int stageID)
    {
        GameDataGet();      // 변경 전 서버에서 데이터를 바로 들고 온다

        int stageNum = stageID % 100;   // 스테이지가 100개가 넘어가면 바꿔야함

        userData.RoundStage[stageNum - 1]++;

        GameDataUpdate();   // 서버에 변경사항을 바로 연동해준다
    }

    /// <summary>
    /// 권새롬 추가 --> 유저가 스테이지를 클리어했는지 확인하는 함수
    /// </summary>
    /// <returns></returns>
    public bool IsClearStage(int stageKey)
    {
        int stageNum = stageKey % 100;

        if (userData.ClearStage[stageNum-1] == 0)
            return false;

        return true;
    }

    /// <summary>
    /// 게임정보가 있는지 없는지 체크 하는 함수
    /// </summary>
    /// <returns></returns>
    public bool CheckUserInfo()
    {
        bool result;

        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());

        if (bro.IsSuccess())
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public void RoundStageInsert()
    {
        BasicGameDataGet();

        userData.RoundStage = new List<int>();
        int stageCnt = Manager.Data.StageData.tables.Length;    // 스테이지의 전체 갯수
        for (int i = 0; i < stageCnt; i++)
        {
            userData.RoundStage.Add(0);
        }
        GameDataUpdate();
    }

    #endregion

    // 데이터 수정하기
    public void GameDataUpdate()
    {
        if (userData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
            return;
        }

        Param param = new Param();
        param.Add("UID", userData.UID);
        param.Add("OxygenTank", userData.OxygenTank);
        param.Add("HintItem", userData.HintItem);
        param.Add("ClearStage", userData.ClearStage);
        param.Add("RoundStage", userData.RoundStage);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임 정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임 정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }

    /// <summary>
    /// 데이터 유료아이템 변조 검사
    /// </summary>
    public void CheckData()
    {
        // 결제가 성공적으로 이뤄지면 서버의 데이터를 업데이트 해준다
        //      서버의 데이터를 업데이트 할 때는 서버의 데이터를 가져와서 새로운 임시 데이터로 만들어주고
        //      해당 임시데이터를 수정해서 서버에 업로드 해서 바꿔주는 방식

        // 그 이후 서버의 데이터를 다시 가져와서 로컬을 바꿔준다
    }
}
