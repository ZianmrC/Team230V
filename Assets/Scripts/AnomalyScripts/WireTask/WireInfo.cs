using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class WireInfo : MonoBehaviour
{
    public int wireNumber; //Used to determine what part of the wire this script is attached to
    public int plugNumber;
    public GameObject plug;

    private BoxCollider2D collider;
    private bool plugDragged;
    private bool isMouseOverObject;

    private RectTransform canvasRectTransform;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        canvasRectTransform = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<RectTransform>();
    }

}
/*https://www.hiclipart.com/free-transparent-background-png-clipart-pfugd - yellow_plug sprite used
https://www.gamedeveloperstudio.com/graphics/viewgraphic.php?item=1k4w6c4g805p3h6m8i - pipe tileset used
*/
