using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        HoverText.SetActive(true);
        BGpanel.SetActive(true);
    }

    public void OnMouseExit()
    {
        HoverText.SetActive(false);
        BGpanel.SetActive(false);
    }
}
