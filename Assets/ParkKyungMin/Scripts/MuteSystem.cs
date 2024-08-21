using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSystem : MonoBehaviour
{
    //====================찬규 추가=======================
    [SerializeField] Image button;
    [SerializeField] Sprite onSound;
    [SerializeField] Sprite offSound;
    //===================================================

    // 뮤트 상태
    // bool isMuted = false;
    // 게임 전체 뮤트 상태를 사운드 매니저에서 관리

    // 이전 볼륨 레벨을 저장
    //float previousBGMVolume;
    //float previousSFXVolume;

    private void Start()
    {
        if (Manager.Sound.IsMute)       // 뮤트면
        {
            button.sprite = offSound;
        }
        else
        {
            button.sprite = onSound;
        }
        // 현재 믹서 설정으로 이전 볼륨 초기화
        
    }

    public void ToggleMute()
    {
        // true면 false로 false면 true로 바꿔서 상태를 변경시켜줌
        Manager.Sound.IsMute = !Manager.Sound.IsMute;

        // 뮤트
        if (Manager.Sound.IsMute)
        {
            Manager.Sound.Mixer.SetFloat("MasterVolume", -80);
            button.sprite = offSound;
            //Manager.Sound.Mixer.SetFloat("BGMVolume", -80f);
            //Manager.Sound.Mixer.SetFloat("SFXVolume", -80f);
        }
        // 뮤트 해제
        else
        {
            Manager.Sound.Mixer.SetFloat("MasterVolume", 0);
            button.sprite = onSound;
            //Manager.Sound.Mixer.SetFloat("BGMVolume", previousBGMVolume);
            //Manager.Sound.Mixer.SetFloat("SFXVolume", previousSFXVolume);
        }
    }
}


