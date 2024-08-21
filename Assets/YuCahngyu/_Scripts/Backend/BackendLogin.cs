// 뒤끝 SDK namespace 추가
using BackEnd;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 제작 : 찬규
/// 로그인화면에서 게스트 로그인을 하는 기능이 있는 컴포넌트
/// </summary>
public class BackendLogin
{
    private static BackendLogin _instance = null;

    public static BackendLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }

    #region CustomLogin
    // ******************** 커스텀 로그인 ********************
    // 커스텀 회원가입
    public void CustomSignUp(string id, string pw)
    {
        Debug.Log("회원가입을 요청합니다.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("회원가입에 실패했습니다. : " + bro);
        }
    }

    // 커스텀 로그인
    public void CustomLogin(string id, string pw)
    {
        Debug.Log("로그인을 요청합니다.");

        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인이 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("로그인이 실패했습니다. : " + bro);
        }
    }

    // 커스텀아이디의 닉네임 변경함수
    public void UpdateNickname(string nickname)
    {
        Debug.Log("닉네임 변경을 요청합니다.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("닉네임 변경에 성공했습니다 : " + bro);
        }
        else
        {
            Debug.LogError("닉네임 변경에 실패했습니다 : " + bro);
        }
    }
    // ************************************************************
    #endregion

    #region GuestLogin
    // ******************** 게스트 로그인 ********************
    // 게스트계정의 아이디 유무 체크
    public bool CheckGuest()        // 로컬 id의 유무
    {
        bool result;

        string id = Backend.BMember.GetGuestID();

        if (id == null || id == "")
        {
            // id가 없으면 계정 생성을 위해 false반환
            result = false;
        }
        else  // id가 있으면
        {
            // id가 있으면 계정 생성을 넘어가도 되니까 true반환
            result = true;
        }

        return result;
    }

    //게스트 로그인
    public void GuestLogin()
    {
        Backend.BMember.GuestLogin("게스트 로그인으로 로그인함", (callback) =>
        {
            Debug.Log("게스트 로그인을 시도했습니다");
            if (callback.IsSuccess())
            {
                Debug.Log("게스트 로그인에 성공했습니다");                
            }
            else
            {
                Debug.Log("게스트 로그인에 실패했습니다");
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        Debug.Log($"{callback.IsSuccess()} - 400 : device unique ID를 확인 할 수 없습니다");
                        break;
                    case 401:
                        Debug.Log($"{callback.IsSuccess()} - 401 : 잘못된 custom ID입니다");
                        break;
                    case 403:
                        Debug.Log($"{callback.IsSuccess()} - 403 : 차단된 유저입니다");
                        break;
                    case 410:
                        Debug.Log($"{callback.IsSuccess()} - 410 : 서버에서 사라진 유저입니다");
                        break;
                    default:
                        Debug.Log($"{callback.IsSuccess()} - ??? : {int.Parse(callback.GetStatusCode())} 오류 입니다");
                        break;
                }
                Debug.Log($"실패 : {callback.IsSuccess()}");
            }

            Manager.UI.ClosePopUpUI();
        });
    }

    // 기기 내부의 게스트 정보 삭제
    public void DeleteGuestID()
    {
        Backend.BMember.DeleteGuestInfo();
    }
    // ************************************************************
    #endregion

    #region FederationLogin
    // ******************** 페더레이션 로그인 ********************

    // 구글 로그인
    public void GPGSLogin()
    {
        // 이미 로그인 된 경우
        if (Social.localUser.authenticated == true)
        {
            BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // 로그인 성공 -> 뒤끝 서버에 획득한 구글 토큰으로 가입 요청
                    BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");
                }
                else
                {
                    // 로그인 실패
                    Debug.Log("Login failed for some reason");
                }
            });
        }
    }

    // 구글 토큰 받아옴
    public string GetTokens()
    {
        //if (PlayGamesPlatform.Instance.localUser.authenticated)
        //{
        //    // 유저 토큰 받기 첫 번째 방법
        //    string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
        //    // 두 번째 방법
        //    // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        //    return _IDtoken;
        //}
        //else
        //{
        //    Debug.Log("접속되어 있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
        //    return null;
        //}
        return null;
    }
    // ************************************************************
    #endregion

    #region Migration
    // ******************** 마이그레이션 ********************
    // 마이그레이션 : 커스텀계정(게스트계정)을 페더레이션 계정으로 전환하는 과정
    public void Migration()
    {
        //Backend.BMember.ChangeCustomToFederation("federationToken", FederationType.Google, callback =>
        //{
        //    if (callback.IsSuccess())
        //    {
        //        Debug.Log("로그인 타입 전환에 성공했습니다");
        //    }
        //});
    }
    // ************************************************************
    #endregion

    #region Logout & Withdraw
    // 로그아웃
    // 게스트로그인의 경우 로그아웃을 하면 계정을 찾을 수 없다
    public void LogOut()
    {
        Backend.BMember.Logout((callback) =>
        {
            // 이후 처리
        });
    }

    //즉시 탈퇴
    public void Withdraw()
    {
        Backend.BMember.WithdrawAccount(callback =>
        {
            // 이후 처리
        });
    }
    #endregion

    #region GetGuestID
    // 게스트계정의 ID를 가져오는 함수
    public string GetGuestID()
    {
        StringBuilder result = new StringBuilder();

        result.Append(Backend.BMember.GetGuestID());

        return result.ToString();
    }
    #endregion

}