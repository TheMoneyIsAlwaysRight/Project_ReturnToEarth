using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGameClear : MonoBehaviour
{
    [SerializeField] GameObject clearUI;     // 클리어 UI창

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            clearUI.SetActive(true);

            Manager.Chapter.RecordData(3); //정민 추가 : 스테이지 클리어 기록용.
            Time.timeScale = 0;
            MinigameStory.Instance.StartClearStory(); //새롬 추가 : 뒤에스토리 나오게
        }
    }
}
