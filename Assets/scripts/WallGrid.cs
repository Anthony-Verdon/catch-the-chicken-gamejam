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

    private void generateGrid() {
        //go through all the edge of map and set wall tiles
        for (int x = 0; x < Globals.MAP_WIDTH; x++) {
            if (x == 0 || x == Globals.MAP_WIDTH - 1) {
                for (int y = 0; y < Globals.MAP_HEIGHT; y++) {tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            } else {
                tilemap.SetTile(new Vector3Int(x, 0, 0), tile);
                tilemap.SetTile(new Vector3Int(x, Globals.MAP_HEIGHT - 1, 0), tile);
            }
        }
    }
}


