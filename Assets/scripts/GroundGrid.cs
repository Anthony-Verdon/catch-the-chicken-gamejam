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
        for (int x = -Globals.OFFSET; x < Globals.MAP_WIDTH + Globals.OFFSET; x++) {
            for (int y = -Globals.OFFSET; y < Globals.MAP_HEIGHT + Globals.OFFSET; y++) {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}