using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallGrid : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;

    void Start() {
        generateGrid();
    }

    void generateGrid() {
        //go through all the edge of map and set wall tiles
        for (int x = 0; x < width; x++) {
            if (x == 0 || x == width - 1) {
                for (int y = 0; y < height; y++) {tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            } else {
                tilemap.SetTile(new Vector3Int(x, 0, 0), tile);
                tilemap.SetTile(new Vector3Int(x, height - 1, 0), tile);
            }
        }
    }
}


