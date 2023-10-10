using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    AudioSource click;

    public void Start()
    {
        click = GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        click.Play();
    }
}
