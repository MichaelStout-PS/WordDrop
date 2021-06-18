using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UIAnimation;

[DisallowMultipleComponent]
public class TouchField : Image, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
{




    [Tooltip("The object that will receive drag data after the completion of a successful drag")]
    public GameObject Target;

    private bool Selected = false;


    public float dragIncrementMultiplier = 1f;
    public float dragDuration = 0;
    public float maximumDragDuration = 5f;



    //animator
    public Touch_Animation Animation_Controller;


    //swipe values
    private Vector2 _dragOrigin;
    private Vector2 _dragRelease;
    [SerializeField] SwipeData Swipe_Data;

    [Tooltip("if enabled, the input will complete when the player drags outside of the Touch Field")]
    public bool endOnTouchExit = true;


    public void OnPointerDown(PointerEventData eventData)
    {
        Selected = true;
        _dragOrigin = eventData.pressPosition;
        if (Animation_Controller != null)
        {
            Animation_Controller.StartAnimation(dragIncrementMultiplier, maximumDragDuration);
        }

    }



    public void OnPointerUp(PointerEventData eventData)
    {
        Selected = false;
        _dragRelease = eventData.position;

        if (Animation_Controller != null)
        {
            Animation_Controller.EndAnimation();
        }


        SwipeProcessing();
        dragDuration = 0;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragDuration > 0f && endOnTouchExit == true)
        {
            OnPointerUp(eventData);
        }
    }



    /// <summary>
    /// Processes swipe input 
    /// </summary>
    void SwipeProcessing()
    {


        SwipeData swipeData = new SwipeData();

        // set direction data
        swipeData.directionRaw = (_dragOrigin - _dragRelease).normalized * -1;

        float factor = 100f;
        var roundedX = (Mathf.Round(swipeData.directionRaw.x * factor)) / factor;
        var roundedY = (Mathf.Round(swipeData.directionRaw.y * factor)) / factor;

        swipeData.direction = new Vector2(roundedX, roundedY);

        float heldPercentage = Mathf.InverseLerp(0, maximumDragDuration, dragDuration);

        swipeData.heldPercentage = heldPercentage;

        if (Target != null)
        {
            Target.SendMessage("OnSwipe", swipeData);
        }

        Swipe_Data = swipeData;
    }


    public void OnDrag(PointerEventData eventData)
    {

        if (Selected == true)
        {
            dragDuration += Time.deltaTime * dragIncrementMultiplier;

            if (Animation_Controller != null)
            {
                Animation_Controller.SendMessage("AnimationReciever", dragDuration, SendMessageOptions.DontRequireReceiver);
            }

            if (Target != null)
            {
                Target.SendMessage("OnFieldDrag", new DragData(_dragOrigin, eventData.pressPosition), SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    /*
    private void ColorAnimation()
    {
        // increase _gradientColor or reset to 0 if loop enabled
        _gradientPosition += dragIncrementMultiplier * Time.deltaTime;
        print(_gradientPosition);
        if (_gradientPosition > maximumDragDuration && loopGradient == true) {
            Debug.Log("Gradient reset is running");
            _gradientPosition = 0f;
            }


        float gradientKey = Mathf.InverseLerp(0f, maximumDragDuration, _gradientPosition);
        this.color = colorOverDuration.Evaluate(gradientKey);

    }
    */

}



public struct DragData
{
    public Vector2 StartPosition;
    public Vector2 CurrentPosition;

    public DragData(Vector2 startPosition, Vector2 currentPosition)
    {
        StartPosition = startPosition;
        CurrentPosition = currentPosition;
    }

}


[Serializable]
public struct SwipeData
{
    public Vector2 direction;
    public Vector2 directionRaw;
    public float swipeTime;
    public float heldPercentage;
}



public interface ITouchFieldReciever<SwipeData>
{

    void OnSwipe(SwipeData swipeData);


    /// <summary>
    /// Receives the start and current position of the drag event.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    void OnFieldDrag(DragData dragData);

}