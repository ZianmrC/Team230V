using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionCord : GeneralPlug
{
    private Vector2[] extensionCordSlots;
    public GameObject[] colorIdentifier;
    public GameObject[] coloredPlugs;
    public GameObject plugSlots;
    public string color; //The color the Extension cord is supposed to hold
    public string otherColor; //The color of the other extension cord
    public bool allCorrect;

    private float hD = 588f;
    private float vD = 241f;
    private List<GameObject> instantiatedSlots = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //Plug Positions for extensionCord1;
        extensionCordSlots = new Vector2[6];
        extensionCordSlots[0] = new Vector2(-141 + hD, 0 + vD);
        extensionCordSlots[1] = new Vector2(-86 + hD, 0 + vD);
        extensionCordSlots[2] = new Vector2(-32 + hD, 0 + vD);
        extensionCordSlots[3] = new Vector2(32 + hD, 0 + vD);
        extensionCordSlots[4] = new Vector2(89 + hD, 0 + vD);
        extensionCordSlots[5] = new Vector2(148 + hD, 0 + vD);

        for (int i = 0; i < extensionCordSlots.Length; i++)
        {
            GameObject newSlot = Instantiate(plugSlots, extensionCordSlots[i], Quaternion.identity, transform);
            PlugTask_ExtensionCordSlot slotScript = newSlot.GetComponent<PlugTask_ExtensionCordSlot>();
            slotScript.color = color;
            instantiatedSlots.Add(newSlot);
        }


        // Create the Color Identifier
        Vector2 position = new Vector2(-172, 0);
        Quaternion rotation = Quaternion.identity; // No rotation
        Transform parentTransform = transform; // Parent transform

        if (color == "Blue")
        {
            Instantiate(colorIdentifier[0], position, rotation, parentTransform);
        }
        else if (color == "Yellow")
        {
            Instantiate(colorIdentifier[1], position, rotation, parentTransform);
        }
        else if (color == "Red")
        {
            Instantiate(colorIdentifier[2], position, rotation, parentTransform);
        }
        else if (color == "Green")
        {
            Instantiate(colorIdentifier[3], position, rotation, parentTransform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlugSlots();
    }
    private void CheckPlugSlots()
    {
        foreach(var slot in instantiatedSlots)
        {
            PlugTask_ExtensionCordSlot slotScript = slot.GetComponent<PlugTask_ExtensionCordSlot>();
            if(!slotScript.correctlyPlaced)
            {
                allCorrect = false;
            }
        }
        allCorrect = true;
    }
}
