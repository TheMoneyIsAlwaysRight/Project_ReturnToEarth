using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] RawImage rawImage;

    public VideoPlayer VideoPlayer { get { return videoPlayer; } set { videoPlayer = value; } }
    public RawImage RawImage { get { return rawImage; } set { rawImage = value; } }

    public void Release()
    {
        RenderTexture texture = GetComponent<RawImage>().texture as RenderTexture;
        if (texture != null)
        {
            Debug.Log("Release");
            texture.Release();
        }
    }


}
