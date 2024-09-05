using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    [SerializeField] GameObject upWall;
    [SerializeField] GameObject downWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    int posX;
    int posY;

    public int PosX { get { return posX; } set { posX = value; } }
    public int PosY { get { return posY; } set { posY = value; } }


    //
    public enum WallDir
    {
        Up,
        Down,
        Left,
        Right

    }
    // Method : 타일의 이미지를 흰색으로 변경
    public void ChangeWhite()
    {
        Color color = new Color();
        color.r = 255f;
        color.b = 255f;
        color.g = 255f;
        color.a = 255f;
        sprite.color = color;
    }
    // Method : 타일의 이미지를 검은색으로 변경
    public void ChangeBlack()
    {
        Color color = new Color();
        color.r = 0f;
        color.b = 0f;
        color.g = 0f;
        color.a = 255f;
        sprite.color = color;
    }
    // Method : 타일에서 벽을 파괴하는 메소드
    public void DestroyWall(WallDir wall)
    {
        switch (wall)
        {
            case WallDir.Up:
                upWall.SetActive(false);
                break;
            case WallDir.Down:
                downWall.SetActive(false);
                break;
            case WallDir.Left:
                leftWall.SetActive(false);
                break;
            case WallDir.Right:
                rightWall.SetActive(false);
                break;
        }
    }



}
