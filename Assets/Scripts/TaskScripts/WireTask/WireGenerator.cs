using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private Vector2[] plugSockets;
    private float currentTimer;

    [Header("Tooltip/Mascot Variables")]
    public GameObject wireMascot;
    private GameObject textObject;
    private TextMeshProUGUI text;
    private Vector2 originPosition;
    private Vector2 endPosition;
    private RectTransform rect;
    public float moveSpeed = 500f;
    private bool stopTooltip;
    private bool spoken;
    private void Awake()
    {
        originPosition = new Vector2(-220, -696); spoken = false;
        endPosition = new Vector2(-220, -374);
        if(taskVariables.wireDifficulty1Time < EventManager.TotalGameTime)
        {
            plugSockets = new Vector2[4];
            plugSockets[0] = new Vector2(-300, 150);
            plugSockets[1] = new Vector2(-100, 150);
            plugSockets[2] = new Vector2(100, 150);
            plugSockets[3] = new Vector2(300, 150);
        }
        else
        {
            plugSockets = new Vector2[3];
            plugSockets[0] = new Vector2(-300, 150);
            plugSockets[1] = new Vector2(0, 150);
            plugSockets[2] = new Vector2(300, 150);
        }
        Shuffle(plugSockets);
        numberOfWires = Layout1(1, plugSockets[0]) + Layout2(2, plugSockets[1]) + Layout3(3, plugSockets[2]);
    }
    private Vector2[] Shuffle(Vector2[] array)
    {
        int n = array.Length;
        for(int i = 0; i<n; i++)
        {
            int randomIndex = Random.Range(i, n);
            Vector2 temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
    private void Start()
    {
        currentTimer = 0f; stopTooltip = false;
        wireCounter = 0;
        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();

        rect = wireMascot.GetComponent<RectTransform>();
        rect.anchoredPosition = originPosition;
        Transform textObject = transform.Find("Mascot/Container/Image/Text (TMP)");
        text = textObject.GetComponent<TextMeshProUGUI>();

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
        currentTimer += Time.deltaTime;
        if (currentTimer > taskVariables.wireHelpTime && !stopTooltip)
        {
            if (rect.anchoredPosition.y < endPosition.y)
            {
                float newY = rect.anchoredPosition.y + moveSpeed * Time.deltaTime;
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);
            }
            else if(!stopTooltip)
            {
                text.enabled = true;
                if (spoken == false)
                {
                    EventManager.PlayAudioSource("Mumbling");
                    spoken = true;
                }
                text.text = "The wires seem untangled! Try clicking and \ndragging the plug, tracing the path of the \nwire its connect to.";
                if(wireCounter >= 5)
                {
                    stopTooltip = true;
                    rect.anchoredPosition = originPosition;
                }
            }
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

    int Layout1(int plugNumber, Vector2 startLocation) //Start location refers to what starting cord's location is
    {
        Vector2 a = startLocation;
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
    int Layout2(int plugNumber, Vector2 startLocation)
    {
        Vector2 a = startLocation;
        //Instantiate Vector2 Array containing Locations
        Vector2[] positions = new Vector2[20];
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
        positions[18] = (new Vector2(0, -6) * gridSize) + a;
        positions[19] = (new Vector2(0, -7) * gridSize) + a;

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
        InstantiateWire(vertical, positions[18], 18, newPlug, plugNumber);
        InstantiateWire(vertical, positions[19], 19, newPlug, plugNumber);
        

        Instantiate(newPlug, this.transform);
        return positions.Length - 1;
    }
    int Layout3(int plugNumber, Vector2 startLocation)
    {
        Vector2 a = startLocation;
        //Instantiate Vector2 Array containing Locations
        Vector2[] positions = new Vector2[28];
        positions[0] = (new Vector2(0, 0) * gridSize) + a; // 'a' is the start location of the particular entangled wire
        positions[1] = (new Vector2(0, -1) * gridSize) + a;
        positions[2] = (new Vector2(0, -2) * gridSize) + a;
        positions[3] = (new Vector2(-1, -2) * gridSize) + a; 
        positions[4] = (new Vector2(-2, -2) * gridSize) + a; 
        positions[5] = (new Vector2(-2, -3) * gridSize) + a; 
        positions[6] = (new Vector2(-2, -4) * gridSize) + a; 
        positions[7] = (new Vector2(-2, -5) * gridSize) + a; 
        positions[8] = (new Vector2(-2, -6) * gridSize) + a; 
        positions[9] = (new Vector2(-1, -6) * gridSize) + a; 
        positions[10] = (new Vector2(0, -6) * gridSize) + a; 
        positions[11] = (new Vector2(1, -6) * gridSize) + a; 
        positions[12] = (new Vector2(2, -6) * gridSize) + a; 
        positions[13] = (new Vector2(2, -5) * gridSize) + a; 
        positions[14] = (new Vector2(2, -4) * gridSize) + a; 
        positions[15] = (new Vector2(2, -3) * gridSize) + a; 
        positions[16] = (new Vector2(1, -3) * gridSize) + a; 
        positions[17] = (new Vector2(0, -3) * gridSize) + a; 
        positions[18] = (new Vector2(-1, -3) * gridSize) + a; 
        positions[19] = (new Vector2(-1, -4) * gridSize) + a; 
        positions[20] = (new Vector2(-1, -5) * gridSize) + a; 
        positions[21] = (new Vector2(0, -5) * gridSize) + a; 
        positions[22] = (new Vector2(1, -5) * gridSize) + a; 
        positions[23] = (new Vector2(1, -4) * gridSize) + a; 
        positions[24] = (new Vector2(0, -4) * gridSize) + a; 
        positions[25] = (new Vector2(0, -5) * gridSize) + a; 
        positions[26] = (new Vector2(0, -6) * gridSize) + a; 
        positions[27] = (new Vector2(0, -7) * gridSize) + a; 
        

        GameObject newPlug = plug;
        RectTransform rect = newPlug.GetComponent<RectTransform>();
        WireTaskDragPlug wireTask = newPlug.GetComponent<WireTaskDragPlug>();
        wireTask.plugNumber = plugNumber;
        rect.anchoredPosition = positions[0];

        //Instantiate WireTypes using Vector2 Array
        InstantiateWire(vertical, positions[1], 1, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[2], 2, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[3], 3, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[4], 4, newPlug, plugNumber);
        InstantiateWire(vertical, positions[5], 5, newPlug, plugNumber);
        InstantiateWire(vertical, positions[6], 6, newPlug, plugNumber);
        InstantiateWire(vertical, positions[7], 7, newPlug, plugNumber);
        InstantiateWire(bottomLeftCurve, positions[8], 8, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[9], 9, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[10], 10, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[11], 11, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[12], 12, newPlug, plugNumber);
        InstantiateWire(vertical, positions[13], 13, newPlug, plugNumber);
        InstantiateWire(vertical, positions[14], 14, newPlug, plugNumber);
        InstantiateWire(topRightCurve, positions[15], 15, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[16], 16, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[17], 17, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[18], 18, newPlug, plugNumber);
        InstantiateWire(vertical, positions[19], 19, newPlug, plugNumber);
        InstantiateWire(bottomLeftCurve, positions[20], 20, newPlug, plugNumber);
        InstantiateWire(horizontal, positions[21], 21, newPlug, plugNumber);
        InstantiateWire(bottomRightCurve, positions[22], 22, newPlug, plugNumber);
        InstantiateWire(topRightCurve, positions[23], 23, newPlug, plugNumber);
        InstantiateWire(topLeftCurve, positions[24], 24, newPlug, plugNumber);
        InstantiateWire(vertical, positions[25], 25, newPlug, plugNumber);
        InstantiateWire(vertical, positions[26], 26, newPlug, plugNumber);
        InstantiateWire(vertical, positions[27], 27, newPlug, plugNumber);
        
        Instantiate(newPlug, this.transform);
        return positions.Length - 1;
    }
}