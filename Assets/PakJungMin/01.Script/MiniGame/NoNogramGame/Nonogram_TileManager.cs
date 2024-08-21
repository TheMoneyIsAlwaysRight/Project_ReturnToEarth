using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
/**************************************************************************************************
                                    **** Class Fuctions Logic****                                        

    문제를 만들기 위해선, 유니티 에디터 상에서, 각각의 타일마다 isKey 필드를 체크해야한다.

    -Awake 개괄- 

        0. 최하단 좌측 타일을 (0,0)으로 간주하고, 순서대로 오브젝트명을 (x,y) 순서쌍으로 매겨나간다. 기본값은 15x15.
        1. 노노그램 맵 상의 모든 타일을 관리하는 타일 딕셔너리(TileDic)에 저장한다.
        2. 비워두는 타일,퀴즈 타일을 제외한 모든 타일을 타일 딕셔너리(PlayeDic)에 저장한다.

    -Start 개괄-

        0. 모든 타일의 Text를 ""로 대치한다.
        1. SetVerticalQuiz(),SetHorizonalQuiz() 함수를 통해, 문제 타일에 문제를 할당한다.


    -문제 할당 개괄-

        0. 0부터 시작하여 최대 열/행 수까지 행/열을 순서대로 검사한다.
        1. 비워두는 칸임을 뜻하는 isBlank가 체크된 타일이 있는 열/행은 검사를 건너뛴다.
        2. 열(위에서 아래)검사/행(왼쪽에서 오른쪽)검사 전에 퀴즈 타일/상호작용 타일을 분리해 각각의 준비된 리스트에 순서대로 저장한다.
        3. 상호작용 타일을 순차 탐색하면서, 빈 곳/문제 체크된 여부를 검사하며, 체크된 곳의 연속여부 및 반대의 경우를 모두 검사하고, 정수형 변수 배열로 저장한다.
        4. 퀴즈 타일에 뒤에서부터,정수형 변수 배열의 앞에서부터 순서대로 저장한다.

***************************************************************************************************/
/// <summary>
/// Class : 노노그램 퍼즐 매니저
/// </summary>
public class Nonogram_TileManager : MonoBehaviour
{
    [SerializeField] NonogramGame game;
    [Header("노노그램 전체맵이 몇 X 몇 ? : 기본값 15x15")]
    [SerializeField] int lengthX;
    [SerializeField] int lengthY;

    Nonogram_Tile[] tileArray;

    Dictionary<string, Nonogram_Tile> TileDic = new Dictionary<string, Nonogram_Tile>();
    Dictionary<string, Nonogram_Tile> PlayDic = new Dictionary<string, Nonogram_Tile>();

    /*******************************************************************************
                            ****Method****
                            
     - SetVerticalQuiz : 열에 문제 할당 함수
     - SetHorizonalQuiz : 행에 문제 할당 함수
     - CheckClearGame : 게임 클리어 여부 함수

    ********************************************************************************/
    void SetVerticalQuiz()
    {
        List<Nonogram_Tile> quizTile_List = new List<Nonogram_Tile>();
        List<Nonogram_Tile> playerTile_List = new List<Nonogram_Tile>();
        List<int> count_List = new List<int>();
        int count = 0;

        for (int x = 0; x < lengthX; x++)
        {
            Debug.Log($"현재 : {x}");
            if(quizTile_List.Count != 0 || playerTile_List.Count != 0)
            {
                count = 0;
                playerTile_List.Clear();
                quizTile_List.Clear();
                count_List.Clear();
            }

            for (int y = lengthX - 1; y >= 0; y--) //특정 열의 한 행씩 내려가며 검사한다.
            {
                if (TileDic[$"{x},{y}"].isBlank)//비워두는 칸이 있는 열은 검사할 필요 없으니, 다음 열 이동.
                {
                    Debug.Log($"{x}열은 검사할 필요가 없다.");
                    break;
                }
                else if (TileDic[$"{x},{y}"].isQuiz)//문제 칸일 경우 quiz 리스트 추가.
                {
                    quizTile_List.Add(TileDic[$"{x},{y}"]);
                }
                else if (!TileDic[$"{x},{y}"].isQuiz && !TileDic[$"{x},{y}"].isBlank)
                {
                    playerTile_List.Add(TileDic[$"{x},{y}"]);
                }
            }




            if (quizTile_List.Count > 0 && playerTile_List.Count > 0)
            {
                bool isExistKey = playerTile_List.Exists(item => item.isKey); //이 열에서 해답 타일이 있는지 확인한다.

                if (!isExistKey) //체크된 행이 하나도 없었을 때 
                {
                    Debug.Log($"{x} Line is Null");
                    quizTile_List[quizTile_List.Count - 1].GetComponentInChildren<TMP_Text>().text = "0";
                    continue;
                }

                //행을 하나하나 내려가며, isKey 여부를 체크한다.
                for (int index = 0; index < playerTile_List.Count; index++)
                {
                    if (playerTile_List[index].isKey) // 키면
                    {
                        count++; //카운트 증가.
                        if (index == playerTile_List.Count - 1)  //그런데 마지막 요소 도착 시
                        {
                            if (count > 0) // 기존의 카운트가 있었다면
                            {
                                count_List.Add(count); //추가하고 종료.
                                Debug.Log($"111.{x}열 {count}가 추가됨");
                            }
                            count = 0;
                        }
                    }
                    else if (!playerTile_List[index].isKey) // 키가 아니면
                    {
                        if (count > 0) //기존의 카운팅이 있었다면.
                        {
                            count_List.Add(count);

                            Debug.Log($"222.{x}열 {count}가 추가됨");
                            count = 0;
                        }

                    }
                }

            }
            Debug.Log($"{x}열 : 문제 개수 {count_List.Count}");
            Debug.Log($"{x}열 : 최대 문제 개수 {quizTile_List.Count}");

            for (int alpha = 0; alpha < count_List.Count; alpha++)
            {
                quizTile_List[quizTile_List.Count - (alpha + 1)].GetComponentInChildren<TMP_Text>().text = count_List[count_List.Count -( alpha +1 ) ].ToString();

            }
        }


    }

    void SetHorizonalQuiz()
    {
        List<Nonogram_Tile> quizTile_List = new List<Nonogram_Tile>();
        List<Nonogram_Tile> playerTile_List = new List<Nonogram_Tile>();
        List<int> count_List = new List<int>();
        int count = 0;

        for (int y = lengthY - 1; y >= 0; y--)
        {
            if (quizTile_List.Count != 0 || playerTile_List.Count != 0)
            {
                count = 0;
                playerTile_List.Clear();
                quizTile_List.Clear();
                count_List.Clear();
            }

            for (int x = 0; x < lengthX; x++) //특정 열의 한 행씩 내려가며 검사한다.
            {
                if (TileDic[$"{x},{y}"].isBlank)//비워두는 칸이 있는 열은 검사할 필요 없으니, 다음 열 이동.
                {
                    Debug.Log($"{x}열은 검사할 필요가 없다.");
                    break;
                }
                else if (TileDic[$"{x},{y}"].isQuiz)//문제 칸일 경우 quiz 리스트 추가.
                {
                    quizTile_List.Add(TileDic[$"{x},{y}"]);
                }
                else if (!TileDic[$"{x},{y}"].isQuiz && !TileDic[$"{x},{y}"].isBlank)
                {
                    playerTile_List.Add(TileDic[$"{x},{y}"]);
                }
            }

            if (quizTile_List.Count > 0 && playerTile_List.Count > 0)
            {
                bool isExistKey = playerTile_List.Exists(item => item.isKey); //이 열에서 해답 타일이 있는지 확인한다.

                if (!isExistKey) //체크된 행이 하나도 없었을 때 
                {
                    quizTile_List[quizTile_List.Count - 1].GetComponentInChildren<TMP_Text>().text = "0";
                    continue;
                }

                //행을 하나하나 내려가며, isKey 여부를 체크한다.
                for (int index = 0; index < playerTile_List.Count; index++)
                {
                    if (playerTile_List[index].isKey) // 키면
                    {
                        count++; //카운트 증가.
                        if (index == playerTile_List.Count - 1)  //그런데 마지막 요소 도착 시
                        {
                            if (count > 0) // 기존의 카운트가 있었다면
                            {
                                count_List.Add(count); //추가하고 종료.
                            }
                            count = 0;
                        }
                    }
                    else if (!playerTile_List[index].isKey) // 키가 아니면
                    {
                        if (count > 0) //기존의 카운팅이 있었다면.
                        {
                            count_List.Add(count);

                            count = 0;
                        }

                    }
                }

            }
            for (int alpha = 0; alpha < count_List.Count; alpha++)
            {
                quizTile_List[quizTile_List.Count - (alpha + 1)].GetComponentInChildren<TMP_Text>().text = count_List[count_List.Count - (alpha + 1)].ToString();

            }
        }


    }

    public void CheckClearGame()
    {
        KeyValuePair<string, Nonogram_Tile> unClearTile = PlayDic.FirstOrDefault(item => item.Value.IsChecked != item.Value.isKey);
        //PlayDic에서 현재 문제의 해답과 플레이어가 체크한 답이 맞지 않은 상태의 타일을 찾는다.
        if (unClearTile.Equals(default(KeyValuePair<string, Nonogram_Tile>)))
        {
            Debug.Log("노노그램 클리어");

            //---> 권새롬 추가 : 아이템이 없어져야함. 
            Manager.Game.Inven.RemoveItem(106038);
            Manager.Game.Inven.RemoveItem(106039);
            Manager.Game.Inven.RemoveItem(106040);
            Manager.Game.Inven.SetInven();
            //------------------------------------------

            game.IsPuzzleClear = true;
        }
        else
        {
            //Debug.Log($"안풀린 문제가 있다 : {unClearTile.Value.gameObject.name}");
            return;
        }
    }


    /*******************************************************************************
                                ****Fuction Flow****
                                
    ********************************************************************************/

    private void Awake()
    {
        tileArray = GetComponentsInChildren<Nonogram_Tile>(); //자식의 모든 타일 오브젝트 가져온다.
        int x = 0;
        int y = lengthY - 1;
        /*
            맵 상의 모든 타일을 "{x},{y}" 식으로 이름을 변경하고, TileDic에 추가한다.
            이중에 isBlank,isQuiz가 체크되지 않은 타일은 PlayDic에도 추가한다.       
         */
        foreach (Nonogram_Tile tile in tileArray)
        {

            tile.gameObject.name = $"{x},{y}";

            TileDic.Add(tile.gameObject.name, tile);

            if (!tile.isBlank && !tile.isQuiz) { PlayDic.Add(tile.gameObject.name, tile); }

            x++;
            if (x >= lengthX)
            {
                x = 0;
                y--;
            }
        }
    }

    private void Start()
    {
        foreach (KeyValuePair<string, Nonogram_Tile> tile in TileDic)
        {
            //모든 타일들을 검사, 비워놓는 곳을 제외하곤 글자 ""로 변경
            if (!tile.Value.isBlank)
            {
                tile.Value.GetComponentInChildren<TMP_Text>().text = "";
            }
        }
        SetVerticalQuiz();
        SetHorizonalQuiz();
    }
}
