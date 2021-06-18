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


public class SimpleScaler : MonoBehaviour, ITweenManager
{
    [Tooltip("This is one of the two scales this object will be moved between")]
    public Vector3 fromScale = Vector3.zero;
    [Tooltip("Leave this as zeroes to set automatically on Play")]
    public Vector3 toScale = Vector3.zero;

    [Tooltip("This is how long the tween will take")]
    [Range(0.01f, 10)] public float duration = 2;
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
        if (toScale == Vector3.zero)
        {
            toScale = transform.localScale;
        }
        transform.localScale = fromScale;

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
                return transform.DOScale(toScale, duration).SetEase(Ease.InOutSine).Play();

            case TweenTemplate.Elastic:
                return transform.DOScale(toScale, duration).SetEase(Ease.OutElastic).Play();

            default:
                return transform.DOScale(toScale, duration).SetEase(Ease.InOutSine).Play();

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
                return transform.DOScale(fromScale, duration).SetEase(Ease.InOutSine).Play();

            case TweenTemplate.Elastic:
                return transform.DOScale(fromScale, duration).SetEase(Ease.InOutCubic).Play();

            default:
                return transform.DOScale(fromScale, duration).SetEase(Ease.InOutSine).Play();
        }
    }

}


#if UNITY_EDITOR

[CustomEditor(typeof(SimpleScaler))]
public class SimpleScalerEditor : Editor
{

    SimpleScaler ss;

    void OnEnable()
    {
        ss = target as SimpleScaler;
    }

    public void OnSceneGUI()
    {
        ss = target as SimpleScaler;


        
    }


    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        

        
        //Display number of elements, add and remove buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("In"))
        {
            ss.In();
        }
        if (GUILayout.Button("Out"))
        {
            ss.Out();
        }
        GUILayout.EndHorizontal();

    }


}

#endif