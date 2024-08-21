using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    /* 
        << 미로 매니저 >>>

        **********************************************************************************************************
        
        <특징> 

            0. 싱글턴 패턴

            1. 이동가능한 곳,방문한 곳을 기록하는 walkable,visit 리스트.

            2. 백트래킹 기법을 위한 backTack 스택 자료구조

            3. 무작위 미로 생성 메서드.


        <구현 로직> 
        
        -무작위 미로 생성 메서드 로직-
        
            사용한 알고리즘 : 깊이 우선 탐색(DFS),백트래킹

            0. 외부 벽 혹은 아예 뚫지 못하는 타일을 의미하는 IsWall 체크가 된 타일들을 제외한 모든 타일들을 walkable 배열에 저장한다.

            1. 시작 위치를 visit,backTrack에 기록한다.

            2. 현재 위치를 기준으로 동,서,남,북의 타일을 검사하여, 이동 가능,불가능 여부를 판단한다.

            4. 갈 수 있다면, 그 쪽으로 이동하고 가로막는 타일 간의 벽을 제거하고, 방문한 타일을 BackTrack과 visited에 추가한다.

            5. 만약 이동할 수 없는 경우, BackTrack에서 Pop하여, 이전의 타일로 차례차례 돌아가 길을 찾는다.

            6. 모든 walkable 타일이 체크되었다면, 미로를 모두 만든 것으로 판단하고 종료한다. 
        
        **********************************************************************************************************

*/



    static MazeManager instance;
    public static MazeManager mazeManager { get { return instance; } }

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }

        instance = this;
    }

    //Enum : 방향을 표시하는 열거형.
    public enum MazeDir
    {
        Up,
        Down,
        left,
        Right
    }

    List<Tile> walkableList; //방문하지 않은 타일들
    List<Tile> visited; //방문했던 타일들
    Stack<Tile> backTrack; //방문한 타일들을 순차적으로 기록한 스택.
    string nowTile;

    /*
        Method : 무작위 미로 생성 메소드
    */
    public void CreateMaze()
    {
        //0. 방문하지 않은 모든 타일 개수만큼 배열 생성.
        backTrack = new Stack<Tile>();
        visited = new List<Tile>();
        walkableList = new List<Tile>();


        //1. 이동 가능한 타일들을 walkableList에서 삽입.
        foreach (KeyValuePair<string, Tile> tile in TileManager.tileManager.tileDir)
        {
            if (!tile.Value.IsWall)
            {
                walkableList.Add(tile.Value);
            }
        }

        //2. 랜덤한 타일을 시작 위치로 결정하고 visit,backTrack에 기록한다.
        int count = walkableList.Count;
        int randomNumber = Random.Range(0, walkableList.Count + 1);
        Tile startTile = TileManager.tileManager.Tiles[randomNumber];
        nowTile = startTile.gameObject.name;
        VisitTile(startTile);

        //3. 현재 위치를 기준으로 길을 탐색.길을 찾아낼 떄마다 walkable의 요소를 제거해간다.
        Tile newtile = RandomDir(startTile);

        //4. walkable가 0, 즉 모든 이동가능한 타일을 검사했을 경우, 미로를 다 만든 것 간주하고 종료한다.
        while (walkableList.Count > 0)
        {
            if (newtile == null) { break; }
            newtile = RandomDir(newtile);
        }

    }

    /* 
        Method : 현재 타일에서 무작위로 4방향 중 한 방향을 선택하고, 갈 수 있으면 가게하는 메소드.
    */
    public Tile RandomDir(Tile nowTile)
    {
        bool canMove = false;
        Tile nextTile;
        Tile resultTile;

        bool upDir = true;
        bool downDir = true;
        bool leftDir = true;
        bool rightDir = true;


        while (!canMove)
        {
            //0. 현재 위치인 nowTile을 기준으로 무작위 확률로 4방향을 결정한다.
            MazeDir randomDir = (MazeDir)Random.Range(0, 5);


            //만일 4방향이 전부 이동이 불가능하다면 이전 타일로 순차적으로 되돌아가, 길을 다시 찾는다.
            if ((!upDir && !downDir) && (!leftDir && !rightDir))
            {
                if (backTrack.Count == 0) { return null; }
                return backTrack.Pop();
            }
            switch (randomDir)
            {
                // 타일이 존재하지 않거나, 이미 방문했거나, 이동 불가능한 타일들은 제외.
                case MazeDir.Up:
                    // 타일이 실제로 존재하는 타일인지 검사.
                    if (!TileManager.tileManager.tileDir.ContainsKey($"{nowTile.PosX},{nowTile.PosY + 1}"))
                    {
                        upDir = false;
                        continue;
                    }
                    //그 타일이 walkableList에 있는지 검사.
                    if (!walkableList.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX},{nowTile.PosY + 1}"]))
                    {
                        upDir = false;
                        continue;
                    }
                    //그 타일이 이전에 이미 방문했었는지 검사.
                    if (visited.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX},{nowTile.PosY + 1}"]))
                    {
                        upDir = false;
                        continue;
                    }
                    
                    //목표타일을 이동가능한 타일로 판정하고 다음 이동 타일로 지정한다.
                    nextTile = TileManager.tileManager.tileDir[$"{nowTile.PosX},{nowTile.PosY + 1}"];
                    //이동하려는 타일과 현재 타일을 가로막는 벽들을 제거한다.
                    nextTile.DestroyWall(Tile.WallDir.Down);
                    nowTile.DestroyWall(Tile.WallDir.Up);

                    VisitTile(nextTile);                    //방문 목록,스택에 이동가능한 타일 기록.
                    walkableList.Remove(nextTile);          //이동가능한 타일 리스트에서 이동한 타일을 제거한다.

                    canMove = true;
                    resultTile = nextTile;
                    return resultTile;

                case MazeDir.Down:

                    if (!TileManager.tileManager.tileDir.ContainsKey($"{nowTile.PosX},{nowTile.PosY - 1}"))
                    {
                        downDir = false;
                        continue;
                    }

                    if (!walkableList.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX},{nowTile.PosY - 1}"]))
                    {
                        downDir = false;
                        continue;
                    }
                    if (visited.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX},{nowTile.PosY - 1}"]))
                    {
                        downDir = false;
                        continue;
                    }
                    nextTile = TileManager.tileManager.tileDir[$"{nowTile.PosX},{nowTile.PosY - 1}"];
                    nextTile.DestroyWall(Tile.WallDir.Up);
                    nowTile.DestroyWall(Tile.WallDir.Down);
                    VisitTile(nextTile);
                    walkableList.Remove(nextTile);
                    canMove = true;
                    resultTile = nextTile;
                    return resultTile;
                case MazeDir.Right:

                    if (!TileManager.tileManager.tileDir.ContainsKey($"{nowTile.PosX + 1},{nowTile.PosY}"))
                    {
                        rightDir = false;
                        continue;
                    }

                    if (!walkableList.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX + 1},{nowTile.PosY}"]))
                    {
                        rightDir = false;
                        continue;
                    }
                    if (visited.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX + 1},{nowTile.PosY}"]))
                    {
                        rightDir = false;
                        continue;
                    }
                    nextTile = TileManager.tileManager.tileDir[$"{nowTile.PosX + 1},{nowTile.PosY}"];
                    nextTile.DestroyWall(Tile.WallDir.Left);
                    nowTile.DestroyWall(Tile.WallDir.Right);
                    VisitTile(nextTile);
                    walkableList.Remove(nextTile);
                    canMove = true;
                    resultTile = nextTile;
                    return resultTile;
                case MazeDir.left:

                    if (!TileManager.tileManager.tileDir.ContainsKey($"{nowTile.PosX - 1},{nowTile.PosY}"))
                    {
                        leftDir = false;
                        continue;
                    }

                    if (!walkableList.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX - 1},{nowTile.PosY}"]))
                    {
                        leftDir = false;
                        continue;
                    }
                    if (visited.Contains(TileManager.tileManager.tileDir[$"{nowTile.PosX - 1},{nowTile.PosY}"]))
                    {
                        leftDir = false;
                        continue;
                    }


                    nextTile = TileManager.tileManager.tileDir[$"{nowTile.PosX - 1},{nowTile.PosY}"];


                    nextTile.DestroyWall(Tile.WallDir.Right);
                    nowTile.DestroyWall(Tile.WallDir.Left);
                    VisitTile(nextTile);
                    walkableList.Remove(nextTile);
                    canMove = true;
                    resultTile = nextTile;
                    return resultTile;
            }
        }
        return null;

    }

    //Method : 방문한 타일은 Stack과 List에 삽입.
    public void VisitTile(Tile tile)
    {
        if (!visited.Contains(tile))
        {
            backTrack.Push(tile);
            visited.Add(tile);
            tile.ChangeWhite();
        }
    }


}
