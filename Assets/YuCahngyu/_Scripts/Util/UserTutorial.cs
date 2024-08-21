using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UserTutorial : PopUpUI
{
    [SerializeField] VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = Manager.Resource.Load<VideoClip>("tutorial");
        videoPlayer.Play();
    }

    public void ExitButton()
    {
        Manager.UI.ClosePopUpUI();
    }
}
