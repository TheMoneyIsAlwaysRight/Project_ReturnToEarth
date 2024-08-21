using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SwipeController : MonoBehaviour
{
    [SerializeField] GameObject scrollbar;
    [SerializeField] UI_ChapterButton enter;
    [SerializeField] float scroll_pos = 0;
    [SerializeField] float[] pos;
    [SerializeField] StageSellect[] stages;

    public StageSellect[] Stages { get { return stages; } }
    public float Scroll_Pos { get { return scroll_pos; } set { scroll_pos = value; } }
    private void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + distance / 2 && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

        // 스크롤링 정도에 따른 버튼의 스테이지 활성화 유무
        if (enter.IsLoading)
            return;

        if (scroll_pos < 0.25f)
        {
            enter.StageID = stages[0].SceneID;
        }
        else if (0.25f <= scroll_pos && scroll_pos < 0.75f)
        {
            enter.StageID = stages[1].SceneID;
        }
        else   // 0.75 <= scroll_pos
        {
            enter.StageID = stages[2].SceneID;
        }
    }

}
