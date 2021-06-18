using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

[System.Serializable]
public class TweenAnimator : MonoBehaviour
{
#if UNITY_EDITOR
    public string animatorName = "TweenAnimator";
   [TextArea(0,5)] public string Notes = "";
#endif

    public bool playOnEnable = false;
    public float startDelay = 5f;
    public Sequence tweenSequence;


    [SerializeReference] public List<TweenAnimation_Abstract> animationCommands = new List<TweenAnimation_Abstract>();


    private void OnEnable()
    {
        DOTween.Init();
        tweenSequence = DOTween.Sequence();
        ProcessAnimatorData(animationCommands, tweenSequence);
        if (playOnEnable == true)
        {

            StartCoroutine(PlayAnimation());
        }

    }


    private void OnDisable()
    {
        tweenSequence.Kill();
    }


    public IEnumerator PlayAnimation()
    {
       
        yield return new WaitForSeconds(startDelay);
        tweenSequence.PlayForward();
     
       

        yield return null;

    }


    public void ReverseAnimation()
    {
        tweenSequence.PlayBackwards();
    }



    void ProcessAnimatorData(List<TweenAnimation_Abstract> AnimationList,Sequence sequence)
    {
    
        for (int i = 0; i < AnimationList.Count; i++ ){

            switch (AnimationList[i].AnimationType)
            {
                case TweenAnimationType.none:

                    break;


                case TweenAnimationType.UIfade:

                    var fadeTarget = (TweenAnimation_UIFade)AnimationList[i];
                    if (fadeTarget.target != null)
                    {
                        float startOpacity = fadeTarget.startOpacity;
                        fadeTarget.target.alpha = startOpacity;
                        //  fadeTarget.target.alpha = 0f;



                        if (fadeTarget.ease == Ease.Unset)
                        {
                            sequence.Join(fadeTarget.target.DOFade(fadeTarget.TargetOpacity, fadeTarget.animationTime).SetEase(fadeTarget.animationCurve));
                        }
                        else
                        {
                            sequence.Join(fadeTarget.target.DOFade(fadeTarget.TargetOpacity, fadeTarget.animationTime).SetEase(fadeTarget.ease));
                        }
                    }

                    break;


                case TweenAnimationType.UIposition:

                    var posTarget = (TweenAnimation_UIPosition)AnimationList[i];
                    if (posTarget.target != null)
                    {
                        posTarget.target.position = posTarget.startPosition;


                        if (posTarget.ease == Ease.Unset)
                        {
                            sequence.Join(posTarget.target.DOMove(posTarget.targetPosition, posTarget.animationTime, false).SetEase(posTarget.animationCurve));
                        }
                        else
                        {
                            sequence.Join(posTarget.target.DOMove(posTarget.targetPosition, posTarget.animationTime, false).SetEase(posTarget.ease));
                        }
                    }
                    break;


                case TweenAnimationType.UIrotation:

                    var rotTarget = (TweenAnimation_UIRotation)AnimationList[i];
                    if (rotTarget.target != null)
                    {
                        rotTarget.target.eulerAngles = rotTarget.startRotation;

                        if (rotTarget.ease == Ease.Unset)
                        {
                            sequence.Join(rotTarget.target.DORotate(rotTarget.targetRotation, rotTarget.animationTime, RotateMode.FastBeyond360).SetEase(rotTarget.animationCurve));
                        }
                        else
                        {
                            sequence.Join(rotTarget.target.DORotate(rotTarget.targetRotation, rotTarget.animationTime, RotateMode.FastBeyond360).SetEase(rotTarget.ease));
                        }
                    }
                        break;

                case TweenAnimationType.UIscale:

                    var scaleTarget = (TweenAnimation_UIScale)AnimationList[i];

                    if (scaleTarget.target != null)
                    {
                        scaleTarget.target.localScale = scaleTarget.startScale;


                        if (scaleTarget.ease == Ease.Unset)
                        {
                            sequence.Join(scaleTarget.target.DOScale(scaleTarget.targetScale, scaleTarget.animationTime).SetEase(scaleTarget.animationCurve));
                        }
                        else
                        {
                            sequence.Join(scaleTarget.target.DOScale(scaleTarget.targetScale, scaleTarget.animationTime).SetEase(scaleTarget.ease));
                        }
                    }

                    break;

                case TweenAnimationType.UIcolor:
                    var colTarget = (TweenAnimation_UIColor)AnimationList[i];

                    if (colTarget.target != null)
                    {
                        colTarget.target.color = colTarget.startColor;

                        if (colTarget.ease == Ease.Unset)
                        {
                            sequence.Join(colTarget.target.DOColor(colTarget.targetColor, colTarget.animationTime).SetEase(colTarget.animationCurve));
                        }
                        else
                        {
                            sequence.Join(colTarget.target.DOColor(colTarget.targetColor, colTarget.animationTime).SetEase(colTarget.ease));
                        }
                    }
                    break;

                case TweenAnimationType.UItargetposition:
                    var transTarget = (TweenAnimation_UITargetPosition)AnimationList[i];

                    if (transTarget != null)
                    {
                        transTarget.target.position = transTarget.startTransform.position;
                        transTarget.target.eulerAngles = transTarget.startTransform.eulerAngles;
                        transTarget.target.localScale = transTarget.startTransform.localScale;



                        if (transTarget.ease == Ease.Unset)
                        {
                            sequence.Join(transTarget.target.DOMove(transTarget.targetTransform.position, transTarget.animationTime, false).SetEase(transTarget.animationCurve));
                            sequence.Join(transTarget.target.DOScale(transTarget.targetTransform.localScale, transTarget.animationTime).SetEase(transTarget.animationCurve));
                            sequence.Join(transTarget.target.DORotate(transTarget.targetTransform.rotation.eulerAngles, transTarget.animationTime).SetEase(transTarget.animationCurve));
                        }
                        else
                        {
                            sequence.Join(transTarget.target.DOMove(transTarget.targetTransform.position, transTarget.animationTime, false).SetEase(transTarget.ease));
                            sequence.Join(transTarget.target.DOScale(transTarget.targetTransform.localScale, transTarget.animationTime).SetEase(transTarget.ease));
                            sequence.Join(transTarget.target.DORotate(transTarget.targetTransform.rotation.eulerAngles, transTarget.animationTime).SetEase(transTarget.ease));
                        }
                    }

                    break;





                case TweenAnimationType.UIshake:
                    var shakeTarget = (TweenAnimation_UIShake)AnimationList[i];

                    if (shakeTarget.target != null)
                    {
                        //   duration strength vibrato randomness fadeout


                        if (shakeTarget.ease == Ease.Unset)
                        {
                            sequence.Join(shakeTarget.target.DOShakeAnchorPos(shakeTarget.animationTime, shakeTarget.strength, shakeTarget.vibrato, shakeTarget.randomness, false, shakeTarget.fadeout).SetEase(shakeTarget.ease));
                        }
                        else
                        {
                            sequence.Join(shakeTarget.target.DOShakeAnchorPos(shakeTarget.animationTime, shakeTarget.strength, shakeTarget.vibrato, shakeTarget.randomness, false, shakeTarget.fadeout).SetEase(shakeTarget.ease));
                        }
                    }

                    break;
            }
        }

    }

 
}



