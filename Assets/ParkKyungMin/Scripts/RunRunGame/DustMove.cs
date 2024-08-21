using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

}
