using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class BrokenToasterTask : MonoBehaviour, IDropHandler
{
    public int taskID;
    public int score = 5;
    public RectTransform rectTransform;
    public Vector2 spawnPosition;
    EventManager eventManager;
    
    private bool canRemoveTask;
    private float timer;
    //Parent Animation
    public GameObject parentAnimation;
    public Vector2 originPosition;
    public Vector2 endPosition;
    private RectTransform rect;
    public float moveSpeed = 950f;

    void Start()
    {
        timer = 0f; canRemoveTask = false;
        rectTransform = GetComponent<RectTransform>();
        spawnPosition = rectTransform.anchoredPosition;

        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
        rect = parentAnimation.GetComponent<RectTransform>();
        rect.anchoredPosition = originPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            string objectName = eventData.pointerDrag.name;

            if (objectName == "Parent")
            {
                Debug.Log("Parent Detected!");
                canRemoveTask = true;
                rect.anchoredPosition = endPosition;
            }
        }
    }
    void Update()
    {
        if (canRemoveTask)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if(timer > 1.5f)
            {
                eventManager.ChecksTasksForID(taskID);
                eventManager.UpdateBoolArrayGivenID(taskID);
                eventManager.AddScore(score);
                Destroy(gameObject);
            }
        }
        
        
    }


}
