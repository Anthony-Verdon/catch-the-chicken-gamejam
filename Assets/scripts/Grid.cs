using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tile;
    [SerializeField] private Transform camera;

    void Start() {
        generateGrid();
    }

    void generateGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                var newTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                newTile.name = $"Tile {x} {y}";
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                    newTile.InitWall();
                } else {
                    var isOffset = (x % 2 == 0 && y % 2 != 0 ) || (x % 2 != 0 && y % 2 == 0);
                    newTile.InitGround(isOffset);
                }
            }
        }
        camera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}


