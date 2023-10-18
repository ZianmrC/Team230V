using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TagDraggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    //Referencing the ArrowCycler Object
    public GameObject arrowCycleObject;
    private Animator animator;

    public Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originPosition;
    private bool isDragging;
    private bool isColliding;

    public float dragTimer;
    private float currentTimer;
    private GameObject parentObject;
    public TaskVariables taskVariables;

    //EventSystem
    EventManager eventManager;

    public int taskID;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originPosition = new Vector2(38, 106);

        eventManager = GameObject.Find("EventSystem").GetComponent<EventManager>();
        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
        parentObject = parentRectTransform.gameObject;

        animator = arrowCycleObject.GetComponent<Animator>();
        arrowCycleObject.SetActive(false);
    }

    void Update()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if(isColliding && isDragging)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer > dragTimer)
            {
                eventManager.ChecksTasksForID(taskID);
                eventManager.UpdateBoolArrayGivenID(taskID);
                eventManager.AddScore(taskVariables.sinkScore);
                eventManager.DecreaseOverload(1);
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
        arrowCycleObject.SetActive(true);
        isDragging = true;
        animator.SetBool("isCycling", true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        animator.SetBool("isCycling", false);
        arrowCycleObject.SetActive(false);
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
