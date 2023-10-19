using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
    }
}