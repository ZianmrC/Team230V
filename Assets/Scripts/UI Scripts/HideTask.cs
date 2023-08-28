using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideTask : MonoBehaviour
{
    public GameObject task;

    private Button button;
    private CanvasRenderer canvasRenderer;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        canvasRenderer = GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MakeUI_Invisible();
    }
    public void TaskToHide()
    {
        Destroy(task);
        Debug.Log("test");
    }
    public void MakeUI_Invisible()
    {
        canvasRenderer.SetAlpha(0);
    }
    public void MakeUI_Visible()
    {
        canvasRenderer.SetAlpha(1);
    }
}
