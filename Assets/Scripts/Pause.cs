using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject anchor;
    public AnamolySpawner game;
    private GameObject pauseScreenInstance;
    public bool gamePaused;
    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseScreenInstance == null){
            gamePaused = false;
            game.continueGame();
        }
        else{
            gamePaused = true;
        }

    }
    public void pauseGame()
    {
        if(!gamePaused){
            pauseScreenInstance = Instantiate(pauseScreen, anchor.transform);
            pauseScreenInstance.transform.SetParent(anchor.transform);
            game.pauseGame();
            gamePaused = true;
        }

    }
    public void gamecontinue(){
        gamePaused = false;
    }
}
