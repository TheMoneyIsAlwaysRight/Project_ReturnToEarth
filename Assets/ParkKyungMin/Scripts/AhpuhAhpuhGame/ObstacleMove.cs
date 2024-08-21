using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    
    private void Start()
    {
        Destroy(gameObject, 5);  // 5초뒤에 삭제되게 하면서 메모리절약
    }

    private void Update()
    {
        // 이동
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
