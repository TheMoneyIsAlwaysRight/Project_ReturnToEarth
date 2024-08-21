using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioMixer mixer;
    public AudioMixer Mixer { get { return mixer; } }

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource sfxPlayer = null;

    [SerializeField] bool isMute;        // true면 뮤트, false면 안뮤트
    public bool IsMute { get { return isMute; } set { isMute = value; } }

    private void Start()
    {
        isMute = false;
    }

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }


    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                sfxPlayer.clip = sfx[i].clip;
                sfxPlayer.PlayOneShot(sfxPlayer.clip);
            }
        }
    }
}