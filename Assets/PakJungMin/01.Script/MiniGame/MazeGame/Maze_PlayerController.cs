using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Maze_PlayerController : MonoBehaviour { 


    Vector3 xPos;
    Vector3 yPos;

    void Update()
    {
        ListenInput();
    }

    private void ListenInput()
    {
        if (Input.touchCount > 0)
        {
            // 터치 입력 시,
            Touch touch = Input.GetTouch(0);       // only touch 0 is used
            if (touch.phase == TouchPhase.Moved)
            {
                //터치된 좌표를 월드 공간 좌표로 변환 후 그 좌표에서 수직으로 레이캐스트 발사
                yPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(yPos, Vector2.zero);
                //플레이어가 레이캐스트 대상이라면, 터치 좌표로 플레이어 좌표를 변경
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Maze_PlayerController>())
                    {
                        gameObject.transform.position = new Vector3(yPos.x, yPos.y, 0.5f);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스 입력 시,
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == GetComponent<BoxCollider>())
                    gameObject.transform.position = new Vector3(yPos.x, yPos.y, 0.5f);
            }
        }
    }
}
