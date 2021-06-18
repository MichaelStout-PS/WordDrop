using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuAnimatorMode {Seperate,Reverse }
public class TweenMenuAnimatorController : MonoBehaviour
{
    public MenuAnimatorMode menuAnimatorMode = MenuAnimatorMode.Seperate;
    public TweenAnimator openAnimator;
    public TweenAnimator closeAnimator;
    



    public void PlayOpenAnimation()
    {
        if(openAnimator != null)
        {
            openAnimator.StartCoroutine("PlayAnimation");
        }

    }

    public void PlayCloseAnimation()
    {

        switch (menuAnimatorMode)
        {
            case MenuAnimatorMode.Reverse:
                if (openAnimator != null)
                {
                    openAnimator.ReverseAnimation();
                }
                break;




            case MenuAnimatorMode.Seperate:
                if (closeAnimator != null)
                {
                   
                    closeAnimator.PlayAnimation();

                }
                break;

        }
    }
}
