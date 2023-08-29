using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public static int taskCounter = 0;
    public static float TotalGameTime;
    private float updateTimer;
    private float percentage;
    public TextMeshProUGUI taskCounterText;
    public int numberOfTasksUntilOverload = 2; //The minimum number of tasks present in order to start incrementing the Electricity Overlaod
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CountTasks();
        taskCounterText.text = $"{taskCounter}";
        updateTimer += Time.deltaTime; TotalGameTime += Time.deltaTime;
        if(updateTimer > 1.5f)
        {
            //CountTasks();
            if(taskCounter == 0 && percentage > 0f) //If there are no tasks currently, decrease the overload bar
            {
                percentage--;
            }
            else if (taskCounter > 0 && taskCounter >= numberOfTasksUntilOverload)
            {
                percentage += (taskCounter - 1);
            }
            text.text = $"Electricity Overload: {percentage}%";
            updateTimer = 0f;
            //Debug.Log($"Task Counter: {taskCounter}");
        }
    }
    public void CountTasks()
    {
        GameObject[] taskObjects = GameObject.FindGameObjectsWithTag("Task");

        taskCounter = taskObjects.Length;
        //Debug.Log($"Number of tasks currently found: {taskCounter}");
        /*
        foreach (var taskObject in taskObjects)
        {
            Debug.Log($"Task object name: {taskObject.name}");
        }
        */
    }
    public void ChecksTasksForID(int ID)
    {
        GameObject[] taskObjects = GameObject.FindGameObjectsWithTag("Task");
        foreach (var taskObject in taskObjects)
        {
            TaskInfo taskInfo = taskObject.GetComponent<TaskInfo>();
            int number = taskObject.GetComponent<TaskInfo>().taskID;
            if (taskInfo != null && taskInfo.taskID == ID)
            {
                // Perform your action here, for example:
                // taskInfo.UpdateBoolArray();
                Destroy(taskObject);
            }
        }
    }
    public void UpdateBoolArrayGivenID(int ID)
    {
        AnamolySpawner.occupiedAnomalyLocations[ID] = false;
        AnamolySpawner.availableSpots.Add(ID);

    }

}
