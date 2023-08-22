using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRoomClass : MonoBehaviour
{
    public string targetSceneName; // Name of the scene to load
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeRoom()
    {
        Debug.Log("Changing to scene: " + targetSceneName);
        SceneManager.LoadScene(targetSceneName);
    }

}
