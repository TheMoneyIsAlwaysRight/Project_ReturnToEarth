using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractPuzzle : PuzzleActiveInteract
{
    [SerializeField] SpriteRenderer marble;
    [SerializeField] Sprite changeMarble;
    [SerializeField] List<SpriteRenderer> renders;
    [SerializeField] List<Sprite> sprites;
    
    public void SetLeverSprite(LeverGame.LeverState left, LeverGame.LeverState middle, LeverGame.LeverState right,bool isClear)
    {
        renders[0].sprite = sprites[(int)left];
        renders[1].sprite = sprites[3 + (int)middle];
        renders[2].sprite = sprites[6 + (int)right];

        if (isClear == true)
        {
            marble.sprite = changeMarble;
        }
    }

    public void OffMarble()
    {
        marble.gameObject.SetActive(false);
    }

    public override void Load()
    {
        marble.sprite = changeMarble;
        //정답레버로 set해야함
        //SetLeverSprite();
    }
}
