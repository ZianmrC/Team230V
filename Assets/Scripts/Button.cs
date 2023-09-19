using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameCamera gameCamera;
    public GameObject kitchen;
    public GameObject livingroom;
    public GameObject bathroom;
    public GameObject targetRoom;
    private string currentRoom = "Kitchen";
    public bool isLeftButton;

    EventManager eventManager;

    //Variables only Applicable to Tasks that use Parent Mechanic
    public GameObject taskToRemove;
    private int taskID;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = "Kitchen";
        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
        if(taskToRemove != null ) //If button is used in Parent Task
        {
            if (taskToRemove.GetComponent<BrokenToasterTask>() != null)
            {
                taskID = taskToRemove.GetComponent<BrokenToasterTask>().taskID;
                score = taskToRemove.GetComponent<BrokenToasterTask>().score;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void ChangeRoom()
    {
        DetermineTargetRoom();
        gameCamera.Move(targetRoom);
    }
    public GameObject DetermineTargetRoom()
    {
        if (isLeftButton && currentRoom == "Kitchen")
        {
            gameCamera.Move(bathroom);
            currentRoom = "Bathroom";
            targetRoom = bathroom;
            Debug.Log("1");
        }
        else if (!isLeftButton && currentRoom == "Kitchen")
        {
            gameCamera.Move(livingroom);
            currentRoom = "Livingroom";
            targetRoom = livingroom;
            Debug.Log("2");
        }
        else if (isLeftButton && currentRoom == "Bathroom")
        {
            gameCamera.Move(livingroom);
            currentRoom = "Livingroom";
            targetRoom = livingroom;
            Debug.Log("3");

        }
        else if (!isLeftButton && currentRoom == "Bathroom")
        {
            gameCamera.Move(kitchen);
            currentRoom = "Kitchen";
            targetRoom = kitchen;
            Debug.Log("4");
        }
        else if (isLeftButton && currentRoom == "Livingroom")
        {
            gameCamera.Move(kitchen);
            currentRoom = "Kitchen";
            targetRoom = kitchen;
            Debug.Log("5");
        }
        else if (!isLeftButton && currentRoom == "Livingroom")
        {
            gameCamera.Move(bathroom);
            currentRoom = "Bathroom";
            targetRoom = bathroom;
            Debug.Log("6");
        }
        return targetRoom;
    }
    public void LoseLife()
    {
        eventManager.LoseLife();
        RemoveTask(taskToRemove);
    }

    public void RemoveTask(GameObject task) //Called after parent task was clicked on, resulting in Lostlife
    {
        eventManager.ChecksTasksForID(taskID);
        eventManager.UpdateBoolArrayGivenID(taskID);
        eventManager.AddScore(score);
        Destroy(task);
    }
}
