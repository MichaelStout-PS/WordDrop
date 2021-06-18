using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System;

/*
Cleverly authored by Michael "World's Greatest Genius" Stout
Copyright 2021
traineegenius.com
*/

public class SimpleMover : MonoBehaviour, ITweenManager
{
    [Tooltip("This is one of the two locations this object will be moved between")]
    public Vector3 fromLocation;
    [Tooltip("Leave this as zeroes to set automatically on Play")]
    public Vector3 targetLocation;
    [Tooltip("This is how long the tween will take")]
    [Range(0.01f, 10)] public float duration = 2;
    [Tooltip("This is the template for the type of motion, may include multiple easing functions, more can be easily added")]
    [SerializeField] public TweenTemplate tweenTemplate = TweenTemplate.Simple;

    private bool showing;

    public enum TweenTemplate
    {
        Simple,
        GravityBounceIn,
        ElasticIn,
        BounceIn,
        BounceInOut,
    }

    void Awake()
    {
        if (targetLocation == Vector3.zero)
        {
            targetLocation = transform.GetComponent<RectTransform>().position;
        }
        transform.position = fromLocation;

    }

    public Tween In(bool force = false)
    {
        //Stop Tween duplication if this tween is already running
        if (showing)
        {
            return null;
        }
        showing = true;

        switch (tweenTemplate)
        {
            case TweenTemplate.Simple:
                return transform.DOMove(targetLocation, duration).SetEase(Ease.InOutSine).Play();

            case TweenTemplate.GravityBounceIn:
                return DOTween.Sequence()
                .Insert(0, transform.DOMoveX(targetLocation.x, duration).SetEase(Ease.OutQuad))
                .Insert(0, transform.DOMoveY(targetLocation.y, duration).SetEase(Ease.OutBounce))
                .Append(transform.DOMove(targetLocation, 1f)).Play();

            case TweenTemplate.ElasticIn:
                return transform.DOMove(targetLocation, duration).SetEase(Ease.OutElastic).Play();

            case TweenTemplate.BounceIn:
                return transform.DOMove(targetLocation, duration).SetEase(Ease.OutBounce).Play();

            case TweenTemplate.BounceInOut:
                return transform.DOMove(targetLocation, duration).SetEase(Ease.OutBounce).Play();

            default:
                return transform.DOMove(targetLocation, duration).SetEase(Ease.InOutSine).Play();

        }
    }

    public Tween Out(bool force = false)
    {
        //Stop Tween duplication if this tween is already running
        if (!showing)
        {
            return null;
        }
        showing = false;

        switch (tweenTemplate)
        {
            case TweenTemplate.Simple:
                return transform.DOMove(fromLocation, duration).SetEase(Ease.InOutSine).Play();

            case TweenTemplate.GravityBounceIn:
                return DOTween.Sequence()
                .Insert(0, transform.DOMoveX(fromLocation.x, duration).SetEase(Ease.OutQuad))
                .Insert(0, transform.DOMoveY(fromLocation.y, duration).SetEase(Ease.InBounce))
                .Append(transform.DOMove(fromLocation, 1f)).Play();

            case TweenTemplate.ElasticIn:
                return transform.DOMove(fromLocation, duration).SetEase(Ease.InOutCubic).Play();

            case TweenTemplate.BounceIn:
                return transform.DOMove(fromLocation, duration).SetEase(Ease.InOutCubic).Play();

            case TweenTemplate.BounceInOut:
                return transform.DOMove(fromLocation, duration).SetEase(Ease.OutBounce).Play();

            default:
                return transform.DOMove(fromLocation, duration).SetEase(Ease.InOutSine).Play();
        }
    }

    
}

//[Serializable]
public interface ITweenManager {
    Tween In(bool force = false);
    Tween Out(bool force = false);
}


#if UNITY_EDITOR

[CustomEditor(typeof(SimpleMover))]
public class TweenIdeasEditor : Editor
{

    SimpleMover ti;

    void OnEnable()
    {
        ti = target as SimpleMover;
    }

    public void OnSceneGUI()
    {
        ti = target as SimpleMover;

        

        //Draggable handle for the from position
        EditorGUI.BeginChangeCheck();
        float size = HandleUtility.GetHandleSize(ti.fromLocation);

        Vector3 newFromPosition = ti.fromLocation;


        Handles.color = Handles.xAxisColor;

        //Draw rectangle to show fromPosition
        RectTransform rectTransform = ti.GetComponent<RectTransform>();
        Rect rect = new Rect(ti.fromLocation.x-(rectTransform.rect.width/2), ti.fromLocation.y-(rectTransform.rect.height/2), rectTransform.rect.width, rectTransform.rect.height);

        Handles.DrawSolidRectangleWithOutline(rect, Color.clear, Color.red);

        /* //Draw arrows to move for fromPosition
        Handles.color = Handles.xAxisColor;
        newFromPosition.x = (Handles.FreeMoveHandle(ti.fromLocation, Quaternion.identity, size*2, Vector3.one * 10, Handles.ArrowHandleCap)).x;
        Handles.color = Handles.yAxisColor;
        newFromPosition.y = (Handles.FreeMoveHandle(ti.fromLocation, Quaternion.LookRotation(Vector3.up), size, Vector3.one * 10, Handles.ArrowHandleCap)).y;
        */

        //Draw handle to drag around fromPosition
        newFromPosition = Handles.FreeMoveHandle(ti.fromLocation, Quaternion.identity, size / 5, Vector3.one * 50, Handles.RectangleHandleCap);


        Handles.Label(ti.fromLocation, "From Position");
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(ti, "Changed From Position");
            ti.fromLocation = newFromPosition;
        }
    }


    bool defaultInspectorValuesToggleGroup = false;
    public override void OnInspectorGUI()
    {
        //Default inspector fields (not custom) in toggleable group
        defaultInspectorValuesToggleGroup = EditorGUILayout.Toggle("Show Default Inspector Values", defaultInspectorValuesToggleGroup);
        if (defaultInspectorValuesToggleGroup)
        {
            base.OnInspectorGUI();
        }
        else
        {

            ti = target as SimpleMover;

            //fromlocation
            EditorGUILayout.BeginHorizontal();
            ti.fromLocation = EditorGUILayout.Vector2Field("From Location", ti.fromLocation, GUILayout.MinWidth(300));
            if (GUILayout.Button("Reset"))
            {
                ti.fromLocation = ti.transform.position;
            }
            EditorGUILayout.EndHorizontal();

            //targetlocation (set by transform.position in scene)
            //ti.targetLocation = EditorGUILayout.Vector2Field("target Location", ti.targetLocation);

            //duration
            ti.duration = EditorGUILayout.Slider("Duration", ti.duration, 0.01f, 10);


            //tween template
            ti.tweenTemplate = (SimpleMover.TweenTemplate)EditorGUILayout.EnumPopup("Tween Template", ti.tweenTemplate);

            //Display number of elements, add and remove buttons
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("In"))
            {
                ti.In();
            }
            if (GUILayout.Button("Out"))
            {
                ti.Out();
            }
            GUILayout.EndHorizontal();

        }
    }


}

#endif