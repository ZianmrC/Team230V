using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explaination_Manager : MonoBehaviour
{
    public GameObject ArrowExpl;
    public GameObject ParentExpl;
    public GameObject Anomaly2Expl;
    public GameObject Anomaly1Expl;
    public string sceneToSwitchTo;
    private int counter = 0;

    private void Start()
    {
        InitializeObjects();
    }

    private void InitializeObjects()
    {
        ArrowExpl.SetActive(false);
        ParentExpl.SetActive(false);
        Anomaly2Expl.SetActive(false);
        Anomaly1Expl.SetActive(true);
        counter = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (counter == 0)
            {
                Anomaly1Expl.SetActive(false);
                Anomaly2Expl.SetActive(true);
                counter++;
            }
            else if (counter == 1)
            {
                Anomaly2Expl.SetActive(false);
                ParentExpl.SetActive(true);
                counter++;
            }
            else if (counter == 2)
            {
                ParentExpl.SetActive(false);
                ArrowExpl.SetActive(true);
                counter++;
            }
            else if (counter == 3)
            {
                ArrowExpl.SetActive(false);
                SceneManager.LoadSceneAsync(sceneToSwitchTo);
            }
        }
    }
}
