using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class WireGenerator : MonoBehaviour
{
    //Wire Image Variables
    public GameObject topLeftCurve;
    public GameObject topRightCurve;
    public GameObject bottomLeftCurve;
    public GameObject bottomRightCurve;
    public GameObject horizontal;
    public GameObject vertical;
    public GameObject plug;

    //Other Variables
    public float gridSize;
    public GameObject[] plugSockets;

    private void Awake()
    {
        Layout1(1, plugSockets[0]);
    }
    private void Start()
    {
    }
    private void Update()
    {

    }
    private void InstantiateWire(GameObject wireType, Vector2 position, int wireNumber, GameObject plug)
    {
        GameObject newWire = Instantiate(wireType, this.transform);
        RectTransform rectTransform = newWire.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;

        WireInfo wireInfo = newWire.GetComponent<WireInfo>();
        wireInfo.wireNumber = wireNumber;
        wireInfo.plug = plug; //Assign the dragable plug as a field variable
    }

    void Layout1(int wireNumber, GameObject startLocation) //Start location refers to what starting cord's location is
    {
        RectTransform transform = startLocation.GetComponent<RectTransform>();
        Vector2 a = transform.anchoredPosition;
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
        rect.anchoredPosition = positions[0];

        InstantiateWire(vertical, positions[1], 2, newPlug);
        InstantiateWire(vertical, positions[2], 3, newPlug);
        InstantiateWire(bottomLeftCurve, positions[3], 4, newPlug);
        InstantiateWire(horizontal, positions[4], 5, newPlug);
        InstantiateWire(horizontal, positions[5], 6, newPlug);
        InstantiateWire(horizontal, positions[6], 7, newPlug);
        InstantiateWire(topRightCurve, positions[7], 8, newPlug);
        InstantiateWire(vertical, positions[8], 9, newPlug);
        InstantiateWire(bottomRightCurve, positions[9], 10, newPlug);
        InstantiateWire(horizontal, positions[10], 11, newPlug);
        InstantiateWire(horizontal, positions[11], 12, newPlug);
        InstantiateWire(horizontal, positions[12], 13, newPlug);
        InstantiateWire(horizontal, positions[13], 14, newPlug);
        InstantiateWire(topLeftCurve, positions[14], 15, newPlug);
        InstantiateWire(bottomLeftCurve, positions[15], 16, newPlug);
        InstantiateWire(topRightCurve, positions[16], 17, newPlug);
        InstantiateWire(vertical, positions[17], 18, newPlug);

        Instantiate(newPlug, this.transform);
    }
}