using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentTask : MonoBehaviour
{
    private Parent parent; //Parent 
    public ParentMediator mediator;
    public float overlapThreshold = 0.1f; // Adjust this to control the overlap sensitivity

    private RectTransform parentRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        parent = mediator.getParent();
        Debug.Log("I have got the Parent");
        parentRectTransform = parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {        
        // Convert Image position to world space
        Vector3 imageWorldPosition = RectTransformUtility.WorldToScreenPoint(null, parentRectTransform.position);

        // Convert GameObject position to screen space
        Vector3 gameObjectScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        // Check if the Image and GameObject positions are close enough to be considered overlapping
        float distance = Vector3.Distance(imageWorldPosition, gameObjectScreenPosition);
        if (distance <= overlapThreshold){
            Debug.Log("We are overlapping");
            Destroy(gameObject);
        }
    }
 

    bool CheckOverlapUsingColliders(Collider collider1, Collider collider2)
    {
        if (collider1 != null && collider2 != null)
        {
            return collider1.bounds.Intersects(collider2.bounds);
        }
        return false;
    }

}
