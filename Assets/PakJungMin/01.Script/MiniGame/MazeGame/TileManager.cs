using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    static TileManager instance;

    public static TileManager tileManager { get { return instance; } }

    public Dictionary<string, Tile> tileDir = new Dictionary<string, Tile>();

    [SerializeField] public Tile[] Tiles;
    [SerializeField] int lengthX;
    [SerializeField] int lengthY;


    private void Awake()
    {
        if(instance != null) { Destroy(gameObject); }

        instance = this;
    }

    private void Start()
    {
        Tiles = GetComponentsInChildren<Tile>();
        int x = 0;
        int y = 0;
        foreach (Tile tile in Tiles)
        {
            tile.gameObject.name = $"{x},{y}";
            tile.PosX = x;
            tile.PosY = y;
            x++;
            tileDir.Add(tile.gameObject.name, tile);
            if (x >= lengthX)
            {
                y++;
                x = 0;
            }
        }

        MazeManager.mazeManager.CreateMaze();
    }
}
