using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class WaterTask : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPosition;

    private bool isDragging = false;
    public GameObject waterDrop;
     private int xRange = 200;
     private int yRange = 100;

    private GameObject[] water;

    private bool start = false;
    void Start()
    {
        Debug.Log("we are starting");
        for (int i = 0; i < 100; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0.0f);
            GameObject waterDropInstance = Instantiate(waterDrop, this.transform);
            waterDropInstance.transform.SetParent(canvas.transform, false);
            RectTransform rectTransform = waterDropInstance.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = spawnPosition;
        }
    }
     private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision detected with other object!");
            // Check if the GameObject collided with another object
            if (collision.gameObject.CompareTag("water")&&start)
            {
                // Destroy the collided GameObject
                Destroy(collision.gameObject);
            }
        }
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rectTransform.localPosition;
        Debug.Log(originalPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
        isDragging = true;
        canvasGroup.blocksRaycasts = false;
        start = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                rectTransform.localPosition = offsetToOriginal;
                
                    
            }
        }
    }

       


    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true; 
        start = false;
    }
    
}
