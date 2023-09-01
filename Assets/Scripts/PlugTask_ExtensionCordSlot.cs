using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlugTask_ExtensionCordSlot : MonoBehaviour, IDropHandler
{
    public string color = "Blue";
    public bool correctlyPlaced = false;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            PlugInfo plugInfo = eventData.pointerDrag.GetComponent<PlugInfo>();
            DragAndDropUI dragdrop = eventData.pointerDrag.GetComponent<DragAndDropUI>();
            if (!string.Equals(plugInfo.color, color, StringComparison.OrdinalIgnoreCase))
            {
                dragdrop.InvalidPosition(eventData);
                Debug.Log("Incorrect!");
                correctlyPlaced = false;
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                Debug.Log("correct");
                correctlyPlaced = false;
            }
        }
    }
}
