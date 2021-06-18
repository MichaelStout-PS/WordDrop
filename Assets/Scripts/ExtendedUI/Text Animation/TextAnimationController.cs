using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




/// <summary>
/// created by: James Jordan
/// Created on: 16/04/2021
/// 
/// This script is used as a non invasive way to modify text being displayed on the UI. rather than store the text component 
/// you would call this controller instead and use its methods to control the text component while incorporating the animation.
/// </summary>
public class TextAnimationController : MonoBehaviour
{

    public TextMeshProUGUI targetText;
    public TextBehaviour textBehaviour;
    public bool LoopAnimation;
    public bool PlayOnEnable;





    private void OnEnable()
    {
        if (PlayOnEnable == true)
        {
            textBehaviour.PlayAnimation(targetText, targetText.text);
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }



    public void WriteText(string newText)
    {
        textBehaviour.PlayAnimation(targetText, newText);

    }


}



public interface ITextAnimator
{


    void PlayAnimation(TextMeshProUGUI textObject, string newText);
    void StopAnimation(TextMeshProUGUI text);
}

