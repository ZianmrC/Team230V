using System.Collections.Generic;
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

    //Other Variables
    public float gridSize;
    public GameObject plug1;

    private void Start()
    {
        Layout1(1, plug1);
    }
    private void Update()
    {

    }
    private void InstantiateWire(GameObject wire, Vector2 grid, int wireNumber)
    {
        Vector3 position = new Vector3(grid.x, grid.y, 0f);
        Instantiate(wire, this.transform);
        RectTransform rectTransform = wire.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;

        WireInfo component = wire.GetComponent<WireInfo>();
        component.wireNumber = wireNumber;
    }
    void Layout1(int wireNumber, GameObject startLocation)
    {
        RectTransform transform = startLocation.GetComponent<RectTransform>();
        Vector2 a = transform.anchoredPosition;
        Vector2[] positions = new Vector2[10];
        positions[0] = (new Vector2(0, 1) * gridSize) + a;

        InstantiateWire(horizontal, positions[0], wireNumber);
    }
}