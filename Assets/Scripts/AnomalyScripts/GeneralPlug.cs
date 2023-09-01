using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GeneralPlug : MonoBehaviour
{
    public GameObject[] imageArray;
    public int numberOfPlugs; // The number of plugs that spawns

    public int numberOfColors; //The number of different colored plugs to match, if it's 3, 3 extension cords will spawn instead of 2
    private string[] colors = { "Blue", "Yellow", "Red", "Green" }; 
    private string[] colorsToMatch;

    //Reference ExtensionCord Children Objects
    public GameObject ExtensionCord1;
    public GameObject ExtensionCord2;
    private string extensionCord1Color;
    private string extensionCord2Color;

    private bool[] correctPlacements; //An array to track if the correct plug has been placed into the correct socket
    // Start is called before the first frame updates
    void Awake()
    {
        //ENSURE TO FIX THIS ONCE ACTUAL ASSETS MADE

        //Pick 2 Random Colors
        while (extensionCord2Color == null || extensionCord1Color == null)
        {
            int randomIndex = Random.Range(0, colors.Length);
            int randomIndex2 = Random.Range(0, colors.Length);
            extensionCord1Color = colors[randomIndex];
            extensionCord2Color = colors[randomIndex2];
            if(extensionCord1Color == extensionCord2Color)
            {
                extensionCord2Color = null;
            }
        }

        //Reference 2 Extension Cord Child Objects
        /*
        Transform extensionCord1Transform = transform.Find("ExtensionCord1");
        Transform extensionCord2Transform = transform.Find("ExtensionCord2");
        ExtensionCord1 = extensionCord1Transform.gameObject;
        ExtensionCord2 = extensionCord2Transform.gameObject;
        */
        ExtensionCord1.GetComponent<ExtensionCord>().color = extensionCord1Color;
        ExtensionCord1.GetComponent<ExtensionCord>().otherColor = extensionCord2Color;
        ExtensionCord2.GetComponent<ExtensionCord>().color = extensionCord2Color;
        ExtensionCord2.GetComponent<ExtensionCord>().otherColor = extensionCord1Color;

        Debug.Log($"Color after assigning: {extensionCord1Color}");
        Debug.Log($"Color after assigning: {extensionCord2Color}");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InstantiateImage(Vector2 position, GameObject image)
    {
        GameObject newImage = Instantiate(image, this.transform);
        UnityEngine.UI.Image newimage = newImage.GetComponent<UnityEngine.UI.Image>();

        RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
        newImageRectTransform.anchoredPosition = position;
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
