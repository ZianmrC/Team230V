using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameCamera gameCamera;
    public GameObject targetRoom;

    // Start is called before the first frame update
    void Start()
    {
        
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
                        gameCamera.Move(targetRoom);
                        Debug.Log(targetRoom);
                    }
                }
            }
        }

    public void ChangeRoom()
    {
        Debug.Log("Changing room");
        gameCamera.Move(targetRoom);
    }
}
