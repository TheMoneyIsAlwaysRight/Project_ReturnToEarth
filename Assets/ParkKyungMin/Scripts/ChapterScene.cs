using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterScene : BaseScene
{
    [SerializeField] PopUpUI starUI;
    [SerializeField] PopUpUI heartUI;
    [SerializeField] TMP_Text starCount; //정민 추가. 현재까지 얻은 별 개수 업데이트 위함.

    private void Start()
    {
        Manager.Sound.StopBGM();
        Manager.Sound.PlayBGM("Login");
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    // ================= 찬규 추가 ====================
    public void MoveLoginScene()
    {
        Manager.Scene.LoadScene("LoginScene");
    }

    public void ClickStar()
    {
        // Manager.UI.ShowPopUpUI(starUI);
    }

    public void ClickHeart()
    {
        // Manager.UI.ShowPopUpUI(heartUI);
    }

    //정민 추가------------------------------------------------------------------------------

    //Method : 별 개수 업데이트 해주는 함수
    public void UpdateScore()
    {
        starCount.text = ChapterManager.Instance.Score.ToString();
    }
    private void OnEnable()
    {
        ChapterManager.Instance.SumScore();
        UpdateScore();
    }

    //---------------------------------------------------------------------------------------
}
