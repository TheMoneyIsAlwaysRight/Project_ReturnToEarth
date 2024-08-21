using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
/************************************************************************************
                             **** Class Fuction Logic****
                                    
                                    
               - 퍼즐을 활성화/비활성화.

               - 퍼즐 해제 시, UNLOCKED 텍스트 UI On/Off 기능

                                    
**************************************************************************************/
/// <summary>
/// Class : 모든 퍼즐이 상속받아야하는 기반 클래스.
/// </summary>
public class BasePuzzle : MonoBehaviour
{

    /******************************************************************************   
                           **** Field ****

     disableTimer  : 비활성화 되기 까지 함수.
     locked_Text   : Locked/UNLOCKED UI의 텍스트
     isPuzzleClear : 퍼즐 해제 여부
     PuzzleObject  : 퍼즐이 나오는 인터렉트 오브젝트 참조 프로퍼티 --> 권세롬 추가
     IsPuzzleClear : 퍼즐 해제 시, UNLOCKED UI 활성화 및 비활성화 코루틴 실행 프로퍼티.
     
     ******************************************************************************/

    [SerializeField] float disableTimer;
    [SerializeField] TMP_Text locked_Text;
    [SerializeField] bool isPuzzleClear;

    //Property : 퍼즐이 나오는 인터렉트 오브젝트 참조 프로퍼티 --> 권새롬 추가. 
    public PuzzleActiveInteract PuzzleObject { get; set; }

    //Property : 퍼즐 해제 시, UNLOCKED 텍스트 UI 활성화 및 퍼즐 비활성화 코루틴 실행 프로퍼티
    public bool IsPuzzleClear
    {
        get { return isPuzzleClear; }
        set
        {
            isPuzzleClear = value;
            if (isPuzzleClear)
            {
                //------------------------------------------> 권새롬 추가. 퍼즐이 켜지는 상호작용 오브젝트가 연결되어있으면 Clue 클리어 체크.
                    if (PuzzleObject != null)
                        PuzzleObject.CompletePuzzle();
                //-------------------------------------------
                if (locked_Text != null)
                {
                    locked_Text.color = Color.green;
                    locked_Text.text = $"Unlock";
                }
                    StartCoroutine(ShowUnlocked());
            }
        } 
    }


    /******************************************************************************    
                               **** Method ****
        
        ShowUnlock : 퍼즐을 푼 후 일정시간 뒤에 닫히는 코루틴
        SetGameOff : 미니게임 비활성화 함수
            
     ******************************************************************************/

    //Method : 일정시간 뒤 퍼즐 비활성화 코루틴
    IEnumerator ShowUnlocked()
    {
        while (true)
        {
            disableTimer += Time.deltaTime;
            yield return null;
            if (disableTimer >= 1f)
            {
                SetGameOff();
                disableTimer = 0;
                break;
            }
        }
    }

    //Method : 미니게임 비활성화
    public virtual void SetGameOff()
    {
        Manager.Game.Camera.CurrentCamState = CameraController.CameraState.Base; //미니 게임 비활성화 시, 카메라 위치 재조정.

        //---------------------------------------------------------------------------------------------
        if (Manager.Game.Inter == null)
        {
            Manager.Game.Inter = GameObject.Find("TouchController").GetComponent<InteractController>();
        }
        //---------------------------------------------------------------------------------------------

        Manager.Game.Inter.CloseInteract();

        PuzzleManager.Inst.EndPuzzle(gameObject.name);
    }






}
