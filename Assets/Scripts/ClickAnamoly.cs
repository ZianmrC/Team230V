using UnityEngine;
using UnityEngine.UI;

public class ClickAnamoly : MonoBehaviour
{

    private GameObject EventManager;
    ElectricityOverload electricityOverload;
    private void Start()
    {
        EventManager = GameObject.Find("EventSystem");
        electricityOverload = EventManager.GetComponent<ElectricityOverload>();
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
                    Destroy(gameObject); //Destroy object for now, in further development, this will spawn task
                    //gameObject.SetActive(false)
                    electricityOverload.CountTasks();
                }
            }
        }
    }
}
