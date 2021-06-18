using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIAnimation
{

    /// <summary>
    /// James Jordan
    /// 12/04/2021
    /// Animation controller used for extended functionality of TouchField Input
    /// </summary>


    public class Animation_Fill : Touch_Animation
    {
        public Image targetImage;

        [Header("Input Settings")]
        public bool UseControllerValues;
        public float fillIncrementMultiplier = 0.01f;
        public float fillDuration = 0f;
        public float fillPosition = 0f;
        public float resetSpeedMultiplier = 1f;
        public TouchField_TransitionType Transition;


       

        public override void StartAnimation(float dragIncrementMultiplier,float maximumDragDuration)
        {
            StopCoroutine("ResetLerpAnimation");

            if (UseControllerValues == true)
            {
                this.fillIncrementMultiplier = dragIncrementMultiplier;
                this.fillDuration = maximumDragDuration;
            }
            
        }


        public override void AnimationReciever(float dragDuration)
        {
            fillPosition += Time.deltaTime * fillIncrementMultiplier;
            targetImage.fillAmount = fillPosition;
        }

        public override void EndAnimation()
        {
            switch (Transition)
            {

                case TouchField_TransitionType.Reset:
                    fillPosition = 0f;
                    targetImage.fillAmount = fillPosition;
                    break;

                case TouchField_TransitionType.ResetLerp:
                    StartCoroutine("ResetLerpAnimation");

                    break;

            }
        }



            private IEnumerator ResetLerpAnimation()
            {

           fillPosition = Mathf.Clamp(fillPosition, 0, 1);

                while (targetImage.fillAmount != 0f)
                {
                fillPosition -= Time.deltaTime * resetSpeedMultiplier;
                targetImage.fillAmount = fillPosition;
                yield return new WaitForSeconds(0.01f);
                }
            }











    }


}
