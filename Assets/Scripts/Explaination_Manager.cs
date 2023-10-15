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
    public GameObject Hazard2;
    public GameObject ArrowArrow;
    public GameObject ArrowScore;
    public GameObject ArrowCounter;
    public GameObject ArrowLife;
    public GameObject ArrowParent;
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
        Hazard2.SetActive(false);
        ArrowArrow.SetActive(false);
        ArrowScore.SetActive(false);
        ArrowCounter.SetActive(false);
        ArrowLife.SetActive(false);
        ArrowParent.SetActive(false);
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
                text.text = "Your house is under attack by an electric ghost! But don’t worry," +
                    "<br> i’m here to help you how to be safe!";
                counter++;
            }
            else if (counter == 2)
            {
                text.text = "First of all, we need to look for electrical problems, " +
                    "<br>like this one!";
                Hazard.SetActive(true);
                Hazard2.SetActive(true);
                counter++;
            }
            else if (counter == 3)
            {
                text.text = "They spawn randomly in the house, so you will need to <br> move around using the arrows on the left and right!";
                Hazard.SetActive(false);
                Hazard2.SetActive(false);
                ArrowArrow.SetActive(true);
                counter++;
            }
            else if (counter == 4)
            {
                text.text = "Solving them will give you safety points, and also stops the " +
                    "<br>electricity from overloading!";
                ArrowArrow.SetActive(false);
                ArrowScore.SetActive(true);
                counter++;
            }
            else if (counter == 5)
            {
                text.text = "The counter on the top shows how many hazards are present " +
                    "<br>Throughout the house!";
                ArrowScore.SetActive(false);
                ArrowCounter.SetActive(true);
                counter++;
            }
            else if (counter == 6)
            {
                text.text = "And finally, some hazards are very dangerous, so make sure to call <br>a parent by dragging them on it! ";
                counter++;
                ArrowCounter.SetActive(false);
                ArrowParent.SetActive(true);
            }
            else if (counter == 7)
            {
                text.text = "Make sure not to touch them yourself though! You might get badly hurt!";
                counter++;
                ArrowParent.SetActive(false);
                ArrowLife.SetActive(true);
            }
            else if (counter == 8)
            {
                text.text = "That’s all from me, let’s go and stop that ghost!";
                ArrowLife.SetActive(false);
                counter++;
            }
            else if (counter == 9)
            {
                ArrowExpl.SetActive(false);
                SceneManager.LoadSceneAsync(sceneToSwitchTo);
            }
        }
    }
}
