using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WireTaskDragPlug : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    public Canvas canvas;
    public int currentWireNumber;
    public bool isDragging;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        currentWireNumber = 0;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        isDragging = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
    public void InvalidPosition(PointerEventData eventData)
    {

    }
}
