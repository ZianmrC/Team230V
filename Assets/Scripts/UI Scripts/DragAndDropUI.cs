using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    [HideInInspector] public Vector2 lastPosition;
    [HideInInspector] public Vector2 spawnPosition;

    [Header("Dragged Plug Images")]
    Sprite sprite;
    public Image originalImage;
    GameObject draggedPlug;
    RectTransform childRect;
    GameObject childObject;
    Image childImage;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Transform child = transform.Find("DraggedPlug");
        if (child != null)
        {
            childRect = child.GetComponent<RectTransform>();
            childObject = child.gameObject;
            childImage = childObject.GetComponent<Image>();
        }
    }


    void Start()
    {
        originalImage = GetComponent<Image>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        spawnPosition = rectTransform.anchoredPosition;
        if (childObject != null)
        {
            originalImage.enabled = true;
            childImage.enabled = false;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if(childObject!=null)
        {
            originalImage.enabled = false;
            childImage.enabled = true;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("test");
        //lastPosition = rectTransform.position;
        if (childObject != null)
        {
            originalImage.enabled = true;
            childImage.enabled = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition = lastPosition;
        if (childObject != null)
        {
            originalImage.enabled = true;
            childImage.enabled = false;
        }
    }
    public void InvalidPosition(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition = spawnPosition;
    }
    public void RevertImage()
    {
        if (childObject != null)
        {
            originalImage.enabled = true;
            childImage.enabled = false;
        }
    }
}