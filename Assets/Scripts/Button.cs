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
    // Start is called before the first frame update
    void Start()
    {
        currentRoom = "Kitchen";
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (isLeftButton && currentRoom == "Kitchen")
                    {
                        gameCamera.Move(livingroom);
                        currentRoom = "Livingroom";
                        targetRoom = livingroom;
                    }
                    else if (!isLeftButton && currentRoom == "Kitchen")
                    {
                        gameCamera.Move(bathroom);
                        currentRoom = "Bathroom";
                        targetRoom = bathroom;
                    }
                    else if (isLeftButton && currentRoom == "Bathroom")
                    {
                        gameCamera.Move(kitchen);
                        currentRoom = "Kitchen";
                        targetRoom = kitchen;

                    }
                    else if (!isLeftButton && currentRoom == "Bathroom")
                    {
                        gameCamera.Move(livingroom);
                        currentRoom = "Livingroom";
                        targetRoom = livingroom;
                    }
                    else if (isLeftButton && currentRoom == "Livingroom")
                    {
                        gameCamera.Move(bathroom);
                        currentRoom = "Bathroom";
                        targetRoom = bathroom;

                    }
                    else if (!isLeftButton && currentRoom == "Livingroom")
                    {
                        gameCamera.Move(kitchen);
                        currentRoom = "Kitchen";
                        targetRoom = kitchen;
                    }
                }
            }
        }
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
            gameCamera.Move(livingroom);
            currentRoom = "Livingroom";
            targetRoom = livingroom;
        }
        else if (!isLeftButton && currentRoom == "Kitchen")
        {
            gameCamera.Move(bathroom);
            currentRoom = "Bathroom";
            targetRoom = bathroom;
        }
        else if (isLeftButton && currentRoom == "Bathroom")
        {
            gameCamera.Move(kitchen);
            currentRoom = "Kitchen";
            targetRoom = kitchen;
        }
        else if (!isLeftButton && currentRoom == "Bathroom")
        {
            gameCamera.Move(livingroom);
            currentRoom = "Livingroom";
            targetRoom = livingroom;
        }
        else if (isLeftButton && currentRoom == "Livingroom")
        {
            gameCamera.Move(bathroom);
            currentRoom = "Bathroom";
            targetRoom = bathroom;

        }
        else if (!isLeftButton && currentRoom == "Livingroom")
        {
            gameCamera.Move(kitchen);
            currentRoom = "Kitchen";
            targetRoom = kitchen;
        }
        return targetRoom;
    }
}
