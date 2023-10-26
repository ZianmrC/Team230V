using UnityEngine;
using UnityEngine.EventSystems;

public class Parent : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public ParentMediator mediator;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPosition;

    private bool isDragging = false;

    void Start()
    {
        mediator.setParent(this);
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
        //Debug.Log(originalPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
        isDragging = true;
        canvasGroup.blocksRaycasts = false;

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
        
        rectTransform.localPosition = originalPosition;
        
    }
    
}
