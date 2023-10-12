using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Explaination_Manager : MonoBehaviour
{
    public GameObject ArrowExpl;
    public TextMeshProUGUI text;
    public GameObject Hazard;
    public string sceneToSwitchTo;
    private int counter = 0;

    private void Start()
    {
        InitializeObjects();
    }

    private void InitializeObjects()
    {
        ArrowExpl.SetActive(true);
        Hazard.SetActive(false);
        counter = 0;
        text.text = "Hey there! I’m Todd, a fellow safety hero!";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (counter == 0)
            {
                
                counter++;
            }
            else if (counter == 1)
            {
                text.text = "Your house is under attack by an electric ghost! But don’t worry, i’m here to help you how to be safe!";
                counter++;
            }
            else if (counter == 2)
            {
                text.text = "First of all, we need to look for electrical hazards, like this one!";
                Hazard.SetActive(true);
                counter++;
            }
            else if (counter == 3)
            {
                text.text = "Solving them will give you safety points, and also stops the electricity from overloading!";
                Hazard.SetActive(false);
                counter++;
            }
            else if (counter == 4)
            {
                text.text = "The counter on the top shows how many hazards are present Throughout the house!";
                counter++;
            }
            else if (counter == 5)
            {
                text.text = "And finally, some hazards are very dangerous, so make sure to call a parent by dragging them on it! ";
                counter++;
            }
            else if (counter == 6)
            {
                text.text = "That’s all from me, let’s go and stop that ghost!";
                counter++;
            }
            else if (counter == 7)
            {
                ArrowExpl.SetActive(false);
                SceneManager.LoadSceneAsync(sceneToSwitchTo);
            }
        }
    }
}
