using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{

    [SerializeField] Transform[] backGround;
    [SerializeField] float scrollSpeed;
    [SerializeField] float offset;

    private void Update()
    {
        for (int i = 0; i < backGround.Length; i++)
        {
            backGround[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World);

            // 범위에 벗어나면 이동시킴
            if (backGround[i].position.x < -offset)
            {
                Vector2 pos = new Vector2(offset, backGround[i].position.y);
                backGround[i].position = pos;
            }
        }
    }
}
