using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class BrokenToasterTask : MonoBehaviour, IDropHandler
{
    public int taskID;
    public int score = 5;
    public RectTransform rectTransform;
    public Vector2 spawnPosition;
    EventManager eventManager;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        spawnPosition = rectTransform.anchoredPosition;

        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            string objectName = eventData.pointerDrag.name;

            if (objectName == "Parent")
            {
                Debug.Log("feragrwu");
                eventManager.ChecksTasksForID(taskID);
                eventManager.UpdateBoolArrayGivenID(taskID);
                eventManager.AddScore(score);
                Destroy(gameObject);
            }
        }
    }


}
