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
    private Vector2[] topExtensionCordPositions; // An array to hold the Vector2 positions of the very top extension cord

    // Reference ExtensionCord Children Objects
    public GameObject ExtensionCord1;
    public GameObject ExtensionCord2;
    [HideInInspector] private string extensionCord1Color = null;
    [HideInInspector] private string extensionCord2Color = null;

    private Vector2[] extensionCord1Slots;
    private Vector2[] extensionCord2Slots;
    private List<GameObject> instantiatedSlots = new List<GameObject>();
    public GameObject[] colorIdentifier;
    public GameObject plugSlots;

    public static bool awakeCalled = false;

    protected float hD = 583f;
    protected float vD = 237f;

    private int color1Tracker = 2;
    private int color2Tracker = 2;
    private int neutralColorTracker = 2;
    private GameObject color1;
    private GameObject color2;


    private void Start()
    {
        Debug.Log("test1");
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
        Debug.Log("test3");
        topExtensionCordPositions = new Vector2[6];
        topExtensionCordPositions[0] = new Vector2(730, 698);
        topExtensionCordPositions[1] = new Vector2(818, 698);
        topExtensionCordPositions[2] = new Vector2(910, 698);
        topExtensionCordPositions[3] = new Vector2(1013, 698);
        topExtensionCordPositions[4] = new Vector2(1110, 698);
        topExtensionCordPositions[5] = new Vector2(1204, 698);

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
            InstantiatePlug(topExtensionCordPositions[i]);
        }

        GameObject eventObj = GameObject.Find("EventSystem");
        if (eventObj != null)
        {
            eventManager = eventObj.GetComponent<EventManager>();
        }

        //Plug positions
        //ExtensionCord1
        extensionCord1Slots = new Vector2[6];
        extensionCord1Slots[0] = new Vector2(730, 545);
        extensionCord1Slots[1] = new Vector2(818, 545);
        extensionCord1Slots[2] = new Vector2(910, 545);
        extensionCord1Slots[3] = new Vector2(1013, 545);
        extensionCord1Slots[4] = new Vector2(1110, 545);
        extensionCord1Slots[5] = new Vector2(1204, 545);
        //ExtensionCord2
        extensionCord2Slots = new Vector2[6];
        extensionCord2Slots[0] = new Vector2(730, 370);
        extensionCord2Slots[1] = new Vector2(818, 370);
        extensionCord2Slots[2] = new Vector2(910, 370);
        extensionCord2Slots[3] = new Vector2(1013, 370);
        extensionCord2Slots[4] = new Vector2(1110, 370);
        extensionCord2Slots[5] = new Vector2(1204, 370);

        for (int i = 0; i < extensionCord1Slots.Length; i++) //ExtensionCord1
        {
            GameObject newSlot = Instantiate(plugSlots, extensionCord1Slots[i], Quaternion.identity, transform);
            PlugTaskSlot slotScript = newSlot.GetComponent<PlugTaskSlot>();
            slotScript.color = extensionCord1Color;
            instantiatedSlots.Add(newSlot);
        }
        for (int i = 0; i < extensionCord2Slots.Length; i++) //ExtensionCord2
        {
            GameObject newSlot = Instantiate(plugSlots, extensionCord2Slots[i], Quaternion.identity, transform);
            PlugTaskSlot slotScript = newSlot.GetComponent<PlugTaskSlot>();
            slotScript.color = extensionCord2Color;
            instantiatedSlots.Add(newSlot);
        }

        Transform parentTransform = transform;
        // Create the Color Identifier
        Vector2 position1 = new Vector2(680, 547); //ExtensionCord1 position
        Vector2 position2 = new Vector2(680, 360); //ExtensionCord2 position
        Quaternion rotation = Quaternion.identity; // No rotation

        if (extensionCord1Color == "Blue")
        {
            Instantiate(colorIdentifier[0], position1, rotation, parentTransform);
        }
        else if (extensionCord1Color == "Yellow")
        {
            Instantiate(colorIdentifier[1], position1, rotation, parentTransform);
        }
        else if (extensionCord1Color == "Red")
        {
            Instantiate(colorIdentifier[2], position1, rotation, parentTransform);
        }
        else if (extensionCord1Color == "Green")
        {
            Instantiate(colorIdentifier[3], position1, rotation, parentTransform);
        }
        //
        if (extensionCord2Color == "Blue")
        {
            Instantiate(colorIdentifier[0], position2, rotation, parentTransform);
        }
        else if (extensionCord2Color == "Yellow")
        {
            Instantiate(colorIdentifier[1], position2, rotation, parentTransform);
        }
        else if (extensionCord2Color == "Red")
        {
            Instantiate(colorIdentifier[2], position2, rotation, parentTransform);
        }
        else if (extensionCord2Color == "Green")
        {
            Instantiate(colorIdentifier[3], position2, rotation, parentTransform);
        }
        Debug.Log(extensionCord1Color);
        Debug.Log(extensionCord2Color);
    }

    private void Update()
    {
        if (numberOfCorrectPlaces == 4)
        {
            Debug.Log("Task Completed");
            eventManager.ChecksTasksForID(taskID);
            eventManager.UpdateBoolArrayGivenID(taskID);
            Destroy(this.gameObject);
            awakeCalled = false;
        }
        text.text = $"Number of Plugs to match: {4 - numberOfCorrectPlaces}";
    }

    public void InstantiatePlug(Vector2 position)
    {
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0) // 0 spawns a neutralColorTracker
        {
            if (neutralColorTracker == 0)
            {
                InstantiatePlug(position);
            }
            else
            {
                neutralColorTracker--;
                //Instantiate(neutralPlug, position, Quaternion.identity, topExtensionCord.transform);
                Instantiate(neutralPlug, position, Quaternion.identity, this.transform);
            }
        }
        else if (randomIndex == 1) // 1 spawns color1
        {
            if (color1Tracker == 0)
            {
                InstantiatePlug(position);
            }
            else
            {
                color1Tracker--;
                //Instantiate(color1, position, Quaternion.identity, topExtensionCord.transform);
                Instantiate(color1, position, Quaternion.identity, this.transform);
            }
        }
        else if (randomIndex == 2) // 2 spawns color2
        {
            if (color2Tracker == 0)
            {
                InstantiatePlug(position);
            }
            else
            {
                color2Tracker--;
                //Instantiate(color2, position, Quaternion.identity, topExtensionCord.transform);
                Instantiate(color2, position, Quaternion.identity, this.transform);
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

