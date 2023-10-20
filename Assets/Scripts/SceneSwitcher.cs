using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToStartMenuScene()
    {
        Debug.Log("click detected");
        // Replace "StartMenu (Prototype)" with the name of your scene

        SceneManager.LoadScene("StartMenu (Prototype)");
    }
}
