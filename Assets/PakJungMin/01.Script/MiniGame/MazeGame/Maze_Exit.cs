using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Exit : MonoBehaviour
{
    //권새롬 추가 --> 퍼즐과 연결하기 위한 변수
    public Maze_Puzzle Puzzle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Maze_PlayerController>())
        {
            GetComponentInParent<MazeGame>().IsPuzzleClear = true;
            Puzzle.CompleteGame(); //권새롬 추가 --> 퍼즐UI도 complete되어야함.
        }
    }
}
