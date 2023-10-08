using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class generalWaterTask : MonoBehaviour
{
    public int taskID;

        public GameObject whipe;
        private Canvas canvas; // Declare the canvas variable

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject whipeInstnace = Instantiate(whipe);
        whipeInstnace.transform.SetParent(canvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
