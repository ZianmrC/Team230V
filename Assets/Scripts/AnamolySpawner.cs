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
    public static List<int> availableSpots = new List<int>();
    public GameObject SwitchTask; //Switches Task
    public GameObject LVTask; //Living Room Task
    public GameObject BRTask; //Bathroom Task
    public GameObject PlugTask; //Plug task
    public GameObject ParentTask; //Parent Task

    public float maxSpawnTime; //Gurantees to spawn an anamoly if one hasn't spawned after a particular amount of time
    public float percentageIncrementInterval;
    public float percentageIncrement; //Used to increase the chances of spawning at every interval
    public float spawnChance = 10f;
    private float originalChance;

    private float timeSinceLastSpawn;
    private GameObject e;
    EventManager eventManager;
    private bool difficultyIncreased1 = false;
    private bool difficultyIncreased2 = false;
    // Start is called before the first frame update
    void Start()
    {
        originalChance = spawnChance;
        occupiedAnomalyLocations = new bool[anomalyLocations.Length];
        for(int i = 0; i<anomalyLocations.Length; i++)
        {
            occupiedAnomalyLocations[i] = false;
            availableSpots.Add(i);
        }
        e = GameObject.Find("EventSystem");
        eventManager = e.GetComponent<EventManager>();
        difficultyIncreased1 = false;
        difficultyIncreased2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.totalGameTime += Time.deltaTime;
        {
            if(EventManager.totalGameTime > 40 && !difficultyIncreased1)
            {
                spawnChance += 10f;
                maxSpawnTime -= 5f;
                difficultyIncreased1 = true;
            }
            if (EventManager.totalGameTime > 80 && !difficultyIncreased2)
            {
                spawnChance += 5f;
                percentageIncrement += 1;
                difficultyIncreased2 = true;
            }
        }
        //Spawning Anamoly
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > percentageIncrementInterval) //Chance to Spawn anamoly after number of seconds
        {
            float randomFloat = Random.Range(0.0f, 100f);
            //Debug.Log($"Random Float: {randomFloat}, spawnChance: {spawnChance}");
            if(timeSinceLastSpawn >= maxSpawnTime) //100% to spawn anomaly after given time
            {
                SpawnAnamoly();
            }
            else if (randomFloat < spawnChance)
            {
                SpawnAnamoly();
                spawnChance = originalChance;
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
        if (availableSpots.Count == 0)
        {
            //Debug.Log("All Tasks are present");
            return;
        }

        int randomIndex = Random.Range(0, availableSpots.Count);
        int randomLocation1 = availableSpots[randomIndex];
        int randomLocation = randomLocation1;
        availableSpots.RemoveAt(randomIndex);

        //Debugging
        //Debug.Log($"Available spots count: {availableSpots.Count}");
        //Debug.Log($"Selected random location: {randomLocation}");
        if (occupiedAnomalyLocations[randomLocation] == false)
        {
            //Debug.Log($"Selected spot is not occupied. Spawning anomaly...");
        }
        else
        {
            //Debug.Log($"Selected spot is occupied. Trying again...");
        }

        if (occupiedAnomalyLocations[randomLocation] == false)
        {
            occupiedAnomalyLocations[randomLocation] = true; //Signify that task is already at spawn location
            if (randomLocation < 2)
            {
                //InstantiateAnomaly(ParentTask, anomalyLocations[randomLocation], randomLocation); //Causes Errors
                InstantiateAnomaly(SwitchTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if (randomLocation < 3)
            {
                InstantiateAnomaly(SwitchTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if (randomLocation == 6)
            {
                InstantiateAnomaly(PlugTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if (randomLocation < 7)
            {
                InstantiateAnomaly(SwitchTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if (randomLocation < 9)
            {
                InstantiateAnomaly(PlugTask, anomalyLocations[randomLocation], randomLocation);
            }
        }
    }
    void InstantiateAnomaly(GameObject anomaly, Transform location, int taskID)
    {
        anomaly.GetComponent<TaskInfo>().taskID = taskID;
        Instantiate(anomaly, location);
    }
}
