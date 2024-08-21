using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************************
                           **** Class ****                                    
                                                                                        
        - 모든 퍼즐을 관리하는 싱글턴 패턴 매니저                                                                                                                                        
        - 모든 퍼즐을 담은 퍼즐 딕셔너리                                                                                                                                         
        - 퍼즐을 활성화/비활성화 함수                                                                                                                                             
        - 퍼즐 활성화 시, 백그라운드 배경 관리 함수.                                       
                                                                                        
********************************************************************************************/
public class PuzzleManager : MonoBehaviour
{

    /***************************************************************************************
                                 **** Field ****                        
    ****************************************************************************************/
    static PuzzleManager instance;                                                              //싱글턴 패턴 객체 변수
    private GameObject img_bg;                                                                  // 퍼즐 배경 게임 오브젝트
    private GameObject puzzle_Canvas;                                                           //퍼즐 캔버스
    [SerializeField] SettingUI settingUI;                                                       //설정 윈도우 창 객체  
    public Dictionary<string, GameObject> puzzleDic = new Dictionary<string, GameObject>();     //모든 퍼즐 정보를 담은 딕셔너리

    private UI_Puzzle puzzleUI;                                                                 //새롬 추가 --> 모든 퍼즐의 상위 오브젝트
    public Dictionary<int, List<object>> CorrectAnswers = new Dictionary<int, List<object>>();  //새롬 추가 --> 퍼즐의 정답을 저장함
    public static PuzzleManager Inst { get { return instance; } }                               //싱글턴 패턴의 매니저 접근 프로퍼티





    /***************************************************************************************
                               **** Method **** 
    ****************************************************************************************/

    //Method : 딕셔너리에 저장된 퍼즐을 호출 및 활성화 함수
    public void PlayPuzzle(string puzzleName)
    {
        if (puzzleDic.ContainsKey(puzzleName) && !puzzleDic[puzzleName].GetComponent<BasePuzzle>().IsPuzzleClear)
        {
            //---> 권새롬 추가 : 랜더모드가 Overlay 모드일때는 잔상을 없애기 위한 코드.
            CanvasGroup cg = puzzleDic[puzzleName].GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
            //---------------------------------------------------------------

            puzzleDic[puzzleName].SetActive(true);
            SetBG(true);
        }
    }
    //Method : 딕셔너리의 저장된 퍼즐을 비활성화 함수
    public void EndPuzzle(string puzzleName)
    {
        if (puzzleDic.ContainsKey(puzzleName))
        {
            //---> 권새롬 추가 : 랜더모드가 Overlay 모드일때는 잔상을 없애기 위한 코드.
            CanvasGroup cg = puzzleDic[puzzleName].GetComponent<CanvasGroup>();
            if (cg != null)
            {
                StartCoroutine(FixedAfterImage(cg,puzzleDic[puzzleName]));
                return;
            }
            //---------------------------------------------------------------

            puzzleDic[puzzleName].SetActive(false);
            SetBG(false);
        }
    }

    IEnumerator FixedAfterImage(CanvasGroup cg ,GameObject puzzle)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        yield return new WaitForSecondsRealtime(0.1f);
        SetBG(false);
        puzzle.SetActive(false);
    }
    //Method : 퍼즐 실행 시 배경의 검은 패널을 관리하는 함수
    public void SetBG(bool active)
    {
       puzzleUI.SetImage(active); // 권새롬 수정. 기능은 똑같음. background 접근해서 껏다 켜기

    }

    // Method : 새롬 추가 --> 퍼즐 Load한것을 Set함
    public void SetPuzzle(GameObject canvas) 
    {
        puzzle_Canvas = canvas;
        puzzleUI = puzzle_Canvas.GetComponent<UI_Puzzle>();
        if (puzzle_Canvas != null)
        {
            BasePuzzle[] array = puzzle_Canvas.GetComponentsInChildren<BasePuzzle>(true);
            foreach (BasePuzzle puzzle in array)
            {
                puzzleDic.Add(puzzle.gameObject.name, puzzle.gameObject);
            }
        }
    }

    /***************************************************************************************
                               **** Unity Flow ****                             
    ****************************************************************************************/
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        instance = this;
    }

}
