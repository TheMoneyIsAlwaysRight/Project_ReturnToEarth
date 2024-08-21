using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ScreenShotManager : MonoBehaviour
{
    [Header("플래시")]
    [SerializeField] Image img_flash;
    [Header("스크린샷 앨범")]
    [SerializeField] Image screenShot;
    [Header("세팅 UI")]
    [SerializeField] SettingUI setting;

    [Header("프론트 인게임 UI")]
    [SerializeField] GameObject frontUI;
    [Header("백 인게임 UI")]
    [SerializeField] GameObject backUI;
    [Header("인벤토리 UI")]
    [SerializeField] GameObject invenUI; 
    [Header("사진 액자에 저장된 스크린샷들")]
    public Image[] Album = new Image[3]; //기본값 3개로 하드코딩.

    Color flash_Color;

    /******************************************************************************
                                 **** Method ****
    *******************************************************************************/

    //Method : 촬영 버튼에 부여 함수.
    public void GetScreenShot()
    {
        StartCoroutine(ScreenShot_Flow());
    }
    //Coroutine : 촬영 과정 코루틴
    IEnumerator ScreenShot_Flow()
    {
        //설정 창 비활성화.
        setting.SettingOut();
        frontUI.SetActive(false);
        backUI.SetActive(false);
        invenUI.SetActive(false);

        yield return new WaitForEndOfFrame();
        
        //촬영 및 플래시 효과
        Capture(); 
        img_flash.gameObject.SetActive(true);
        Flash();


        //설정 창 재활성화
        setting.OnSetting();
        frontUI.SetActive(true);
        backUI.SetActive(true);
        invenUI.SetActive(true);
    }

    //Method : 화면 촬영 후 앨범에 저장 함수
    void Capture()
    {
        //인벤토리 크기를 제외한 화면 크기만큼의 직사각형 및 이를 촬영할 텍스처 준비
        Rect area = new Rect(0f,170f, Screen.width, Screen.height-170f);
        Texture2D captureTexture = new Texture2D((int)area.width,(int)area.height, TextureFormat.RGB24, false);

        //직사각형 크기만큼의 픽셀을 읽고, 이를 텍스처에 저장
        captureTexture.ReadPixels(area, 0,0);
        captureTexture.Apply();

        //텍스처에서 스프라이트 추출
        Sprite newSprite = Sprite.Create(captureTexture, new Rect(0, 0, captureTexture.width, captureTexture.height), Vector2.one * 0.5f);

        //앨범의 빈 스프라이트에, 추출한 스프라이트 할당
        if (Array.Exists(Album, (item => item.sprite == null)))
        {
            foreach (Image a in Album)
            {
                if (a.sprite == null)
                {
                    a.sprite = newSprite;

                    return;
                }
            }
        }
        else
        {
            Album[0].sprite = newSprite;
        }
    }

    //Method : 촬영 시 플래시 역할 함수
    public void Flash()
    {
        Color prev_Color = img_flash.color;

        //화면을 가득덮는 흰색 이미지 오브젝트 활성화로 촬영 플래시 구현.
        flash_Color.r = 255;
        flash_Color.g = 255;
        flash_Color.b = 255;
        flash_Color.a = 255;

        //화면 페이드 아웃 실행
        StartCoroutine(Fadeout_Screen());

    }

    //Method : 촬영 후 페이드 아웃 효과로 원래 화면으로 되돌리는 함수.
    IEnumerator Fadeout_Screen()
    {
        while (true)
        {
            img_flash.color = new Color(flash_Color.r, flash_Color.g, flash_Color.b, flash_Color.a);
            yield return null;
            flash_Color.a--;
            if (flash_Color.a <= 0)
            {
                img_flash.gameObject.SetActive(false);
                break;
            }
        }
    }
}