using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickAnamoly : MonoBehaviour
{

    private GameObject EventManager;
    private GeneralSwitches GeneralSwitches;
    private TaskInfo taskInfo;
    private Canvas canvas;
    private RectTransform rectTransform;
    public GameObject task;

    private int taskNumber;
    EventManager eventManager;
    private void Start()
    {
        EventManager = GameObject.Find("EventSystem");
        eventManager = EventManager.GetComponent<EventManager>();
        taskInfo = GetComponent<TaskInfo>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (task != null)
                    {
                        if (task.GetComponent<GeneralSwitches>() != null)
                        {
                            taskNumber = GetComponent<TaskInfo>().taskID;
                            task.GetComponent<GeneralSwitches>().taskID = taskNumber; // Pass value of Task ID to the Task's Script
                        }
                        else if (task.GetComponent<GeneralPlug>() != null)
                        {
                            taskNumber = GetComponent<TaskInfo>().taskID;
                            task.GetComponent<GeneralPlug>().taskID = taskNumber; // Pass value of Task ID to the Task's Script
                        }

                        GameObject instantiatedObject = Instantiate(task);
                        instantiatedObject.transform.SetParent(canvas.transform, false);
                        RectTransform instantiatedRT = instantiatedObject.GetComponent<RectTransform>();
                        instantiatedRT.anchoredPosition = Vector2.zero;
                    }
                    // Continue execution without instantiating if task is null
                }
            }
        }
    }

}
//Destroy(gameObject); //Destroy object for now, in further development, this will spawn task
//gameObject.SetActive(false)
//taskInfo.UpdateBoolArray();
