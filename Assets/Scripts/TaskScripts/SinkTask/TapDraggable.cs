using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapDraggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originPosition;
    private bool isDragging;
    private bool isColliding;

    public float dragTimer;
    private float currentTimer;
    private GameObject parentObject;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originPosition = new Vector2(38, 154);
        dragTimer = 2.0f;

        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
        parentObject = parentRectTransform.gameObject;
    }

    void Update()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if(isColliding && isDragging)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer > dragTimer)
            {
                Destroy(parentObject);
            }
        }
        else if(!isColliding || !isDragging)
        {
            //Debug.Log("Failed");
            currentTimer = 0f;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
        this.rectTransform.anchoredPosition = originPosition;
    }
    public void InvalidPosition(PointerEventData eventData)
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TapCollider")
        {
            isColliding = true;
        }
    }
}
