using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenTuto : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject panel;

    public void OnMouseDown()
    {
        panel.SetActive(true);
    }

    public void Back()
    {
        panel.SetActive(false);
    }
}
