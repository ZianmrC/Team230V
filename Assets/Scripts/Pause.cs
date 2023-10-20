using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject anchor;
    public AnamolySpawner game;
    private GameObject pauseScreenInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseScreenInstance == null){
            game.continueGame();
        }

    }
    public void pauseGame()
    {
        pauseScreenInstance = Instantiate(pauseScreen, anchor.transform);
        pauseScreenInstance.transform.SetParent(anchor.transform);
        game.pauseGame();
        

    }
}
