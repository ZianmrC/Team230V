using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AnamolySpawner : MonoBehaviour
{
    public Transform[] anomalyLocations = new Transform[0];
    public static bool[] occupiedAnomalyLocations;
    public GameObject KTask; //Kitchen Task
    public GameObject LVTask; //Living Room Task
    public GameObject BRTask; //Bathroom Task

    public float maxSpawnTime; //Gurantees to spawn an anamoly if one hasn't spawned after a particular amount of time
    public float percentageIncrementInterval;
    public float percentageIncrement; //Used to increase the chances of spawning at every interval
    public float spawnChance = 10f;

    private float timeSinceLastSpawn;
    private GameObject EventManager;
    ElectricityOverload electricityOverload;
    // Start is called before the first frame update
    void Start()
    {
        occupiedAnomalyLocations = new bool[anomalyLocations.Length];
        EventManager = GameObject.Find("EventSystem");
        electricityOverload = EventManager.GetComponent<ElectricityOverload>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spawning Anamoly
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > percentageIncrementInterval) //Chance to Spawn anamoly after number of seconds
        {
            float randomFloat = Random.Range(0.0f, 100f);
            //Debug.Log($"Random Float: {randomFloat}, spawnChance: {spawnChance}");
            if(timeSinceLastSpawn >= maxSpawnTime)
            {
                SpawnAnamoly();
            }
            else if (randomFloat < spawnChance)
            {
                SpawnAnamoly();
                spawnChance = 10f;
            }
            else
            {
                spawnChance += percentageIncrement;
            }
            timeSinceLastSpawn = 0f;
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
                SpawnAnamoly();
            }
        }
        electricityOverload.CountTasks();
    }
}
