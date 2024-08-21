using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;// 뒤끝 SDK namespace 추가
using System.Threading.Tasks; // [변경] async 기능을 이용하기 위해서는 해당 namepsace가 필요

public class BackendManager : Singleton<BackendManager>
{
    void Start()
    {
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }

        //Test();
    }

    void Update()
    {
        Backend.AsyncPoll();
    }

    // =======================================================
    // [추가] 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    // =======================================================
    async void Test()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.GuestLogin();

            Debug.Log("테스트를 종료합니다.");
        });
    }

    #region Login
    public async void GuestLogin()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.GuestLogin();
            Debug.Log("게스트 로그인");
        });
    }

    public async void GoogleLogin()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.GPGSLogin(); // 구글 플레이 게임 서비스 로그인
            Debug.Log("구글 로그인");
        });
    }
    #endregion

    #region GuestID
    public string PrintGuestID()
    {
        return BackendLogin.Instance.GetGuestID();
    }

    public void DeleteGuestID()
    {
        BackendLogin.Instance.DeleteGuestID();
    }
    #endregion

    #region SetServerData
    public void SetServerData()
    {
        // 게스트로그인 계정이 있을 때
        BackendGameData.Instance.GameDataGet();
    }
    #endregion

    #region Log
    /// <summary>
    /// 유저로그 넣는 함수
    /// </summary>
    public void UserLogInsert(UserLog userLog)
    {
        BackendGameLog.Instance.UserLogInsert(userLog);
    }
    #endregion
}
