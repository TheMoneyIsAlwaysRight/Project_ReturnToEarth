using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Chapter : BaseUI
{
    #region Enums
    enum GameObjects
    {
        _2Chapter,
        _3Chapter,
        _4Chapter,
        _5Chapter,
        _HiddenChapter
    }
    enum Texts
    {
        EnterText,
        StoryText,
        StageText,

        _11RoomText,
        _12RoomText,
        _13RoomText,
        _21RoomText,
        _22RoomText,
        _23RoomText,
        _31RoomText,
        _32RoomText,
        _33RoomText,
        _41RoomText,
        _42RoomText,
        _43RoomText,
        _51RoomText,
        _52RoomText,
        _53RoomText,
        _61RoomText,
        _62RoomText,
        _63RoomText
    }
    enum StageImage
    {
        _11StageImage,
        _12StageImage,
        _13StageImage,
        _21StageImage,
        _22StageImage,
        _23StageImage,
        _31StageImage,
        _32StageImage,
        _33StageImage,
        _41StageImage,
        _42StageImage,
        _43StageImage,
        _51StageImage,
        _52StageImage,
        _53StageImage,
        _61StageImage,
        _62StageImage,
        _63StageImage
    }
    #endregion

    private void Start()
    {
        LocalUpdate();
        ImageUpdate();

        GetUI(GameObjects._2Chapter.ToString()).SetActive(false);
        GetUI(GameObjects._3Chapter.ToString()).SetActive(false);
        GetUI(GameObjects._4Chapter.ToString()).SetActive(false);
        GetUI(GameObjects._5Chapter.ToString()).SetActive(false);
        GetUI(GameObjects._HiddenChapter.ToString()).SetActive(false);        
    }

    public void ImageUpdate()
    {
        #region StageImageSet
        GetUI<Image>(StageImage._11StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101500");
        GetUI<Image>(StageImage._12StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101501");
        GetUI<Image>(StageImage._13StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101502");

        GetUI<Image>(StageImage._21StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101503");
        GetUI<Image>(StageImage._22StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101504");
        GetUI<Image>(StageImage._23StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101505");

        GetUI<Image>(StageImage._31StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101506");
        GetUI<Image>(StageImage._32StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101507");
        GetUI<Image>(StageImage._33StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101508");

        GetUI<Image>(StageImage._41StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101509");
        GetUI<Image>(StageImage._42StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101510");
        GetUI<Image>(StageImage._43StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101511");
        
        GetUI<Image>(StageImage._51StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101512");
        GetUI<Image>(StageImage._52StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101513");
        GetUI<Image>(StageImage._53StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101514");

        GetUI<Image>(StageImage._61StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101515");
        GetUI<Image>(StageImage._62StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101516");
        GetUI<Image>(StageImage._63StageImage.ToString()).sprite = Manager.Resource.Load<Sprite>("101517");
        #endregion
    }

    public override void LocalUpdate()
    {
        #region StageNameSet
        GetUI<TMP_Text>(Texts.EnterText.ToString()).text = LanguageSetting.GetLocaleText(102221);
        GetUI<TMP_Text>(Texts.StoryText.ToString()).text = LanguageSetting.GetLocaleText(102220);
        GetUI<TMP_Text>(Texts.StageText.ToString()).text = LanguageSetting.GetLocaleText(102219);

        GetUI<TMP_Text>(Texts._11RoomText.ToString()).text = LanguageSetting.GetLocaleText(102222);
        GetUI<TMP_Text>(Texts._12RoomText.ToString()).text = LanguageSetting.GetLocaleText(102223);
        GetUI<TMP_Text>(Texts._13RoomText.ToString()).text = LanguageSetting.GetLocaleText(102224);
        
        GetUI<TMP_Text>(Texts._21RoomText.ToString()).text = LanguageSetting.GetLocaleText(102225);
        GetUI<TMP_Text>(Texts._22RoomText.ToString()).text = LanguageSetting.GetLocaleText(102226);
        GetUI<TMP_Text>(Texts._23RoomText.ToString()).text = LanguageSetting.GetLocaleText(102227);

        GetUI<TMP_Text>(Texts._31RoomText.ToString()).text = LanguageSetting.GetLocaleText(102228);
        GetUI<TMP_Text>(Texts._32RoomText.ToString()).text = LanguageSetting.GetLocaleText(102229);
        GetUI<TMP_Text>(Texts._33RoomText.ToString()).text = LanguageSetting.GetLocaleText(102230);

        GetUI<TMP_Text>(Texts._41RoomText.ToString()).text = LanguageSetting.GetLocaleText(102231);
        GetUI<TMP_Text>(Texts._42RoomText.ToString()).text = LanguageSetting.GetLocaleText(102232);
        GetUI<TMP_Text>(Texts._43RoomText.ToString()).text = LanguageSetting.GetLocaleText(102233);

        GetUI<TMP_Text>(Texts._51RoomText.ToString()).text = LanguageSetting.GetLocaleText(102234);
        GetUI<TMP_Text>(Texts._52RoomText.ToString()).text = LanguageSetting.GetLocaleText(102235);
        GetUI<TMP_Text>(Texts._53RoomText.ToString()).text = LanguageSetting.GetLocaleText(102236);
        
        GetUI<TMP_Text>(Texts._61RoomText.ToString()).text = LanguageSetting.GetLocaleText(102463);
        GetUI<TMP_Text>(Texts._62RoomText.ToString()).text = LanguageSetting.GetLocaleText(102464);
        GetUI<TMP_Text>(Texts._63RoomText.ToString()).text = LanguageSetting.GetLocaleText(102465);
        #endregion
    }

}
