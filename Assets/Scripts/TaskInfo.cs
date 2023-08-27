using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfo : MonoBehaviour
{
    public float taskTimer;
    private ClickAnamoly CA;
    private float currentTimer;

    public int taskID; //Used to reference itself in AnamolySpawner.occupiedAnamolyLocations
    // Start is called before the first frame update
    void Start()
    {
        CA = GetComponent<ClickAnamoly>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer < taskTimer)
        {
            //Remove 1 Health Point
        }
    }
    public void UpdateBoolArray() //Updates the bool array which dictates if a task is currently there or not
    {
        AnamolySpawner.occupiedAnomalyLocations[taskID] = false;
        Debug.Log($"Positioned freed: {taskID}");
        AnamolySpawner.availableSpots.Add(taskID);
        Debug.Log($"Available spots count after update: {AnamolySpawner.availableSpots.Count}");
    }
    public void UpdateID(int id)
    {
        taskID = id;
    }
}
