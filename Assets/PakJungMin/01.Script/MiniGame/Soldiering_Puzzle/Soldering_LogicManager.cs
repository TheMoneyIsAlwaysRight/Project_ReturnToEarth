using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldering_LogicManager : MonoBehaviour
{
    public Soldering_Puzzle puzzle;
    public List<Soldering_Object> soldObjects;

    public Canvas inPuzzleCanvas;
    public RectTransform rect_cursor;

    public float w;
    public float h;
    private void Awake()
    {
        soldObjects = new List<Soldering_Object>();
    }

    public void Update()
    {
        UpdateCursor();
    }
    
    public void UpdateCursor()
    {
        Vector2 mousePos = Input.mousePosition;
        rect_cursor.localPosition = mousePos + new Vector2(w, h);
    }
    public void CheckAnswer()
    {
        for(int x=0;x<soldObjects.Count;x++)
        {
            if (!soldObjects[x].IsClear)
            {
                return;
            }
        }

        puzzle.IsPuzzleClear = true;
    }


}
