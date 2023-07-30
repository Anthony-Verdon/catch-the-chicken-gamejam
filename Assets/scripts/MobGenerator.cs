using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator: MonoBehaviour
{

    [SerializeField] private int width, height, spawnRateChicken, spawnRateFox;
    [SerializeField] private GameObject Chicken, Fox;

    void Start()
    {
        //GenerateMobs();
        Instantiate(Fox, new Vector3(13, 13, 0), Quaternion.identity);
        Instantiate(Fox, new Vector3(5, 5, 0), Quaternion.identity);
    }

    private void GenerateMobs()
    {
        //go through all the map and try to spawn a chicken or a fox depending on the spawnRate
        for (int x = 2; x < width - 2; x++)
        {
            for (int y = 2; y < height - 2; y++)
            {
                float randomNumber =  Random.Range(0, 100);
                if (randomNumber < spawnRateChicken)
                    Instantiate(Chicken, new Vector3(x, y, 0), Quaternion.identity);
                if (randomNumber < spawnRateFox)
                    Instantiate(Fox, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
