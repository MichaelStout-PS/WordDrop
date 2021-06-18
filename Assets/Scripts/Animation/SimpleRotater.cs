using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/*
Cleverly authored by Michael "World's Greatest Genius" Stout
Copyright 2021
traineegenius.com
*/


public class SimpleRotater : MonoBehaviour, ITweenManager
{
    [Tooltip("This is one of the two scales this object will be moved between")]
    public Vector3 fromRotation = Vector3.zero;

    public bool spinForever = true;
    private Tween foreverTween;

    public Vector3 toRotation = new Vector3(0, 0, 180);

    [Tooltip("This is how long the tween will take")]
    [Range(0.01f, 10)] public float duration = 5;
    [Tooltip("This is the template for the type of motion, may include multiple easing functions, more can be easily added")]
    [SerializeField] public TweenTemplate tweenTemplate = TweenTemplate.Simple;

    public enum TweenTemplate
    {
        Simple,
        Elastic,
    }

    private bool showing;

    void Awake()
    {
        if (toRotation == Vector3.zero)
        {
            toRotation = transform.eulerAngles;
        }
        transform.eulerAngles = fromRotation;

    }

    public Tween In(bool force = false)
    {
        //Stop Tween duplication if this tween is already running
        if (showing)
        {
            return null;
        }
        showing = true;

        if (spinForever)
        {
            foreverTween = transform.DORotate(toRotation, duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).Play();
            //Don't return the infinite tween, because it will make the other scripts think this script is not ready (when it's supposed to be going forever)
            return null;
        }

        switch (tweenTemplate)
        {
            case TweenTemplate.Simple:
                return transform.DORotate(toRotation, duration).SetEase(Ease.InOutSine).Play();

            case TweenTemplate.Elastic:
                return transform.DORotate(toRotation, duration).SetEase(Ease.OutElastic).Play();

            default:
                return transform.DORotate(toRotation, duration).SetEase(Ease.InOutSine).Play();

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

        if (spinForever)
        {
            foreverTween.Kill();
            return transform.DORotate(fromRotation, duration/2).SetEase(Ease.Linear).Play();
        }
        
        switch (tweenTemplate)
        {
            case TweenTemplate.Simple:
                return transform.DORotate(fromRotation, duration).SetEase(Ease.InOutSine).Play();

            case TweenTemplate.Elastic:
                return transform.DORotate(fromRotation, duration).SetEase(Ease.OutElastic).Play();

            default:
                return transform.DORotate(fromRotation, duration).SetEase(Ease.InOutSine).Play();
        }
    }

}


#if UNITY_EDITOR

[CustomEditor(typeof(SimpleRotater))]
public class SimpleRotaterEditor : Editor
{

    SimpleRotater sr;

    void OnEnable()
    {
        sr = target as SimpleRotater;
    }

    public void OnSceneGUI()
    {
        sr = target as SimpleRotater;


    }


    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        
        //Display number of elements, add and remove buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("In"))
        {
            sr.In();
        }
        if (GUILayout.Button("Out"))
        {
            sr.Out();
        }
        GUILayout.EndHorizontal();

    }


}


#endif