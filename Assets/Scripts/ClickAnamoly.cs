using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickAnamoly : MonoBehaviour
{

    private GeneralSwitches GeneralSwitches;
    private TaskInfo taskInfo;
    private Canvas canvas;
    private RectTransform rectTransform;
    public GameObject task;

    private int taskNumber;
    EventManager eventManager;
    private void Start()
    {
        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
        taskInfo = GetComponent<TaskInfo>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventManager.menuOpened)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            taskNumber = GetComponent<TaskInfo>().taskID;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if(task != null)
                    {
                        if(task.GetComponent<GeneralSwitches>() != null)
                        {
                            task.GetComponent<GeneralSwitches>().taskID = taskNumber; //Pass value of Task ID to the Task's Script
                        }
                        else if(task.GetComponent<GeneralPlug>() != null)
                        {
                            task.GetComponent<GeneralPlug>().taskID = taskNumber; //Pass value of Task ID to the Task's Script
                        }
                        else if(task.GetComponent<BrokenToasterTask>() != null)
                        {
                            task.GetComponent<BrokenToasterTask>().taskID = taskNumber; //Pass value of Task ID to the Task's Script
                        }
                        else if(task.GetComponent<WireGenerator>() != null)
                        {
                            task.GetComponent<WireGenerator>().taskID = taskNumber;
                        }
                        else if(task.GetComponent<generalWaterTask>() != null)
                        {
                            task.GetComponent<generalWaterTask>().taskID = taskNumber;
                        }
                        else if(task.GetComponent<SinkTask>() != null)
                        {
                            task.GetComponent<SinkTask>().taskID = taskNumber;
                        }
                        taskNumber = GetComponent<TaskInfo>().taskID;
                        GameObject instantiatedObject = Instantiate(task);
                        instantiatedObject.transform.SetParent(canvas.transform, false);
                        RectTransform instantiatedRT = instantiatedObject.GetComponent<RectTransform>();
                        instantiatedRT.anchoredPosition = Vector2.zero;
                        EventManager.menuOpened = true;
                        instantiatedRT.SetAsFirstSibling();
                    }
                    else { throw new System.Exception(); }

                    //Destroy(gameObject); //Destroy object for now, in further development, this will spawn task
                    //gameObject.SetActive(false)
                    //taskInfo.UpdateBoolArray();
                }
            }
        }
    }
}
