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
    public GameObject SwitchTask;
    public GameObject PlugTask;
    public GameObject BrokenToasterTask; //(Parent Task)
    public GameObject WaterTask;
    public GameObject WireTask;
    public GameObject SinkTask;
    public GameObject BulbTask; //(Parent Task)
    public GameObject BathtubTask; //(Parent Task)
    public GameObject ElectricalFireTask; //(Parent Task)

    public GameObject ParentTask; //Parent Task

    public float maxSpawnTime; //Gurantees to spawn an anamoly if one hasn't spawned after a particular amount of time
    public float percentageInterval;
    public float percentageIncrement; //Used to increase the chances of spawning at every interval
    public float spawnChance = 10f;
    private float originalChance;

    private float timeSinceLastSpawn;
    private bool difficulty1;
    private bool difficulty2;
    private GameObject e;
    EventManager eventManager;
    public TaskVariables taskVariables;

    [Header("For Editting purposes")] public bool pauseSpawning;
    // Start is called before the first frame update
    void Start()
    {
        originalChance = spawnChance; difficulty1 = false; difficulty2 = false;
        occupiedAnomalyLocations = new bool[anomalyLocations.Length];
        for(int i = 0; i<anomalyLocations.Length; i++)
        {
            occupiedAnomalyLocations[i] = false;
            availableSpots.Add(i);
        }
        e = GameObject.Find("EventSystem");
        eventManager = e.GetComponent<EventManager>();


    }

    // Update is called once per frame
    void Update()
    {
        //Spawning Anamoly
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > percentageInterval && !pauseSpawning) //Chance to Spawn anamoly after number of seconds
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
        if(EventManager.TotalGameTime > taskVariables.spawnerDifficultyIncrease1 && !difficulty1)
        {
            spawnChance += 5;
            maxSpawnTime -= 8;
            difficulty1 = true;
        }
        else if (EventManager.TotalGameTime > taskVariables.spawnerDifficultyIncrease1 && !difficulty2)
        {
            percentageIncrement += 2;
            difficulty2 = true;
        }

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
            //Kitchen Spawns
            if(randomLocation == 0) //Switches
            {
                InstantiateAnomaly(SwitchTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if(randomLocation == 1) //Sink
            {
                InstantiateAnomaly(SinkTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if(randomLocation == 2) //Toaster
            {
                InstantiateAnomaly(BrokenToasterTask, anomalyLocations[randomLocation], randomLocation);
            }
            //LivingRoom Spawns
            else if(randomLocation == 3) //Fire
            {
                InstantiateAnomaly(ElectricalFireTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if(randomLocation == 4) //Electrical Bulb (NEED TO MAKE BULB TASK)
            {
                InstantiateAnomaly(BulbTask, anomalyLocations[randomLocation], randomLocation);
            }
            else if(randomLocation == 5) //Wire
            {
                InstantiateAnomaly(WireTask, anomalyLocations[randomLocation], randomLocation);
            }
            //BathRoom Spawns
            else if (randomLocation == 6) //Water (STOPPED FOR TESTING PURPOSES)
            {
                InstantiateAnomaly(WaterTask, anomalyLocations[randomLocation], randomLocation);
                //SpawnAnamoly();
            }
            else if (randomLocation == 7) //Plug (STOPPED FOR TESTING PURPOSES)
            {
                InstantiateAnomaly(WaterTask, anomalyLocations[randomLocation], randomLocation);
                SpawnAnamoly();
            }
            else if (randomLocation == 8) //Bathtub (NEED TO MAKE BATHTUB TASK)
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
