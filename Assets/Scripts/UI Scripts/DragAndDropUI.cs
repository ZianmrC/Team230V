using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    [HideInInspector] public Vector2 lastPosition;
    [HideInInspector] public Vector2 spawnPosition;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        spawnPosition = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("test");
        //lastPosition = rectTransform.position;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition = lastPosition;
    }
    public void InvalidPosition(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition = spawnPosition;
    }
}