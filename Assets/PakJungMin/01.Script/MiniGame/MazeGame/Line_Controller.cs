using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line_Controller : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> vertex_List;
    private bool isDrawing = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 0;
        vertex_List = new List<Vector3>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            vertex_List.Clear();
            lineRenderer.positionCount = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            vertex_List.Clear();
            lineRenderer.positionCount = 0;
            isDrawing = false;
        }

        if (isDrawing)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,1f));
            if (vertex_List.Count == 0 || Vector3.Distance(vertex_List[vertex_List.Count - 1], mousePosition) > 0.1f)
            {
                vertex_List.Add(mousePosition);
                lineRenderer.positionCount = vertex_List.Count;
                lineRenderer.SetPositions(vertex_List.ToArray());
            }
        }
    }
}

