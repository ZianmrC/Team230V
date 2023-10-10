using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralPlug : MonoBehaviour
{
    public static int numberOfCorrectPlaces = 0;
    EventManager eventManager;
    public GameObject[] imageArray;
    public int taskID;
    public TextMeshProUGUI text;

    private string[] colors = { "Blue", "Yellow", "Red", "Green" };
    public GameObject neutralPlug;
    public GameObject topExtensionCord;

    //Spawn Locations of Plugs + PlugsSlots
    public GameObject[] topExtensionCordSpawns;
    public GameObject[] plugSlotSpawn1;
    public GameObject[] plugSlotSpawn2;
    public GameObject[] colorIdentifierSpawns;

    // Reference ExtensionCord Children Objects
    public GameObject ExtensionCord1;
    public GameObject ExtensionCord2;
    [HideInInspector] private string extensionCord1Color = null;
    [HideInInspector] private string extensionCord2Color = null;

    private List<GameObject> instantiatedSlots = new List<GameObject>();
    public GameObject[] colorIdentifier;
    public GameObject plugSlots;

    public static bool awakeCalled = false;

    private int color1Tracker = 2;
    private int color2Tracker = 2;
    private int neutralColorTracker = 2;
    private GameObject color1;
    private GameObject color2;

    //Help text (If player doesn't achieve the goal after a certain amount of time, show help text)
    private float currentTimer;
    public float helpTimer;
    public TextMeshProUGUI helpText;

    public TaskVariables taskVariables;


    private void Start()
    {
        helpText.enabled = false;
        numberOfCorrectPlaces = 0;
        // ENSURE TO FIX THIS ONCE ACTUAL ASSETS MADE
        // Pick 2 Random Colors
        while (extensionCord2Color == null || extensionCord1Color == null)
        {
            int randomIndex = Random.Range(0, colors.Length);
            int randomIndex2 = Random.Range(0, colors.Length);
            extensionCord1Color = colors[randomIndex];
            extensionCord2Color = colors[randomIndex2];
            if (extensionCord1Color == extensionCord2Color || extensionCord1Color == colors[3])
            {
                extensionCord2Color = null;
            }
        }

        if (extensionCord1Color == "Blue") { color1 = imageArray[0]; }
        else if (extensionCord1Color == "Yellow") { color1 = imageArray[1]; }
        else if (extensionCord1Color == "Red") { color1 = imageArray[2]; }
        else if (extensionCord1Color == "Green") { color1 = imageArray[3]; }

        if (extensionCord2Color == "Blue") { color2 = imageArray[0]; }
        else if (extensionCord2Color == "Yellow") { color2 = imageArray[1]; }
        else if (extensionCord2Color == "Red") { color2 = imageArray[2]; }
        else if (extensionCord2Color == "Green") { color2 = imageArray[3]; }

        for (int i = 0; i < 6; i++)
        {
            InstantiatePlug(topExtensionCordSpawns[i]);
        }

        GameObject eventObj = GameObject.Find("EventSystem");
        if (eventObj != null)
        {
            eventManager = eventObj.GetComponent<EventManager>();
        }

        for (int i = 0; i < plugSlotSpawn1.Length; i++) //ExtensionCord1
        {
            Transform position1 = plugSlotSpawn1[i].transform;
            Vector3 positionVector = position1.position;
            GameObject newSlot = Instantiate(plugSlots, positionVector, Quaternion.identity, transform);
            PlugTaskSlot slotScript = newSlot.GetComponent<PlugTaskSlot>();
            slotScript.color = extensionCord1Color;
            instantiatedSlots.Add(newSlot);
        }
        for (int i = 0; i < plugSlotSpawn2.Length; i++) //ExtensionCord2
        {
            Transform position1 = plugSlotSpawn2[i].transform;
            Vector3 positionVector = position1.position;
            GameObject newSlot = Instantiate(plugSlots, positionVector, Quaternion.identity, transform);
            PlugTaskSlot slotScript = newSlot.GetComponent<PlugTaskSlot>();
            slotScript.color = extensionCord2Color;
            instantiatedSlots.Add(newSlot);
        }

        Transform parentTransform = transform;
        // Create the Color Identifier
        Transform position = colorIdentifierSpawns[0].transform;
        Vector3 positionV1 = position.position;
        Transform position2 = colorIdentifierSpawns[1].transform;
        Vector3 positionV2 = position2.position;
        Quaternion rotation = Quaternion.identity; // No rotation

        if (extensionCord1Color == "Blue")
        {
            Instantiate(colorIdentifier[0], positionV1, rotation, parentTransform);
        }
        else if (extensionCord1Color == "Yellow")
        {
            Instantiate(colorIdentifier[1], positionV1, rotation, parentTransform);
        }
        else if (extensionCord1Color == "Red")
        {
            Instantiate(colorIdentifier[2], positionV1, rotation, parentTransform);
        }
        else if (extensionCord1Color == "Green")
        {
            Instantiate(colorIdentifier[3], positionV1, rotation, parentTransform);
        }
        //
        if (extensionCord2Color == "Blue")
        {
            Instantiate(colorIdentifier[0], positionV2, rotation, parentTransform);
        }
        else if (extensionCord2Color == "Yellow")
        {
            Instantiate(colorIdentifier[1], positionV2, rotation, parentTransform);
        }
        else if (extensionCord2Color == "Red")
        {
            Instantiate(colorIdentifier[2], positionV2, rotation, parentTransform);
        }
        else if (extensionCord2Color == "Green")
        {
            Instantiate(colorIdentifier[3], positionV2, rotation, parentTransform);
        }
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if (numberOfCorrectPlaces == 4)
        {
            Debug.Log("Task Completed");
            eventManager.ChecksTasksForID(taskID);
            eventManager.UpdateBoolArrayGivenID(taskID);
            eventManager.AddScore(taskVariables.plugScore);
            eventManager.DecreaseOverload(2);
            Destroy(this.gameObject);
            awakeCalled = false;
        }

        if(helpTimer < currentTimer)
        {
            helpText.enabled = true;
        }
        text.text = $"Plugs Left: {4 - numberOfCorrectPlaces}";
    }

    public void InstantiatePlug(GameObject spawnLocation)
    {
        Transform position = spawnLocation.transform;
        Vector3 positionVector = position.position;
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0) // 0 spawns a neutralColorTracker
        {
            if (neutralColorTracker == 0)
            {
                InstantiatePlug(spawnLocation);
            }
            else
            {
                neutralColorTracker--;
                //Instantiate(neutralPlug, position, Quaternion.identity, topExtensionCord.transform);
                Instantiate(neutralPlug, positionVector, Quaternion.identity, this.transform);
            }
        }
        else if (randomIndex == 1) // 1 spawns color1
        {
            if (color1Tracker == 0)
            {
                InstantiatePlug(spawnLocation);
            }
            else
            {
                color1Tracker--;
                //Instantiate(color1, position, Quaternion.identity, topExtensionCord.transform);
                Instantiate(color1, positionVector, Quaternion.identity, this.transform);
            }
        }
        else if (randomIndex == 2) // 2 spawns color2
        {
            if (color2Tracker == 0)
            {
                InstantiatePlug(spawnLocation);
            }
            else
            {
                color2Tracker--;
                //Instantiate(color2, position, Quaternion.identity, topExtensionCord.transform);
                Instantiate(color2, positionVector, Quaternion.identity, this.transform);
            }
        }
    }
    public GameObject InstantiateSlot(Vector2 position, GameObject slot, string color)
    {
        GameObject newImage = Instantiate(slot, this.transform);
        UnityEngine.UI.Image newimage = newImage.GetComponent<UnityEngine.UI.Image>();

        RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
        newImageRectTransform.anchoredPosition = position;
        GetComponent<PlugInfo>().color = color;
        return slot;
    }
}

