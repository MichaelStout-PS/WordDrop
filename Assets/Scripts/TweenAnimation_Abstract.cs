using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
public enum TweenAnimationType {none,UIscale,UIposition,UItargetposition,UIrotation,UIfade, UIcolor, UIshake, UIfill}









[Serializable]
public class TweenAnimation_Abstract 
{

    public string animationName = "";
    public float animationTime = 1f;
    public Ease ease;
    public AnimationCurve animationCurve = AnimationCurve.Linear(0,0,1,1);
    [NonSerialized] public TweenAnimationType AnimationType = TweenAnimationType.none;
}



// Scale animation 
[Serializable]
public class TweenAnimation_UIScale : TweenAnimation_Abstract
{
   
    public RectTransform target;
    public Vector3 startScale = Vector3.zero;
    public Vector3 targetScale = Vector3.one;

    public TweenAnimation_UIScale()
    {
        
        this.animationName = "UIScale";
        this.target = null;
        this.startScale = Vector3.zero;
        this.targetScale = Vector3.one;
        this.AnimationType = TweenAnimationType.UIscale;
    }

    public TweenAnimation_UIScale(RectTransform target,Vector3 startScale, Vector3 targetScale, AnimationCurve animationCurve, float animationTime)// : base( animationCurve, animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UIscale;
        this.startScale = startScale;
        this.targetScale = targetScale;
        this.animationCurve = animationCurve;
        this.animationTime = animationTime;
    }
}

// position animation 
[Serializable]
public class TweenAnimation_UIPosition : TweenAnimation_Abstract
{
    public RectTransform target;
    public Vector3 startPosition = Vector3.zero;
    public Vector3 targetPosition = Vector3.one;

    public TweenAnimation_UIPosition()
    {
        this.animationName = "UIPosition";
        this.target = null;
        this.startPosition = Vector3.zero;
        this.targetPosition = Vector3.one;
        this.AnimationType = TweenAnimationType.UIposition;
    }

    public TweenAnimation_UIPosition(RectTransform target, Vector3 startPosition, Vector3 targetPosition, AnimationCurve animationCurve, float animationTime)// : base( animationCurve, animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UIposition;
        this.startPosition = startPosition;
        this.targetPosition = targetPosition;
        this.animationCurve = animationCurve;
        this.animationTime = animationTime;
    }
}



// Target position animation 
[Serializable]
public class TweenAnimation_UITargetPosition : TweenAnimation_Abstract
{
    public RectTransform target;
    public RectTransform startTransform = null;
    public RectTransform targetTransform = null;

    public TweenAnimation_UITargetPosition()
    {
        this.animationName = "UITargetPosition";
        this.target = null;
        this.startTransform = null;
        this.targetTransform = null;
        this.AnimationType = TweenAnimationType.UItargetposition;
    }

    public TweenAnimation_UITargetPosition(RectTransform target, RectTransform startTransform, RectTransform targetTransform, AnimationCurve animationCurve, float animationTime)// : base( animationCurve, animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UItargetposition;
        this.startTransform = startTransform;
        this.targetTransform = targetTransform;
        this.animationCurve = animationCurve;
        this.animationTime = animationTime;
    }
}

// Rotation animation 
[Serializable]
public class TweenAnimation_UIRotation : TweenAnimation_Abstract
{
    
    public RectTransform target;
    public Vector3 startRotation = Vector3.zero;
    public Vector3 targetRotation = Vector3.zero;

    public TweenAnimation_UIRotation()
    {
        this.animationName = "UIRotation";
        this.target = null;
        this.startRotation = Vector3.zero;
        this.targetRotation = Vector3.zero;
        this.AnimationType = TweenAnimationType.UIrotation;
    }

    public TweenAnimation_UIRotation(RectTransform target, Vector3 startRotation, Vector3 targetrotation, AnimationCurve animationCurve, float animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UIrotation;
        this.startRotation = startRotation;
        this.targetRotation = targetrotation;
        this.animationCurve = animationCurve;
        this.animationTime = animationTime;
    }
}



// Fade animation
[Serializable]
public class TweenAnimation_UIFade : TweenAnimation_Abstract
{
    public CanvasGroup target;
    public float startOpacity = 0f;
    public float TargetOpacity = 1f;


    public TweenAnimation_UIFade()
    {
        this.animationName = "UIFade";
        this.target = null;
        this.startOpacity = 0f;
        this.TargetOpacity = 1f;
        this.AnimationType = TweenAnimationType.UIfade;
    }

    public TweenAnimation_UIFade(CanvasGroup target,float startOpacity, float TargetOpacity, AnimationCurve animationCurve, float animationTime)// : base( animationCurve, animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UIfade;
        this.startOpacity = startOpacity;
        this.TargetOpacity = TargetOpacity;
        this.animationCurve = animationCurve;
        this.animationTime = animationTime;
    }

}



[Serializable]
public class TweenAnimation_UIColor : TweenAnimation_Abstract
{

    public Color startColor = Color.white;

    public Color targetColor = Color.white;

    public Graphic target = null;

    


    public TweenAnimation_UIColor()
    {
        this.animationName = "UIColor";
        this.target = null;
        this.startColor = Color.white;
        this.targetColor = Color.white;
        this.AnimationType = TweenAnimationType.UIcolor;
    }

    public TweenAnimation_UIColor(Graphic target, Color startColor, Color TargetColor, AnimationCurve animationCurve, float animationTime)// : base( animationCurve, animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UIcolor;
        this.startColor = startColor;
        this.targetColor = TargetColor;
        this.animationCurve = animationCurve;
        this.animationTime = animationTime;
    }

}


[Serializable]
public class TweenAnimation_UIShake : TweenAnimation_Abstract
{
    public RectTransform target;
    public float strength = 100f;
    public int vibrato = 10;
    public float randomness = 90f;
    public bool fadeout = true;

    //   duration strength vibrato randomness fadeout


    public TweenAnimation_UIShake()
    {
        this.animationName = "Shake";
        this.target = null;        
        this.AnimationType = TweenAnimationType.UIshake;
    }

    public TweenAnimation_UIShake(RectTransform target, float animationTime, float strength, int vibrato, float randomness, bool fadeout)// : base( animationCurve, animationTime)
    {
        this.target = target;
        this.AnimationType = TweenAnimationType.UIshake;
        this.fadeout = fadeout;
        this.randomness = randomness;
        this.vibrato = vibrato;
        this.strength = strength;
        this.animationTime = animationTime;
    }
}


[Serializable]
public class TweenAnimation_UIFill : TweenAnimation_Abstract
{
    public Image Target;
    public Image.FillMethod fillMethod;

    


}


    [Serializable]
public class TweenAnimation_UINON
{
    public GameObject target;


}




