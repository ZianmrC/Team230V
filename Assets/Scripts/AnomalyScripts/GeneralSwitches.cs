using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class GeneralSwitches : MonoBehaviour
{
    private GameObject manager;
    EventManager eventManager;
    private int inputsRequired;
    public float difficulty1Time; //Make this type of activity harder when the total game time has surpassed this amount of time

    private int[] inputs;
    private bool waiting;
    public GameObject[] inputImageArray;
    private Vector2[] positions;
    private bool holdingDown = false;
    private int currentStep = 0;
    private List<int> correctSequence = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("EventSystem");
        eventManager = manager.GetComponent<EventManager>();

        if (EventManager.TotalGameTime > difficulty1Time)
        {
            inputsRequired = 5;
            positions = new Vector2[inputsRequired];

        }
        else
        {
            inputsRequired = 4;
            positions = new Vector2[inputsRequired];

            positions[0] = new Vector2(-200, -40);
            positions[1] = new Vector2(-75, -40);
            positions[2] = new Vector2(75, -40);
            positions[3] = new Vector2(200, -40);
        }
        inputs = new int[inputsRequired];
        correctSequence = new List<int>(Enumerable.Repeat(0, inputsRequired));

        for (int i = 0; i < inputsRequired; i++)
        {
            int randomInt = Random.Range(0, 4);
            inputs[i] = randomInt;
        }
        //Place Images onto empty UI
        for (int i = 0; i < inputsRequired; i++)
        {
            int index = inputs[i];
            InstantiateImage(positions[i], inputImageArray[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStep < inputsRequired)
        {
            if (waiting)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && inputs[currentStep] == 0)
                {
                    correctSequence[currentStep] = 2;
                    currentStep++;
                    waiting = false;
                    Debug.Log("Up Inputted");
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && inputs[currentStep] == 1)
                {
                    correctSequence[currentStep] = 2;
                    currentStep++;
                    waiting = false;
                    Debug.Log("Right Inputted");
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && inputs[currentStep] == 2)
                {
                    correctSequence[currentStep] = 2;
                    currentStep++;
                    waiting = false;
                    Debug.Log("Down Inputted");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && inputs[currentStep] == 3)
                {
                    correctSequence[currentStep] = 2;
                    currentStep++;
                    waiting = false;
                    Debug.Log("Left Inputted");
                }
                else if(!waiting)
                {
                    correctSequence[currentStep] = 1;
                }
                bool allTrue = correctSequence.All(b => b == 2);
                bool hasOne = correctSequence.Any(item => item == 1);
                if(allTrue)
                {
                    Debug.Log("Task Completed!");
                    Destroy(gameObject);
                }
                else if(hasOne) //If incorrect input detected
                {
                    Debug.Log("Try again from the start!");
                    for(int i = 0; i < correctSequence.Count; i++)
                    {
                        correctSequence[i] = 0;
                    }
                    currentStep = 0;
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
}
