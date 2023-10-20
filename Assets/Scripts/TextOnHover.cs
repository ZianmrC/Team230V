using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextOnHover : MonoBehaviour
{

    public GameObject HoverText;
    public GameObject BGpanel;

    // Start is called before the first frame update
    void Start()
    {
        HoverText.SetActive(false);
        BGpanel.SetActive(false);
    }

    public void OnMouseOver()
    {
        if (!IsMouseOnUI())
        {
            HoverText.SetActive(true);
            BGpanel.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        HoverText.SetActive(false);
        BGpanel.SetActive(false);
    }

    private bool IsMouseOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
