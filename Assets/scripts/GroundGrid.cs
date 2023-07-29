using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGrid : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;

    void Start() {
        generateGrid();
    }

    void generateGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}


