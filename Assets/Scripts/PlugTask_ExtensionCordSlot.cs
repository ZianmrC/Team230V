using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlugTask_ExtensionCordSlot : MonoBehaviour, IDropHandler
{
    public string color = "Blue";
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            PlugInfo plugInfo = eventData.pointerDrag.GetComponent<PlugInfo>();
            DragAndDropUI dragdrop = eventData.pointerDrag.GetComponent<DragAndDropUI>();
            if (!string.Equals(plugInfo.color, color, StringComparison.OrdinalIgnoreCase))
            {
                dragdrop.InvalidPosition(eventData);
                Debug.Log($"Plug info color: {plugInfo}, color: {color}");
                Debug.Log("Incorrect!");
            }
            else
            {
                //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = dragdrop.lastPosition;
                Debug.Log($"Correct! Anchored Position: {GetComponent<RectTransform>().anchoredPosition.x}, {GetComponent<RectTransform>().anchoredPosition.y}");
                GeneralPlug.numberOfCorrectPlaces++;
                Destroy(dragdrop); Destroy(this.gameObject);
            }
        }
    }
}
