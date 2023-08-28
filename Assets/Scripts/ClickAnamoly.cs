using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickAnamoly : MonoBehaviour
{

    private GameObject EventManager;
    private TaskInfo taskInfo;
    private Canvas canvas;
    private RectTransform rectTransform;
    public GameObject task;
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
                    if(task == null)
                    {
                        throw new System.Exception();
                    }
                    GameObject instantiatedObject = Instantiate(task);
                    instantiatedObject.transform.SetParent(canvas.transform, false);
                    RectTransform instantiatedRT= instantiatedObject.GetComponent<RectTransform>();
                    instantiatedRT.anchoredPosition = Vector2.zero;

                    Destroy(gameObject); //Destroy object for now, in further development, this will spawn task
                    //gameObject.SetActive(false)
                    taskInfo.UpdateBoolArray();
                }
            }
        }
    }
}
