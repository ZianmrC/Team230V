using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class generalWaterTask : MonoBehaviour
{
    public int taskID;

        public GameObject whipe;
        private Canvas canvas; // Declare the canvas variable
    public GameObject waterDrop;
     private int xRange = 1500;
     private int yRange = 500;
    private List<GameObject> waterDropInstances = new List<GameObject>();
    public int score = 5;
        EventManager eventManager;

    [Header("Tooltip/Mascot Variables")]
    public GameObject waterMascot;
    private GameObject textObject;
    private TextMeshProUGUI tooltipText;
    private Vector2 originPosition;
    private Vector2 endPosition;
    private RectTransform rect;
    public float moveSpeed = 500f;
    private bool stopTooltip;
    private float mascotTimer;
    private bool spoken;
    public TaskVariables taskVariables;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
        GameObject whipeInstnace = Instantiate(whipe, this.transform);
        for (int i = 0; i < 1000; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0.0f);
            GameObject waterDropInstance = Instantiate(waterDrop, this.transform);
            RectTransform rectTransform = waterDropInstance.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = spawnPosition;
            waterDropInstances.Add(waterDropInstance); // Add the instance to the list
        }
        originPosition = new Vector2(-520, -1600);
        endPosition = new Vector2(-520, -1006);
        rect = waterMascot.GetComponent<RectTransform>();
        rect.anchoredPosition = originPosition;
        Transform textObject = transform.Find("Mascot/Container/Image/Text (TMP)");
        tooltipText = textObject.GetComponent<TextMeshProUGUI>();
        mascotTimer = 0f; stopTooltip = false; spoken = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool finished = true;
        // Iterate over the list of water drop instances and check if any no longer exist
        foreach (var waterDropInstance in waterDropInstances)
        {
            if (waterDropInstance != null)
            {
                finished = false;
                break; // No need to continue checking once one instance is found to be destroyed
            }
        }

        if (finished)
        {
            // All water drop instances have been destroyed, execute the rest of the update method
            eventManager.ChecksTasksForID(taskID);
            eventManager.UpdateBoolArrayGivenID(taskID);
            eventManager.AddScore(score);
            eventManager.DecreaseOverload(2);
            Destroy(gameObject);
        }
        mascotTimer += Time.deltaTime;
        if (mascotTimer > taskVariables.switchHelpTime)
        {
            if (waterDropInstances.Count < 990)
            {
                rect.anchoredPosition = originPosition;
            }
            else if (rect.anchoredPosition.y < endPosition.y)
            {
                float newY = rect.anchoredPosition.y + moveSpeed * Time.deltaTime;
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);
            }
            else if (!stopTooltip)
            {
                tooltipText.enabled = true;
                if (spoken == false)
                {
                    EventManager.PlayAudioSource("Mumbling");
                    spoken = true;
                }
                tooltipText.text = "A slippery floor is never good!\nAnd can be more dangerous when \nelectrical appliances are in the picture,\n" +
                    "click and drag the wipe to clean up the floor.";
                if (waterDropInstances.Count < 990)
                {
                    stopTooltip = true;
                    rect.anchoredPosition = originPosition;
                }
            }
        }
    }
}