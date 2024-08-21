using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScrolling : MonoBehaviour
{
    [SerializeField] Transform[] walls;
    [SerializeField] float scrollSpeed;
    [SerializeField] float offset;

    private void Update()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World);

            // 범위에 벗어나면 이동시킴
            if (walls[i].position.x < -offset)
            {
                Vector2 pos = new Vector2(offset, walls[i].position.y);
                walls[i].position = pos;
            }
        }
    }
}
