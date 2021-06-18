using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
Cleverly authored by Michael "World's Greatest Genius" Stout
Copyright 2021
traineegenius.com
*/

public class CreditEntryLerp : MonoBehaviour
{
    [System.NonSerialized]
    public Canvas canvas;

    [SerializeField] public float duration = 2;
    [SerializeField] AnimationCurve _ease;
    public float waitTime;


    Font font;

    bool killed = false;

    private Sequence loopSequence;


    public void Go()
    {
        loopSequence = DOTween.Sequence().Append(
            transform.DOMove(new Vector3(transform.position.x, canvas.pixelRect.height+GetComponent<RectTransform>().rect.height), duration).SetEase(_ease))
            .AppendInterval(waitTime)
            .SetLoops(-1).Play();
    }

    public Tween Kill()
    {
        //Stop Tween duplication if this tween is already running
        if (killed) { return null; }
        killed = true;

        loopSequence.Kill();

        Tween outTween = DOTween.Sequence().Append(
            transform.DOMove(new Vector2(Screen.width*3, transform.position.y), duration).SetAutoKill(true)
        ).Play();

        Destroy(gameObject, duration + 1);

        return outTween;
    }
    
}
