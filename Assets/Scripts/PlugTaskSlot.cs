using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlugTaskSlot : MonoBehaviour, IDropHandler
{
    public string color = "Blue";
    private RectTransform rectTransform;
    private Vector2 spawnPosition;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        spawnPosition = rectTransform.anchoredPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PlugInfo plugInfo = eventData.pointerDrag.GetComponent<PlugInfo>();
            DragAndDropUI dragdrop = eventData.pointerDrag.GetComponent<DragAndDropUI>();
            if (!string.Equals(plugInfo.color, color, StringComparison.OrdinalIgnoreCase))
            {
                dragdrop.InvalidPosition(eventData);
                //Debug.Log("Incorrect!");
            }
            else
            {
                //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = spawnPosition; //Set plug to plug socket's position
                //Debug.Log($"Correct! Anchored Position: {GetComponent<RectTransform>().anchoredPosition.x}, {GetComponent<RectTransform>().anchoredPosition.y}");
                GeneralPlug.numberOfCorrectPlaces++;
                Destroy(dragdrop); 
                Destroy(this.gameObject);
            }
        }
    }
}
