using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DirectMoving : MonoBehaviour {

    [Tooltip("Moving speed on Y axis in local space")]
    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
  
}
