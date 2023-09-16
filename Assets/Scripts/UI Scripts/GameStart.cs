using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private SwitchScene switchscene;

    void Start()
    {
        switchscene = GetComponent<SwitchScene>();
    }

    void Update()
    {
       
    }

    private void OnMouseDown()
    {
        switchscene.SwitchScenes();
    }
}
