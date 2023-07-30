using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator: MonoBehaviour
{
    [SerializeField] private GameObject Chicken, Fox;
    
    void Start()
    {
        GenerateMobs();
        //Instantiate(Fox, new Vector3(Globals.MAP_WIDTH / 2, Globals.MAP_HEIGHT / 2, 0), Quaternion.identity);
    }

    private void GenerateMobs()
    {
        //go through all the map and try to spawn a chicken or a fox depending on the spawnRate
        for (int x = 2; x < Globals.MAP_WIDTH - 2; x++)
        {
            for (int y = 2; y < Globals.MAP_HEIGHT - 2; y++)
            {
                float randomNumber =  Random.Range(0, 100);
                if (randomNumber < Globals.CHICKEN_SPAWN_RATE)
                    Instantiate(Chicken, new Vector3(x, y, 0), Quaternion.identity);
                //if (randomNumber < Globals.FOX_SPAWN_RATE)
                //    Instantiate(Fox, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
