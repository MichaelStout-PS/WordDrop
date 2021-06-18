using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIAnimation
{
    public abstract class Touch_Animation : MonoBehaviour, ITouchAnim_Behaviours
    {




        public virtual void StartAnimation(float dragIncrement, float maximumDragDuration)
        {

        }


        public virtual void AnimationReciever(float dragDuration)
        {
           
        }


        public virtual void EndAnimation()
        {
           
        }

       
    }
}




