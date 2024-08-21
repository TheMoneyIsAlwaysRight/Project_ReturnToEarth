using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

/***********************************************************************************************************
                                    **** Class ****                                  
        - 스테이지 진행 상황 담당 매니저
 **********************************************************************************************************/

//Class : 스테이지 당 게임 플로우 매니저 클래스
public class GameFlowManager : MonoBehaviour
{
    /*******************************************************************************
                                **** Field ****                                
    ********************************************************************************/
    static GameFlowManager instance;
    public static GameFlowManager Inst { get { return instance; } }
    public Dictionary<int, GameObject> ClueDic = new Dictionary<int, GameObject>(); // 현재 스테이지의 단서 딕셔너리
    public bool isStageClear; //스테이지 클리어 여부


    /*******************************************************************************
                                **** Method ****
    ********************************************************************************/

    //Method : 스테이지 진행상황 검사 후 클리어 여부 반환 함수
    public void CheckGameFlow()
    {
        // 단서 딕셔너리에서 클리어하지 않은 단서 있는지 탐색. 반환 객체가 기본값일 경우 없음.
        KeyValuePair<int, GameObject> clueObject = ClueDic.FirstOrDefault(item => (!item.Value.GetComponent<Clue_Object>().IsClear));
       

        //클리어하지 않은 단서가 있다면, 즉 기본값이 아닐 경우.
        if (!clueObject.Equals(default(KeyValuePair<int, GameObject>)))
        {
            isStageClear = false;
        }
        //클리어하지 않은 단서가 없을 경우
        else if(clueObject.Equals(default(KeyValuePair<int, GameObject>)))
        {
            isStageClear = true;
        }
        
        //스테이지를 클리어했다면, 현재 스테이지의 단서 딕셔너리 초기화 및 종료.
        if (isStageClear)
        {
            Manager.Game.Clear();
            ClueDic.Clear();
            return;
        }

    }
    /*******************************************************************************
                                ****Fuction Flow****                              
    ********************************************************************************/
    private void Start()
    {
        if (instance != null) { Destroy(gameObject); }
        instance = this;
    }

}