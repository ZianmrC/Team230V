using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class WireGenerator : MonoBehaviour
{
    EventManager eventManager;
    public TaskVariables taskVariables;
    //Wire Image Variables
    public GameObject topLeftCurve;
    public GameObject topRightCurve;
    public GameObject bottomLeftCurve;
    public GameObject bottomRightCurve;
    public GameObject horizontal;
    public GameObject vertical;
    public GameObject plug;

    [HideInInspector] public int wireCounter;
    private int numberOfWires; //int to keep track of the number of wires spawned
    [HideInInspector] public int taskID;
    private bool awakeCalled;

    //Other Variables
    public float gridSize;
    public GameObject[] plugSockets;

    private void Awake()
    {
        numberOfWires = Layout1(1, plugSockets[0]) + Layout2(2, plugSockets[1]);
    }
    private void Start()
    {
        wireCounter = 0;
        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
        Debug.Log(numberOfWires);
    }
    private void Update()
    {
        if(wireCounter == numberOfWires)
        {
            Debug.Log("Task Completed");
            eventManager.ChecksTasksForID(taskID);
            eventManager.UpdateBoolArrayGivenID(taskID);
            eventManager.AddScore(taskVariables.wireScore);
            eventManager.DecreaseOverload(2);
            wireCounter = 0;
            Destroy(this.gameObject);
            awakeCalled = false;
        }
    }
    private void InstantiateWire(GameObject wireType, Vector2 position, int wireNumber, GameObject plug, int plugNumber)
    {
        GameObject newWire = Instantiate(wireType, this.transform);
        RectTransform rectTransform = newWire.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;

        WireInfo wireInfo = newWire.GetComponent<WireInfo>();
        wireInfo.wireNumber = wireNumber;
        wireInfo.plugNumber = plugNumber;
        wireInfo.plug = plug; //Assign the dragable plug as a field variable
    }

    int Layout1(int plugNumber, GameObject startLocation) //Start location refers to what starting cord's location is
    {
        RectTransform transform = startLocation.GetComponent<RectTransform>();
        Vector2 a = transform.anchoredPosition;
        //Instantiate Vector2 Array containing Locations
        Vector2[] positions = new Vector2[18];
        positions[0] = (new Vector2(0, 0) * gridSize) + a; // 'a' is the start location of the particular entangled wire
        positions[1] = (new Vector2(0, -1) * gridSize) + a;
        positions[2] = (new Vector2(0, -2) * gridSize) + a;
        positions[3] = (new Vector2(0, -3) * gridSize) + a;
        positions[4] = (new Vector2(1, -3) * gridSize) + a;
        positions[5] = (new Vector2(2, -3) * gridSize) + a;
        positions[6] = (new Vector2(3, -3) * gridSize) + a;
        positions[7] = (new Vector2(4, -3) * gridSize) + a;
        positions[8] = (new Vector2(4, -4) * gridSize) + a;
        positions[9] = (new Vector2(4, -5) * gridSize) + a;
        positions[10] = (new Vector2(3, -5) * gridSize) + a;
        positions[11] = (new Vector2(2, -5) * gridSize) + a;
        positions[12] = (new Vector2(1, -5) * gridSize) + a;
        positions[13] = (new Vector2(0, -5) * gridSize) + a;
        positions[14] = (new Vector2(-1, -5) * gridSize) + a;
        positions[15] = (new Vector2(-1, -6) * gridSize) + a;
        positions[16] = (new Vector2(0, -6) * gridSize) + a;
        positions[17] = (new Vector2(0, -7) * gridSize) + a;

        GameObject newPlug = plug;
        RectTransform rect = newPlug.GetComponent<RectTransform>();
        WireTaskDragPlug wireTask = newPlug.GetComponent<WireTaskDragPlug>();
        wireTask.wireGeneratorPrefab = this.gameObject;
        wireTask.plugNumber = plugNumber;
        rect.anchoredPosition = positions[0];

        //Instantiate WireTypes using Vector2 Array
        InstantiateWire(vertical, positions[1], 1, newPlug, plugNumber);
        InstantiateWire(vertical, positions[2], 2, newPlug, plugNumber);
        InstantiateWire(bottomLeftCurve, positions[3], 3, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[4], 4, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[5], 5, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[6], 6, newPlug, plugNumber);
        InstantiateWire(topRightCurve, positions[7], 7, newPlug, plugNumber);
        InstantiateWire(vertical, positions[8], 8, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[9], 9, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[10], 10, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[11], 11, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[12], 12, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[13], 13, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[14], 14, newPlug, plugNumber);
        InstantiateWire(bottomLeftCurve, positions[15], 15, newPlug, plugNumber);
        InstantiateWire(topRightCurve, positions[16], 16, newPlug, plugNumber);
        InstantiateWire(vertical, positions[17], 17, newPlug, plugNumber);

        Instantiate(newPlug, this.transform);
        return positions.Length - 1;
    }
    int Layout2(int plugNumber, GameObject startLocation)
    {
        RectTransform transform = startLocation.GetComponent<RectTransform>();
        Vector2 a = transform.anchoredPosition;
        //Instantiate Vector2 Array containing Locations
        Vector2[] positions = new Vector2[18];
        positions[0] = (new Vector2(0, 0) * gridSize) + a; // 'a' is the start location of the particular entangled wire
        positions[1] = (new Vector2(0, -1) * gridSize) + a;
        positions[2] = (new Vector2(0, -2) * gridSize) + a;
        positions[3] = (new Vector2(0, -3) * gridSize) + a;
        positions[4] = (new Vector2(-1, -3) * gridSize) + a;
        positions[5] = (new Vector2(-2, -3) * gridSize) + a;
        positions[6] = (new Vector2(-3, -3) * gridSize) + a;
        positions[7] = (new Vector2(-3, -4) * gridSize) + a;
        positions[8] = (new Vector2(-2, -4) * gridSize) + a;
        positions[9] = (new Vector2(-1, -4) * gridSize) + a;
        positions[10] = (new Vector2(-1, -3) * gridSize) + a;
        positions[11] = (new Vector2(-1, -2) * gridSize) + a;
        positions[12] = (new Vector2(0, -2) * gridSize) + a;
        positions[13] = (new Vector2(1, -2) * gridSize) + a;
        positions[14] = (new Vector2(1, -3) * gridSize) + a;
        positions[15] = (new Vector2(1, -4) * gridSize) + a;
        positions[16] = (new Vector2(0, -4) * gridSize) + a;
        positions[17] = (new Vector2(0, -5) * gridSize) + a;

        GameObject newPlug = plug;
        RectTransform rect = newPlug.GetComponent<RectTransform>();
        WireTaskDragPlug wireTask = newPlug.GetComponent<WireTaskDragPlug>();
        wireTask.plugNumber = plugNumber;
        rect.anchoredPosition = positions[0];

        //Instantiate WireTypes using Vector2 Array
        InstantiateWire(vertical, positions[1], 1, newPlug, plugNumber);
        InstantiateWire(vertical, positions[2], 2, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[3], 3, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[4], 4, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[5], 5, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[6], 6, newPlug, plugNumber);
        InstantiateWire(bottomLeftCurve, positions[7], 7, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[8], 8, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[9], 9, newPlug, plugNumber);
        InstantiateWire(vertical, positions[10], 10, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[11], 11, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[12], 12, newPlug, plugNumber);
        InstantiateWire(topRightCurve, positions[13], 13, newPlug, plugNumber);
        InstantiateWire(vertical, positions[14], 14, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[15], 15, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[16], 16, newPlug, plugNumber);
        InstantiateWire(vertical, positions[17], 17, newPlug, plugNumber);
        

        Instantiate(newPlug, this.transform);
        return positions.Length - 1;
    }
}