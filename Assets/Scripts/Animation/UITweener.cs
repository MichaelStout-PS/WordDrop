using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using static SimpleMover;
using UnityEngine.UI;

//This script should animate an object between two other "from" and "to" objects. It should be part of a prefab with the from and to objects included. It sets the "real" object's state to be like the from object at first, then animates the real object between the from and two objects on request. This is so that the from and two anchors can be anchored to a canvas and managed by Unity as the canvas size changes e.g. window is resized or phone orientation is rotated.
//-Michael Stout

public class UITweener : MonoBehaviour, ITweenManager
{
    [Header("From and To Objects")]
    //[System.NonSerialized]
    public Transform from;
    //[System.NonSerialized]
    public Transform to;

    public bool overrideStartPosition = false;

    [Header("Tween Parameters")]
    [Tooltip("This is how long the tween will take")]
    [Range(0.01f, 10)] public float duration = 2;

    //The easing function the animation(s) will use
    public Ease ease;

    [Header("Custom Eases")]
    public Ease xPosition;
    public Ease yPosition;
    public Ease rotation;
    public Ease size;
    public Ease colour;

    public bool disableColour = false;

    private bool disableRotation = false;

    private bool showing;

    void Awake()
    {
        //Set from and to if they are not set already and can be found as children
        from = from ?? transform.Find("from");
        to = to ?? transform.Find("to");

        //Set eases to defaults
        xPosition = xPosition != Ease.Unset ? xPosition : ease;
        yPosition = yPosition != Ease.Unset ? yPosition : ease;
        rotation = rotation != Ease.Unset ? rotation : ease;
        size = size != Ease.Unset ? size : ease;
        colour = colour != Ease.Unset ? colour : ease;
    }


    void Start()
    {
        if (from && to)
        {
            ReadyToStart();
        }
    }
    //Start, upon playing the scene
    public void ReadyToStart()
    {
        //Set from and to object's parents to the canvas of this object, anchors are relative to the direct parent
        //from.SetParent(transform.parent);
        //to.SetParent(transform.parent);

        //Hide the from and to objects
        if (disableColour)
        {
            from.GetComponent<UnityEngine.UI.MaskableGraphic>().color = Color.clear;
            to.GetComponent<UnityEngine.UI.MaskableGraphic>().color = Color.clear;
        }
        else
        {
            from.GetComponent<UnityEngine.UI.MaskableGraphic>().enabled = false;
            to.GetComponent<UnityEngine.UI.MaskableGraphic>().enabled = false;
        }

        //Tell the from and to objects who their real object is, for their own scripts (which don't do much)
        from.GetComponentInChildren<fromToScript>().targetObject = gameObject;
        to.GetComponentInChildren<fromToScript>().targetObject = gameObject;

        if (!overrideStartPosition)
        {
            //Set this object's state to the from position's state which should be the starting state
            transform.position = from.position;
            transform.rotation = from.rotation;
            transform.localScale = from.transform.localScale;
            transform.GetComponent<RectTransform>().sizeDelta = from.GetComponent<RectTransform>().sizeDelta;
            if (!disableColour)
            {
                if (gameObject.GetComponent<UnityEngine.UI.MaskableGraphic>())
                {
                    gameObject.GetComponent<UnityEngine.UI.MaskableGraphic>().color = from.GetComponent<UnityEngine.UI.MaskableGraphic>().color;
                }
            }
            transform.GetComponent<RectTransform>().anchorMin = from.GetComponent<RectTransform>().anchorMin;
            transform.GetComponent<RectTransform>().anchorMax = from.GetComponent<RectTransform>().anchorMax;
            transform.GetComponent<RectTransform>().pivot = from.GetComponent<RectTransform>().pivot;
        }

        //Disable rotation tween if simplerotator (constant spinning script) is attached
        if (gameObject.GetComponent<SimpleRotater>())
        {
            disableRotation = true;
        }

    }

    /// <summary>
    /// Either play the going in animation or the going out animation, to go in or out
    /// </summary>
    /// <returns></returns>
    public Tween Toggle()
    {
        if (showing)
        {
            return Out();
        } else
        {
            return In();
        }
    }

    /// <summary>
    /// Animate from from to to
    /// </summary>
    /// <param name="force"></param>
    /// <returns></returns>
    public Tween In(bool force = false)
    {
        //Stop Tween duplication if this tween is already running
        if (showing && !force)
        {
            return null;
        }
        showing = true;

        Sequence sequence = DOTween.Sequence();

        //Set the anchors and pivot
        sequence.Join(
        DOTween.To(() => transform.GetComponent<RectTransform>().anchorMin, x => transform.GetComponent<RectTransform>().anchorMin = x, to.GetComponent<RectTransform>().anchorMin, duration).SetEase(xPosition).Play());
        sequence.Join(
        DOTween.To(() => transform.GetComponent<RectTransform>().anchorMax, x => transform.GetComponent<RectTransform>().anchorMax = x, to.GetComponent<RectTransform>().anchorMax, duration).SetEase(xPosition).Play());
        sequence.Join(
        DOTween.To(() => transform.GetComponent<RectTransform>().pivot, x => transform.GetComponent<RectTransform>().pivot = x, to.GetComponent<RectTransform>().pivot, duration).SetEase(xPosition).Play());


        //Animate from from to to, changing position, rotation, width/height (which isn't the same as scale), and colour
        sequence.Join(
            transform.DOMoveX(to.position.x, duration).SetEase(xPosition)
        ).Join(
            transform.DOMoveY(to.position.y, duration).SetEase(yPosition)
        ).Join(
            transform.DORotate(to.rotation.eulerAngles, disableRotation ? 0 : duration).SetEase(rotation)
        ).Join(
            transform.DOScale(to.localScale, duration).SetEase(size)
        ).Join(
            DOTween.To(() => transform.GetComponent<RectTransform>().sizeDelta, x => transform.GetComponent<RectTransform>().sizeDelta = x, to.GetComponent<RectTransform>().sizeDelta, duration).SetEase(size)
        ).Join(
            disableColour ? null : gameObject.GetComponent<MaskableGraphic>().DOColor(to.GetComponent<UnityEngine.UI.MaskableGraphic>().color, duration).SetEase(colour)
        );


        return sequence.Play();
    }
    
    /// <summary>
    /// Animate from to to from
    /// </summary>
    /// <param name="force"></param>
    /// <returns></returns>
    public Tween Out(bool force = false)
    {
        //Stop Tween duplication if this tween is already running
        if (!showing && !force)
        {
            return null;
        }
        showing = false;

        Sequence sequence = DOTween.Sequence();

        //Set the anchors and pivot
        sequence.Join(
        DOTween.To(() => transform.GetComponent<RectTransform>().anchorMin, x => transform.GetComponent<RectTransform>().anchorMin = x, from.GetComponent<RectTransform>().anchorMin, duration).SetEase(xPosition).Play());
        sequence.Join(
        DOTween.To(() => transform.GetComponent<RectTransform>().anchorMax, x => transform.GetComponent<RectTransform>().anchorMax = x, from.GetComponent<RectTransform>().anchorMax, duration).SetEase(xPosition).Play());
        sequence.Join(
        DOTween.To(() => transform.GetComponent<RectTransform>().pivot, x => transform.GetComponent<RectTransform>().pivot = x, from.GetComponent<RectTransform>().pivot, duration).SetEase(xPosition).Play());


        //Animate from to to from, changing position, rotation, width/height (which isn't the same as scale), and colour
        sequence.Join(
            transform.DOMoveX(from.position.x, duration).SetEase(xPosition)
        ).Join(
            transform.DOMoveY(from.position.y, duration).SetEase(yPosition)
        ).Join(
            transform.DORotate(from.rotation.eulerAngles, duration).SetEase(rotation)
        ).Join(
            transform.DOScale(from.localScale, duration).SetEase(size)
        ).Join(
            DOTween.To(() => transform.GetComponent<RectTransform>().sizeDelta, x => transform.GetComponent<RectTransform>().sizeDelta = x, from.GetComponent<RectTransform>().sizeDelta, duration).SetEase(size)
        ).Join(
            disableColour ? null : gameObject.GetComponent<UnityEngine.UI.MaskableGraphic>().DOColor(from.GetComponent<UnityEngine.UI.MaskableGraphic>().color, duration).SetEase(colour)
        );


        return sequence.Play();
    }

}



#if UNITY_EDITOR

////////Custom Inspector


[CustomEditor(typeof(UITweener), true)]
public class UITweenerEditor : Editor
{
    UITweener uit;
    void OnEnable()
    {
        //base.OnEnable();
        uit = target as UITweener;

        
        //If this object is still a prefab, unpack it all! Sending the children "from" and "to" objects to the canvas where they belong
        if (PrefabUtility.IsAnyPrefabInstanceRoot(uit.gameObject))
        {
            uit.from = uit.transform.Find("from");
            uit.to = uit.transform.Find("to");
            PrefabUtility.UnpackPrefabInstance(uit.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            uit.from.SetParent(uit.transform.parent);
            uit.to.SetParent(uit.transform.parent);
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        uit = target as UITweener;

        //Draw some debug lines between the real object, from object, and to object to help keep track of which things are connected together
        if (uit.from && uit.to)
        {
            Debug.DrawLine(uit.from.position, uit.to.position, Color.red);
            Debug.DrawLine(uit.from.position, uit.transform.position, Color.cyan);
            Debug.DrawLine(uit.transform.position, uit.to.position, Color.cyan);
        }

        //Run the toggle function to animate this object
        if (GUILayout.Button("Toggle InOut"))
        {
            uit.Toggle();
        }
    }
}

#endif