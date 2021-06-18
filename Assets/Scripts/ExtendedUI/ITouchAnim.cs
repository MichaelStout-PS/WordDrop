using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIAnimation
{
    public interface ITouchAnim_Behaviours
    {

        void StartAnimation(float dragIncrement, float maximumDragDuration);

        void AnimationReciever(float dragDuration);

        void EndAnimation();

    }




}
