using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;

public class GeneralSwitches : MonoBehaviour
{
    private TaskInfo taskInfo;
    public int taskID;
    EventManager eventManager;
    private int inputsRequired;
    public GameObject timerTextObject;
    private Text timerText;

    private int[] inputs;
    private bool waiting;
    private float timer;
    public GameObject[] inputImageArray;
    public GameObject greenCheck;
    private Vector2[] positions; //Positions of Image Pictures
    private Vector2[] greenCheckPositions; //Positions of Green Checks - used to indicate which inputs the player has already done
    private bool holdingDown = false;
    private int currentStep = 0;
    private List<int> correctSequence = new List<int>();

    //Help Text
    public TextMeshProUGUI helpText;
    public float helpTimer;
    private float currentTimer;

    //TaskVariables
    public TaskVariables taskVariables;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"TaskID: {taskID}");
        timer = taskVariables.maxTime;
        helpText.enabled = false;
        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();

        if (EventManager.TotalGameTime > taskVariables.switchDifficulty1Time)
        {
            inputsRequired = 5;
            positions = new Vector2[inputsRequired];

            positions[0] = new Vector2(-400, -40);
            positions[1] = new Vector2(-200, -40);
            positions[2] = new Vector2(0, -40);
            positions[3] = new Vector2(200, -40);
            positions[4] = new Vector2(400, -40);

            greenCheckPositions = new Vector2[inputsRequired];

            greenCheckPositions[0] = new Vector2(-400, 60);
            greenCheckPositions[1] = new Vector2(-200, 60);
            greenCheckPositions[2] = new Vector2(0, 60);
            greenCheckPositions[3] = new Vector2(200, 60);
            greenCheckPositions[4] = new Vector2(400, -40);

        }
        else
        {
            inputsRequired = 4;
            positions = new Vector2[inputsRequired];

            positions[0] = new Vector2(-300, -40);
            positions[1] = new Vector2(-125, -40);
            positions[2] = new Vector2(125, -40);
            positions[3] = new Vector2(300, -40);

            greenCheckPositions = new Vector2[inputsRequired];

            greenCheckPositions[0] = new Vector2(-300, 60);
            greenCheckPositions[1] = new Vector2(-125, 60);
            greenCheckPositions[2] = new Vector2(125, 60);
            greenCheckPositions[3] = new Vector2(300, 60);
        }
        inputs = new int[inputsRequired];
        correctSequence = new List<int>(Enumerable.Repeat(0, inputsRequired));

        for (int i = 0; i < inputsRequired; i++)
        {
            int randomInt = UnityEngine.Random.Range(0, 4);
            inputs[i] = randomInt;
        }
        //Place Images onto empty UI
        for (int i = 0; i < inputsRequired; i++)
        {
            int index = inputs[i];
            InstantiateImage(positions[i], inputImageArray[index]);
        }
        timerText = timerTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer > helpTimer)
        {
            helpText.enabled = true;
        }
        float roundedTimer = Mathf.Round(timer * 10f) / 10f;
        timerText.text = $"{roundedTimer}";
        if (currentStep < inputsRequired)
        {
            if (currentStep >= 1)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 4f;
                    ResetInput();
                }
            }
            if (waiting)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && inputs[currentStep] == 0)
                {
                    correctSequence[currentStep] = 2;
                    InstantiateImage(greenCheckPositions[currentStep], greenCheck);
                    currentStep++;
                    waiting = false;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && inputs[currentStep] == 1)
                {
                    correctSequence[currentStep] = 2;
                    InstantiateImage(greenCheckPositions[currentStep], greenCheck);
                    currentStep++;
                    waiting = false;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && inputs[currentStep] == 2)
                {
                    correctSequence[currentStep] = 2;
                    InstantiateImage(greenCheckPositions[currentStep], greenCheck);
                    currentStep++;
                    waiting = false;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && inputs[currentStep] == 3)
                {
                    correctSequence[currentStep] = 2;
                    InstantiateImage(greenCheckPositions[currentStep], greenCheck);
                    currentStep++;
                    waiting = false;
                }
                else if(Input.anyKeyDown && !Input.GetMouseButtonDown(0))
                {
                    correctSequence[currentStep] = 1;
                }
                bool allTrue = correctSequence.All(b => b == 2);
                bool hasOne = correctSequence.Any(item => item == 1);
                if(allTrue)
                {
                    Debug.Log(taskID);
                    eventManager.ChecksTasksForID(taskID);
                    eventManager.UpdateBoolArrayGivenID(taskID);
                    eventManager.AddScore(taskVariables.switchScore);
                    eventManager.DecreaseOverload(1);
                    Destroy(gameObject);
                }
                else if(hasOne) //If incorrect input detected
                {
                    ResetInput();
                }
            }

            if (Input.anyKey && !holdingDown)
            {
                holdingDown = true;
            }

            if (!Input.anyKey && holdingDown)
            {
                holdingDown = false;
                waiting = true; // Reset for the next input
            }
        }
    }

    public void InstantiateImage(Vector2 position, GameObject image)
    {
        GameObject newImage = Instantiate(image, this.transform);
        Image newimage = newImage.GetComponent<Image>();

        RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
        newImageRectTransform.anchoredPosition = position;
    }
    void ResetInput()
    {
        Debug.Log("Try again from the start!");
        for (int i = 0; i < correctSequence.Count; i++)
        {
            correctSequence[i] = 0;
        }
        currentStep = 0;
        timer = taskVariables.maxTime;
        Transform[] children = this.GetComponentsInChildren<Transform>().Where(child => child.name == "greenTick(Clone)").ToArray();

        foreach (Transform child in children)
        {
            child.gameObject.SetActive(false);
        }

    }
}