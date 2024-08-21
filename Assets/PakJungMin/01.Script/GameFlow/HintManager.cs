using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
/// <summary>
/// Class : 힌트 시스템 매니저
/// </summary>
public class HintManager : MonoBehaviour
{

    /***************************************************************************************
                                   **** Field ****

        ui_Isactivated : 힌트 윈도우 창 활성화/비활성화 여부
        hint_indicator : 오브젝트형 단서 찾아주는 힌트 표시기 게임 오브젝트
        hint_Panel     : 힌트 윈도우 창 게임 오브젝트
        hintCounttext  : 힌트 횟수 및 힌트 사용 여부 질문 텍스트
        hint_Count     : 현재 남은 힌트 횟수

    ****************************************************************************************/

    static HintManager instance;
    public static HintManager Inst { get { return instance; } }


    [SerializeField] public GameObject hint_indicator;

    [SerializeField] GameObject hint_Panel;

    [SerializeField] TMP_Text hintCounttext;

    [SerializeField] int hintCount;

    [HideInInspector]
    public List<int> clueFlow = new List<int>();

    GameObject curHint;
    public bool is_Hint_Activated;
    bool is_Activated;


    /***************************************************************************************
                               **** Method ****
    ****************************************************************************************/

    //Method : 남은 힌트 횟수 출력, 힌트 창을 활성화/비활성화 함수.
    public void OpenCloseUI()
    {
        //힌트 창 활성화/비활성화
        is_Activated = !is_Activated;

        //활성화 시 힌트 횟수 출력
        if (is_Activated)
        {
            hint_Panel.SetActive(true);
            hintCounttext.text = $"Chance Count : {hintCount}";
        }
        else
        {
            hint_Panel.SetActive(false);
        }
    }

    //Method : 다음 힌트의 위치 반환 및 힌트 지시기를 위치시키는 함수. -----> UI 상 힌트 버튼에 연동시킬 것.
    public void GetHint()
    {
        // 힌트 지시기가 발동중이거나, 힌트 횟수가 없는 경우 종료
        if (is_Hint_Activated || hintCount <= 0)
        {
            OpenCloseUI();
            return;
        }

        // 현재 "해결해야할 단서리스트" 초기화
        clueFlow.Clear();

        // 단서 딕셔너리에서 클리어하지 않은 단서들만 찾아 "해결해야할 단서리스트"에 삽입.
        foreach (KeyValuePair<int, GameObject> clue in GameFlowManager.Inst.ClueDic)
        {
            if (!clue.Value.GetComponent<Clue_Object>().IsClear)
            {
                clueFlow.Add(clue.Key);
            }
        }

        // 아직 해결해야할 단서가 남아있다면
        if (clueFlow.Count > 0)
        {
            //현재 스테이지의 진행상황에서 우선순위가 높은 단서를 찾고, 힌트지시기 활성화.
            curHint = GameFlowManager.Inst.ClueDic[clueFlow.Min()];
            is_Hint_Activated = true;

            //단서가 현재 카메라 안에 있다면, 힌트 지시기 이동 및 힌트 차감. 아닐경우 종료.
            if (CheckView())
            {
                SetHintPos();
                hintCount--; //힌트 차감됨
            }
            else
            {
                hint_indicator.SetActive(false);
            }
            OpenCloseUI();

        }
        else
        {
            OpenCloseUI();
            return;
        }
    }
    //Method : 다음 힌트가 카메라 안에 있는 확인하는 함수.
    public bool CheckView()
    {
        //현재 진행상황에서 가장 우선 순위가 높은 단서 반환.
        GameObject target = GameFlowManager.Inst.ClueDic[clueFlow.Min()];
        //그 단서의 위치를 뷰포트 공간으로 좌표로 변환.
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.transform.position);

        //단서의 위치가 화면의 크기 상에서 내부에 존재여부 판단.
        bool result = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        
        return result;
    }
    //Method : 힌트 지시기를 다음 단서의 위치로 이동시키는 함수.
    public void SetHintPos()
    {
        //힌트 지시기 오브젝트 활성화 및, 힌트 지시기 이동
        if (curHint == GameFlowManager.Inst.ClueDic[clueFlow.Min()])
        {
            hint_indicator.SetActive(true);

            //단서의 Collier2D의 offset으로 힌트 지시기 이동.
            Vector3 moveVecter = curHint.GetComponent<Collider2D>().offset;
            hint_indicator.transform.position = moveVecter;
        }
    }

    /***************************************************************************************
                               **** Unity Flow ****
    ****************************************************************************************/

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}