using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class EventManager : MonoBehaviour
{
    Button leftButton;
    Button rightButton;
    public TextMeshProUGUI electricityText;
    public TextMeshProUGUI scoreText;
    public  TextMeshProUGUI damageText;
    private bool tookDamage;
    public bool IncreaseOverload;
    private float damageTimer;

    public static string GameOverScene = "GameOver Scene";
    public static bool menuOpened;
    public static int taskCounter = 0;
    public static float TotalGameTime;
    private float updateTimer;
    private float percentage;
    public TextMeshProUGUI taskCounterText;
    public int numberOfTasksUntilOverload = 2; //The minimum number of tasks present in order to start incrementing the Electricity Overload
    public static int Lives = 3;
    public static int Score;
    public GameObject[] livesIcons;

    //Electricity Overload Bar
    public Slider slider;

    //Upon taking Damage, make Mascot appear and provide tooltip
    private Vector2 originPosition;
    private Vector2 endPosition;
    public  GameObject Mascot;
    private RectTransform rect;
    public  float moveSpeed = 500f;

    //SFX
    public GameObject mainCamera;
    public GameObject Footsteps;
    public static AudioSource footsteps;

    public GameObject TaskSuccessAudio;
    public static AudioSource taskSuccessAudio;

    public static AudioSource mascotChatter;
    public GameObject MascotChatter;
    private bool spoken;

    // Start is called before the first frame update
    void Start()
    {
        Lives = 3; EventManager.Score = 0;
        originPosition = new Vector2(-1428, -253);
        endPosition = new Vector2(-682, -253);
        rect = Mascot.GetComponent<RectTransform>();
        rect.anchoredPosition = originPosition;

        leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        rightButton = GameObject.Find("RightButton").GetComponent<Button>();

        //leftButton.currentRoom = "Kitchen";
        //rightButton.currentRoom = "Kitchen";

        Score = 0; spoken = false;
        if(damageText != null ) { damageText.enabled = false; }
        TotalGameTime = 0;
        menuOpened = false;
        if(mainCamera != null )
        {
            footsteps = Footsteps.GetComponent<AudioSource>();
            taskSuccessAudio = TaskSuccessAudio.GetComponent<AudioSource>();
            mascotChatter = MascotChatter.GetComponent<AudioSource>();

            footsteps.volume = 0f; taskSuccessAudio.volume = 0f; mascotChatter.volume = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountTasks();
        taskCounterText.text = $"{taskCounter}";
        updateTimer += Time.deltaTime; TotalGameTime += Time.deltaTime;
        if(updateTimer > 1.5f)
        {
            //CountTasks();
            if(taskCounter == 0 && percentage > 0f) //If there are no tasks currently, decrease the overload bar
            {
                if(percentage != 0 && percentage > 2f) { percentage -= 2; }
                slider.value = percentage;
            }
            else if (taskCounter > 0 && taskCounter >= numberOfTasksUntilOverload && IncreaseOverload)
            {
                percentage += (taskCounter - 1);
                slider.value = percentage;
                //Feedback from Peer Tutor: Maybe pause Overload percentage when currently doing task?
            }
            electricityText.text = $"{percentage}%";
            scoreText.text = $"{Score}";
            updateTimer = 0f;
            //Debug.Log($"Task Counter: {taskCounter}");
        }
        if(percentage >= 100)
        {
            SceneManager.LoadScene(GameOverScene);
        }
        if(tookDamage)
        {
            if (rect.anchoredPosition.x < endPosition.x)
            {
                float newX = rect.anchoredPosition.x + moveSpeed * Time.deltaTime;
                rect.anchoredPosition = new Vector2(newX, rect.anchoredPosition.y);
            }
            else
            {
                if (spoken == false)
                {
                    PlayAudioSource("Mumbling");
                    spoken = true;
                }
                damageText.enabled = true;
                damageText.text = "Oh no! You lost a life. Be careful next time, when dealing with \nscary hazards! Maybe let the grown ups deal with them?";
                damageTimer += Time.deltaTime;
                IncreaseOverload = false; //Pause overload increase to let player read text
                if (damageTimer > 6f)
                {
                    damageText.enabled = false;
                    tookDamage = false;
                    IncreaseOverload = true; //Resume overload increase
                    rect.anchoredPosition = originPosition;
                }
            }
        }
    }   
    public void DecreaseOverload(int decrement) 
    { 
        percentage -= decrement;
        if(percentage < 0)
        {
            percentage = 0; //Ensure percentage never goes negative
        }
    }
    public void CountTasks()
    {
        GameObject[] taskObjects = GameObject.FindGameObjectsWithTag("Task");

        taskCounter = taskObjects.Length;
        //Debug.Log($"Number of tasks currently found: {taskCounter}");
        /*
        foreach (var taskObject in taskObjects)
        {
            Debug.Log($"Task object name: {taskObject.name}");
        }
        */
    }
    public void ChecksTasksForID(int ID)
    {
        GameObject[] taskObjects = GameObject.FindGameObjectsWithTag("Task");
        foreach (var taskObject in taskObjects)
        {
            TaskInfo taskInfo = taskObject.GetComponent<TaskInfo>();
            int number = taskObject.GetComponent<TaskInfo>().taskID;
            if (taskInfo != null && taskInfo.taskID == ID)
            {
                // Perform your action here, for example:
                // taskInfo.UpdateBoolArray();
                Destroy(taskObject);
            }
        }
    }
    public void UpdateBoolArrayGivenID(int ID)
    {
        if(ID != null)
        {
            AnamolySpawner.occupiedAnomalyLocations[ID] = false;
            AnamolySpawner.availableSpots.Add(ID);
        }
        else { Debug.Log("ID given was null."); }
    }
    public void AddScore(int score) { 
        EventManager.Score += score; 
        EventManager.menuOpened = false; 
        PlayAudioSource("TaskSuccess"); 
    }
    public void LoseLife()
    {
        Lives--;
        if(Lives == 2)
        {
            Destroy(livesIcons[2].gameObject);
        }
        else if(Lives == 1)
        {
            Destroy(livesIcons[1].gameObject);
        }
        else if(Lives == 0)
        {
            SceneManager.LoadScene(GameOverScene);
        }
        damageText.enabled = true;
        tookDamage = true;
    }
    public static void PlayAudioSource(string sfx)
    {
        if(sfx == "Footsteps")
        {
            footsteps.volume = 1f;
            footsteps.enabled = true;
            footsteps.Play();
        }
        else if(sfx == "TaskSuccess")
        {
            taskSuccessAudio.volume = 1f;
            taskSuccessAudio.enabled = true;
            taskSuccessAudio.Play();
        }
        else if(sfx == "Mumbling")
        {
            mascotChatter.volume = 1f;
            mascotChatter.enabled = true;
            mascotChatter.Play();
        }
        else { Debug.Log($"Could not recognize the desired input '{sfx}'."); }
    }

}
// Male Audio clip: https://freesound.org/people/pfranzen/sounds/264770/
// Fire GIF: https://tenor.com/view/fire-gif-23339431
// Water tap Image: https://www.vecteezy.com/vector-art/13330055-water-tap-faucet-icon-vector-design-template
// Bathtub PNG: https://www.cleanpng.com/png-kitchen-sink-tap-bathroom-bathtub-top-view-5273859/download-png.html
// Hair Dryer PNG: https://pixabay.com/vectors/dryer-hair-hairdresser-1293118/
