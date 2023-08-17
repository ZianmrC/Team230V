using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AnomalySpawner : MonoBehaviour
{
    public Transform[] anomalyLocations = new Transform[0];
    public static bool[] occupiedAnomalyLocations;
    public GameObject KTask; //Kitchen Task
    public GameObject LVTask; //Living Room Task
    public GameObject BRTask; //Bathroom Task

    public float maxSpawnTime;
    public float percentageIncrementInterval;
    public float percentageIncrement; //Used to increase the chances of spawning at every interval
    public float spawnChance = 10f;

    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        occupiedAnomalyLocations = new bool[anomalyLocations.Length];

    }

    // Update is called once per frame
    void Update()
    {
        //Spawning Anamoly
        spawnTimer += Time.deltaTime;
        if (spawnTimer > percentageIncrementInterval) //Chance to Spawn anamoly after number of seconds
        {
            float randomFloat = Random.Range(0.0f, 100f);
            if (randomFloat > spawnChance)
            {
                SpawnAnamoly();
            }
            spawnTimer = 0f;
        }


        //Increase Spawn Chances if Anamoly not spawned

    }
    public void SpawnAnamoly()
    {
        int randomLocation = Random.Range(0, anomalyLocations.Length);
        if (occupiedAnomalyLocations[randomLocation] == false)
        {
            occupiedAnomalyLocations[randomLocation] = true; //Signify that task is already at spawn location
            if (randomLocation < 3)
            {
                Instantiate(KTask, anomalyLocations[randomLocation]);
            }
            else if (randomLocation < 6)
            {
                Instantiate(LVTask, anomalyLocations[randomLocation]);
            }
            else if (randomLocation < 9)
            {
                Instantiate(BRTask, anomalyLocations[randomLocation]);
            }
        }
        else
        {
            if (occupiedAnomalyLocations.All(element => element))
            {
                Debug.Log("All Tasks are present");
                return;
            }
            else
            {
                Debug.Log("spawn repeated");
                SpawnAnamoly();
            }
        }
    }
}
