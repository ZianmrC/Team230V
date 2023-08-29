using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject livingRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRoomKeyboard();
    }
    void ChangeRoomKeyboard(){
        if(Input.GetKey("c"))
            Move(livingRoom);
    }

    // Move the camera
    public void Move(GameObject targetRoom) {
        // Set the camera’s position
        Vector3 position = targetRoom.transform.position;
        position.y = this.transform.position.y;
        position.z = this.transform.position.z;
        this.transform.position = position;
    }
}
