using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChapterManager : Singleton<ChapterManager>
{
    [SerializeField] List<int> serverData; // 서버에서 받아오는 데이터 리스트. 오로지 받기만 하는 용도. 서버에 올리는 용도 X
    [SerializeField] int[] localData;      // 서버 데이터를 복사한 런타임 데이터 배열 --> 서버 데이터는 가능하면 건드리지 않기 위함.
    [Header("히든 스테이지 리스트 : 유니티 에디터 상에서 할당")]
    [SerializeField] List<int> hiddenStages;
    [SerializeField] int score;          //현재까지 얻은 별의 개수.

    public List<int> openStage;          //스테이지 잠금 기록용 리스트.
    public int prevStage;                //스테이지 클리어 후 이전 스테이지 Key 저장 변수

    public List<int> Server { get { return serverData; } set { serverData = value; } }
    public int[] Local { get { return localData; } }
    public int Score { get { return score; } }

    //Method : 서버 데이터 다시 받아와서 데이터 새로고침
    public void RefreshData()
    {
        openStage.Clear();
        //서버 데이터 -> 로컬 데이터로 깊은 복사 및 배열화.
        Server = BackendGameData.userData.ClearStage;
        localData = Server.ToArray();
        SumScore();

        for (int x = 0; x < localData.Length; x++)
        {
            if (Local[x] != 0 && !openStage.Contains(108000 + (x + 1)))
            {
                openStage.Add(108000 + (x + 1));
            }
            else if (Local[x] == 0)
            {
                break;
            }

        }
        //1-1 스테이지는 기본으로 열리도록 설정.
        if (openStage.Count <= 0)
        {
            openStage.Add(108001);
        }
    }
    /*
       Method : 스테이지 클리어 후 클리어 여부 기록 메서드. GameManager.ShowClearUI()에서 이 함수를 호출
    */
    public void RecordData(int starScore)
    {
        int nowIndex = Manager.Game.CurStageKey - 108001;
        localData[nowIndex] = starScore;
        BackendGameData.Instance.ChangeClearStage(Manager.Game.CurStageKey, starScore);
        int nextStage = Manager.Game.CurStageKey + 1;

        //해금되어 있지 않은 다음 스테이지 해금
        if ((nowIndex+1 <= Local.Length) && !openStage.Contains(nextStage))
        {
            // 다음 스테이지의 히든 여부 판단 후 스테이지 해금
            if (hiddenStages.Contains(nextStage) && this.score >= 40) 
            {
                openStage.Add(nextStage);
                prevStage = Manager.Game.CurStageKey;
                BackendGameData.Instance.ChangeClearStage(nextStage, -1);
            }
            else
            {
                openStage.Add(nextStage);
                prevStage = Manager.Game.CurStageKey;
                BackendGameData.Instance.ChangeClearStage(nextStage, -1);
            }
        }

    }
    //Method : 챕터 씬에서 현재까지 얻은 별 개수 반환 함수
    public void SumScore()
    {
        score = 0;
        for (int x = 0; x < localData.Length; x++)
        {
            if (localData[x] <= 0) { continue; }
            score += localData[x];
        }
    }

    //Method : 챕터 씬에서 레벨 클리어 여부 확인용 메서드
    public int CheckClear()
    {
        //스테이지 클리어 여부 검사 및 잠금 처리
        if (!openStage.Contains(Manager.Game.CurStageKey))
        {
            return 0;
        }
        else
        {
            return Manager.Game.CurStageKey;
        }
    }


    [ContextMenu("Cheat")]
    //Method : 개발자 모드. 모든 챕터을 오픈 ---> 완성 전에 삭제.
    public void SetOnDevMode()
    {
        RecordData(3);
        //for(int x=0;x< localData.Length;x++)
        //{
        //    localData[x] = 3;
        //    if (!openStage.Contains(108001 + x))
        //    {
        //        openStage.Add(108001 + x);
        //    }
        //}

    }
}
