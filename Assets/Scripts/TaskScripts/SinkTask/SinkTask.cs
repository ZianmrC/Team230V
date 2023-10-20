using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SinkTask : MonoBehaviour
{
    public TaskVariables taskVariables;
    public int taskID;
    public GameObject tagDraggable;
    private TagDraggable tagScript;
    private float currentTimer;

    [Header("Tooltip/Mascot Variables")]
    public GameObject sinkMascot;
    private GameObject textObject;
    private TextMeshProUGUI text;
    private Vector2 originPosition;
    private Vector2 endPosition;
    private RectTransform rect;
    public float moveSpeed = 500f;
    private bool stopTooltip;
    private bool spoken = false;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = new Vector2(-250, -696); spoken = false;
        endPosition = new Vector2(-250, -374);
        currentTimer = 0f; stopTooltip = false;

        //Pass TaskID to TagDraggable
        tagScript = tagDraggable.GetComponent<TagDraggable>();
        tagScript.taskID = taskID;

        rect = sinkMascot.GetComponent<RectTransform>();
        rect.anchoredPosition = originPosition;
        Transform textObject = transform.Find("Mascot/Container/Image/Text (TMP)");
        text = textObject.GetComponent<TextMeshProUGUI>();
        rect.anchoredPosition = originPosition;
    }
    private void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer > taskVariables.sinkHelpTime && !stopTooltip)
        {
            if (rect.anchoredPosition.y < endPosition.y) //Move up
            {
                float newY = rect.anchoredPosition.y + moveSpeed * Time.deltaTime;
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);
            }
            else if (!stopTooltip)
            {
                text.enabled = true;
                if (spoken == false)
                {
                    EventManager.PlayAudioSource("Mumbling");
                    spoken = true;
                }
                text.text = "Click and drag the red handle in any direction. \nWe should turn this off before the \nsink is flooded with water, " +
                    "this could become very dangerous ";
                stopTooltip = true;
            }
        }
    }


}
