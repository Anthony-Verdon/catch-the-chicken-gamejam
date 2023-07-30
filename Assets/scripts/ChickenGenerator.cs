using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGenerator: MonoBehaviour
{

    [SerializeField] private int width, height, spawnRate;
    [SerializeField] private GameObject Chicken;
    void Start()
    {
        GenerateChicken();
    }

    private void GenerateChicken()
    {
        //go through all the map and try to spawn a chicken depending on the spawnRate
        for (int x = 2; x < width - 2; x++)
        {
            for (int y = 2; y < height - 2; y++)
            {
                float randomNumber =  Random.Range(0, 100);
                if (randomNumber < spawnRate)
                    Instantiate(Chicken, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
