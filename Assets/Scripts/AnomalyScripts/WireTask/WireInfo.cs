using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireInfo : MonoBehaviour
{
    public int wireNumber;
    public int plugNumber;
    public GameObject plug;

    private BoxCollider2D collider;
    private bool plugDragged;
    private bool isMouseOverObject;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isMouseOverObject = collider.OverlapPoint(mousePosition);
        plugDragged = plug.GetComponent<WireTaskDragPlug>().isDragging;
        if(plugDragged && isMouseOverObject )
        {
            Debug.Log("test");
        }
        else if(isMouseOverObject)
        {
            Debug.Log("test2");
        }
        Debug.Log(mousePosition);
    }
}
/*https://www.hiclipart.com/free-transparent-background-png-clipart-pfugd - yellow_plug sprite used
https://www.gamedeveloperstudio.com/graphics/viewgraphic.php?item=1k4w6c4g805p3h6m8i - pipe tileset used
*/
