using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkTask : MonoBehaviour
{
    public int taskID;
    public GameObject tagDraggable;
    private TagDraggable tagScript;
    // Start is called before the first frame update
    void Start()
    {
        //Pass TaskID to TagDraggable
        tagScript = tagDraggable.GetComponent<TagDraggable>();
        tagScript.taskID = taskID;
    }

   
}
/* Water Tap image from:
 * https://www.vecteezy.com/vector-art/13330055-water-tap-faucet-icon-vector-design-template
 */