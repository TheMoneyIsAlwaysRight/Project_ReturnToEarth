using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : Singleton<AssetBundleManager>
{
    enum VersionTableColumn // 버전 테이블 Column(열) 정보
    {
        fileName, // 번들 파일 명
        version, // 번들 버전 정보
        chapID, //해당 챕터 ID
        downloadLink, // 번들 설치 링크
        android_downloadLink // 안드로이드용 번들 설치 링크
    }

    private string serverVersionTableURL; // 서버 버전 테이블 접속 URL
    private string localVersionTablePath; // 로컬 버전 테이블 경로

    private string[,] serverVersionTable; // 서버 버전 테이블

    private AssetBundle curAssetBundle;
    public AssetBundle UIsBundle;
    
    private bool isLoad = false;
    private bool isAndroid;

    [Header("Patch Progress UI")]
    [SerializeField] private Transform patchScreen;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private TMP_Text loadingSubText;


    private void Start()
    {
        StartCoroutine(InitServerPatch()); // 패치 시작
    }

    public IEnumerator DevMode()
    {
        yield return InitServerPatch();
        yield return ChapterAssetBundleLoad(Manager.Game.CurStageKey, loadingText, loadingSubText);
    }

    IEnumerator InitServerPatch()
    {
        isAndroid = Application.platform == RuntimePlatform.Android;
        if (isLoad == false)
        {
            isLoad = true;
            serverVersionTableURL = "https://docs.google.com/spreadsheets/d/168qxhNls0P-ESdKLtUDAUNLBjTcombH4/export?format=csv"; // 
            yield return LoadAssetBundleProgress(); // 패치 시작
        }
    }

    /// <summary>
    /// 에셋 번들 패치 프로세스
    /// </summary>
    IEnumerator LoadAssetBundleProgress()
    {
        patchScreen.gameObject.SetActive(true);

        loadingText.text = "버전 정보를 받아오는 중";
        yield return CheckBundleVersion();

        loadingText.text = "게임 리소스를 받아오는 중";
        yield return ChapterAssetBundleLoad(0, loadingText, loadingSubText); //UIs 에셋번들을 들고오는 중
        
        //추가. -> 비디오 메모리에 올리기
        Manager.Resource.InitVideo();

        JsonPatchManager.Instance.PatchJson();
        patchScreen.gameObject.SetActive(false);
    }


    /// <summary>
    /// 서버 버전 테이블 저장
    /// </summary>
    IEnumerator CheckBundleVersion()
    {
        loadingSubText.text = "인터넷에 접속합니다.";

        // # 서버 버전 테이블 load
        using (UnityWebRequest v = UnityWebRequest.Get(serverVersionTableURL))
        {
            loadingSubText.text = "서버 버전 테이블을 조회합니다.";

            yield return v.SendWebRequest();

            string[] rows = v.downloadHandler.text.Split('\n');
            serverVersionTable = new string[rows.Length, rows[0].Split(',').Length];
            for (int r = 0; r < serverVersionTable.GetLength(0); r++)
            {
                string[] cols = rows[r].Split(',');
                for (int c = 0; c < serverVersionTable.GetLength(1); c++)
                {
                    serverVersionTable[r, c] = cols[c];
                }
            }

        }
    }
    
    //개별 번들 패치
    public IEnumerator ChapterAssetBundleLoad(int chapterID,TMP_Text loadingText,TMP_Text loadingSubText)
    {
        loadingText.text = "게임 로딩중";
        for (int row = 1; row < serverVersionTable.GetLength(0); row++)
        {
            loadingSubText.text = $"패치를 진행하고 있습니다.";

            string fileName = serverVersionTable[row, (int)VersionTableColumn.fileName];
            string version = serverVersionTable[row, (int)VersionTableColumn.version];
            string chapID = serverVersionTable[row, (int)VersionTableColumn.chapID];
            string downloadURL = serverVersionTable[row, (int)VersionTableColumn.downloadLink];
            string downloadURL_Android = serverVersionTable[row, (int)VersionTableColumn.android_downloadLink];

            if (int.Parse(chapID) != chapterID)
                continue;

            UnityWebRequest v;
            if (isAndroid)
                v = UnityWebRequestAssetBundle.GetAssetBundle(downloadURL_Android, uint.Parse(version), 0);
            else
                v = UnityWebRequestAssetBundle.GetAssetBundle(downloadURL, uint.Parse(version), 0);


            // 통신 진행 정도 확인
            v.SendWebRequest();
            while (!v.isDone) // 통신 끝날 때까지 진행
            {
                StringBuilder comment = new StringBuilder();
                comment.Append($"패치를 진행하고 있습니다.");
                int progress = (int)(v.downloadProgress * 100);
                if (progress != 0)
                    comment.Append($" {progress}%");
                loadingSubText.text = comment.ToString();
                yield return null;
            }


            curAssetBundle = DownloadHandlerAssetBundle.GetContent(v);
            Manager.Resource.Init(curAssetBundle);
            break;
        }
        if (chapterID == 0)
            UIsBundle = curAssetBundle;
        loadingSubText.text = "곧 게임이 시작됩니다.";
    }

}
