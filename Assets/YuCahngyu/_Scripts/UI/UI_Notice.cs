using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Notice : PopUpUI
{
    [SerializeField] TMP_Text notice;           // 추후 한영 전환을 위한 할당용
    [SerializeField] TMP_Text description;      // 추후 한영 전환을 위한 할당용

    public string NoticeText { get; set; }
    public string DescriptionText { get; set; }

    public void ClickBack()
    {
        Manager.UI.ClosePopUpUI();
    }

    public void Setting()
    {
        notice.text = NoticeText;
        description.text = DescriptionText;
    }
}
