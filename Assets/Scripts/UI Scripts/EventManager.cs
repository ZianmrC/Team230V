using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI electricityText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI damageText;
    private bool tookDamage;
    private float damageTimer;

    public static string GameOverScene = "GameOver Scene";
    public static int taskCounter = 0;
    public static float TotalGameTime;
    private float updateTimer;
    private float percentage;
    public TextMeshProUGUI taskCounterText;
    public int numberOfTasksUntilOverload = 2; //The minimum number of tasks present in order to start incrementing the Electricity Overload
    public static int Lives = 3;
    public static int Score;

    public GameObject[] livesIcons;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        damageText.enabled = false;
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
                percentage -= 2;
            }
            else if (taskCounter > 0 && taskCounter >= numberOfTasksUntilOverload)
            {
                percentage += (taskCounter - 1);
            }
            electricityText.text = $"Electricity Overload: {percentage}%";
            scoreText.text = $"Current Score: {Score}";
            updateTimer = 0f;
            //Debug.Log($"Task Counter: {taskCounter}");
        }
        if(percentage >= 100)
        {
            SceneManager.LoadScene(GameOverScene);
        }
        if(tookDamage)
        {
            damageTimer += Time.deltaTime;
            if(damageTimer > 5f)
            {
                damageText.enabled = false;
                tookDamage = false;
            }
            }
        }
    public void DecreaseOverload(int decrement) { percentage -= decrement; }
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
    public void AddScore(int score) { EventManager.Score += score; }
    public void LoseLife()
    {
        Lives--;
        if(Lives == 2)
        {
            Destroy(livesIcons[2].gameObject);
        }
        else if(Lives == 1)
        {
            Destroy(livesIcons[1].gameObject);
        }
        else if(Lives == 0)
        {
            SceneManager.LoadScene(GameOverScene);
        }
        damageText.enabled = true;
        tookDamage = true;
    }

}
