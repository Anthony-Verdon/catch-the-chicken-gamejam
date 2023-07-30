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
        for (int x = -6; x < Globals.MAP_WIDTH + 6; x++) {
            for (int y = -6; y < Globals.MAP_HEIGHT + 6; y++) {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}