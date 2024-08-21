using BackEnd;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using static Define;

public class RandomInitInteract : MonoBehaviour
{
    [Serializable]
    public class ImagesContainer
    {
        public int Num;
        public List<Sprite> images;
    }

    [SerializeField] List<ImagesContainer> imagesContainer;
    [SerializeField] bool isStartInit;

    [Header("랜덤값 할당할때 겹치는건지 할당할 것인지 체크")]
    [SerializeField] bool isRandomUnique;

    private int interactId; 
    private SpriteRenderer[] renderers;
    private bool[] isCheck;
    private int spriteCount; // 하나의 이미지당 랜덤 스프라이트의 갯수

    //선택된 정보(나중에 퍼즐풀때 적용)
    private List<object> selectInfo = new List<object>();
    public List<object> SelectInfo { get { return selectInfo; } }

    private void Awake()
    {
        spriteCount = imagesContainer[0].images.Count;
        interactId = int.Parse(gameObject.name.Substring(0, 6));
        renderers = GetComponentsInChildren<SpriteRenderer>();
        if(isStartInit)
            RandomInit();
    }

    public void RandomInit()
    {
        if(isRandomUnique)
        {
            isCheck = new bool[spriteCount];
        }

        int randomNum = -1;
        for (int i=0;i< renderers.Length; i++)
        {
            randomNum = UnityEngine.Random.Range(0, spriteCount);
            if (isRandomUnique)
            {
                while (true)
                {
                    if (isCheck[randomNum] == false)
                    {
                        isCheck[randomNum] = true;
                        break;
                    }
                    randomNum = UnityEngine.Random.Range(0, spriteCount);
                }
            }
            renderers[i].sprite = imagesContainer[i].images[randomNum];
            selectInfo.Add(randomNum);
        }

        PuzzleManager.Inst.CorrectAnswers.Add(interactId, selectInfo);
    }

}
