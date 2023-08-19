using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElectricityOverload : MonoBehaviour
{
    public TextMeshProUGUI text;

    private int taskCounter = 0;
    private float updateTimer;
    private float percentage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer += Time.deltaTime;
        if(updateTimer > 1.5f)
        {
            if(taskCounter == 0 && percentage > 0f) //If there are no tasks currently, decrease the overload bar
            {
                percentage--;
            }
            percentage += taskCounter;
            text.text = $"Electricity Overload: {percentage}%";
            updateTimer = 0f;
        }
    }
    public void CountTasks()
    {
        GameObject[] taskObjects = GameObject.FindGameObjectsWithTag("Task");

        taskCounter = taskObjects.Length;
    }

}
