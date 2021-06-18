using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// created by: James Jordan
/// Created on: 16/04/2021
/// 
/// Base class for text animation components, Used alongside TextMeshPro
/// </summary>
public abstract class TextBehaviour : MonoBehaviour, ITextAnimator
{
    public bool IsAnimating;
    public bool LoopAnimation;



   





    public virtual void PlayAnimation(TextMeshProUGUI textObject, string newText)
    {
        IsAnimating = true;
        
    }


    public virtual void StopAnimation(TextMeshProUGUI text)
    {
        IsAnimating = false;

    }






}
