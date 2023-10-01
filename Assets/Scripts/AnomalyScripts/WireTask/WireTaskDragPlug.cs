using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WireTaskDragPlug : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    public Canvas canvas;
    public int currentWireNumber;
    public int plugNumber;
    public bool isDragging;
    private CircleCollider2D circleCollider;
    private WireGenerator wireGenerator;
    [HideInInspector] public GameObject wireGeneratorPrefab;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        currentWireNumber = 0;
        wireGenerator = wireGeneratorPrefab.GetComponent<WireGenerator>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        isDragging = true;

        if (circleCollider != null)
        {
            Vector2 colliderOffset = circleCollider.offset;
            colliderOffset.x = rectTransform.anchoredPosition.x;
            colliderOffset.y = rectTransform.anchoredPosition.y;
            circleCollider.offset = colliderOffset;
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        WireInfo wireInfo = collision.gameObject.GetComponent<WireInfo>();
        Transform objectTransform = this.transform;
        if (wireInfo != null && wireInfo.wireNumber == currentWireNumber + 1 && plugNumber == wireInfo.plugNumber)
        {
            Quaternion Up = Quaternion.Euler(0, 0, 0);
            Quaternion Left = Quaternion.Euler(0, 0, 90);
            Quaternion Right = Quaternion.Euler(0, 0, -90);
            Quaternion Down = Quaternion.Euler(0, 0, 180);
            Quaternion Down2 = Quaternion.Euler(0, 0, -180);
            if(collision.gameObject.name.Contains("bottom_left_curve") && objectTransform.rotation == Up) { objectTransform.rotation = Left;}
            else if (collision.gameObject.name.Contains("bottom_left_curve") && objectTransform.rotation == Right) { objectTransform.rotation = Down;}

            if(collision.gameObject.name.Contains("bottom_right_curve") && objectTransform.rotation == Up) { objectTransform.rotation = Right;}
            else if (collision.gameObject.name.Contains("bottom_right_curve") && objectTransform.rotation == Left) { objectTransform.rotation = Down;}

            if(collision.gameObject.name.Contains("top_left_curve") && objectTransform.rotation == Right) { objectTransform.rotation = Up; }
            else if (collision.gameObject.name.Contains("top_left_curve") && (objectTransform.rotation == Down || objectTransform.rotation == Down2)) { objectTransform.rotation = Left; }

            if (collision.gameObject.name.Contains("top_right_curve") && objectTransform.rotation == Left) { objectTransform.rotation = Up; }
            else if (collision.gameObject.name.Contains("top_right_curve") && objectTransform.rotation == Down || objectTransform.rotation == Down2) { objectTransform.rotation = Right; }

            Destroy(collision.gameObject);
            currentWireNumber = wireInfo.wireNumber;
            wireGenerator.wireCounter++;
            Debug.Log(wireGenerator.wireCounter);
        }
    }
}
