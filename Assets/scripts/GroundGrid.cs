using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGrid : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;

    void Start() {
        generateGrid();
    }

    private void generateGrid() {
        //go through all the map and set ground tiles
        for (int x = 0; x < Globals.MAP_WIDTH; x++) {
            for (int y = 0; y < Globals.MAP_HEIGHT; y++) {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}


