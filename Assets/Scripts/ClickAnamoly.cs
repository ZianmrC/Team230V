using UnityEngine;

public class ClickAnamoly : MonoBehaviour
{
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
                }
            }
        }
    }
}
