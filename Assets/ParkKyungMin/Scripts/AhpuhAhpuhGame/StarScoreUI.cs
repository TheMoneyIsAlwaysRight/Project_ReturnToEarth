using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarScoreUI : MonoBehaviour
{
    [SerializeField] AhpuhPlayerController playerController;
    [SerializeField] TextMeshProUGUI starText;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        starText.text = playerController.starPoint.ToString();
    }
}
