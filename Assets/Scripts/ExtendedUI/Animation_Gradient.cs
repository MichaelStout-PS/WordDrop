using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIAnimation
{

    public enum TouchField_TransitionType {None,Reset,ResetLerp }

    /// <summary>
    /// created by: James Jordan
    /// Created on: 08/04/2021
    /// </summary>
    public class Animation_Gradient : Touch_Animation
    {

        #region Input Settings
        public bool UseControllerValues;
        public float DragIncrementMultiplier = 1f;
        public float maximumDragDuration = 1;
        #endregion


        #region Gradient Settings
        public Graphic targetGraphic;
        public bool loopGradient = true;
        public TouchField_TransitionType GradientTransition;
        public Gradient colorOverDuration = new Gradient();
        public float gradientPosition;
        public float resetSpeedMultiplier = 1f;
        #endregion



       //  Expression body testing
        public void ReportStats() => Debug.Log(DragIncrementMultiplier);

        public float GradientPosReport() => gradientPosition += 5f;

        public override string ToString() => $"this object has a gradient position of {gradientPosition}";

    


        public void Awake()
        {
            ReportStats();
            Debug.Log(GradientPosReport());
            Debug.Log(ToString());

         
        }

            




        /// <summary>
        /// Cancels any animations and sets values so that the event can be processed.
        /// </summary>
        /// <param name="dragIncrement">New dragIncrementMultiplier value</param>
        /// <param name="maximumDragDuration">New maximumDragDuration value</param>
        public override void StartAnimation(float dragIncrement, float maximumDragDuration)
        {
            if (UseControllerValues == true)
            {
                this.DragIncrementMultiplier = dragIncrement;
                this.maximumDragDuration = maximumDragDuration;
            }


            StopCoroutine("ResetLerpAnimation");           
        }


        /// <summary>
        /// repeatedly called by the animator durring processing
        /// </summary>
        /// <param name="dragDuration"></param>
        public override void AnimationReciever(float dragDuration)
        {
            // increase _gradientColor or reset to 0 if loop enabled
            gradientPosition += Time.deltaTime * DragIncrementMultiplier;
           
            if (gradientPosition > maximumDragDuration && loopGradient == true)
            {
               // Debug.Log("Gradient reset is running");
                gradientPosition = 0f;
            }

           
            float gradientKey = Mathf.InverseLerp(0f, maximumDragDuration, gradientPosition);
            targetGraphic.color = colorOverDuration.Evaluate(gradientKey);
            
        }


        /// <summary>
        /// Triggers the reset of the animation dependent on transition type
        /// </summary>
        public override void EndAnimation()
        {          

            switch (GradientTransition)
            {

                //reset with no transition
                case TouchField_TransitionType.Reset:
                    gradientPosition = 0f;
                    targetGraphic.color = colorOverDuration.Evaluate(0f);
                    break;

                //reset with lerp transition
                case TouchField_TransitionType.ResetLerp:
                    StartCoroutine(ResetLerpAnimation());
                    break;



            }
        }



        /// <summary>
        /// Lerps object colour back to 0 over time.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ResetLerpAnimation()
        {
            Color targetColor = colorOverDuration.Evaluate(0f);


            Mathf.Clamp(gradientPosition, 0, maximumDragDuration);


            while (targetGraphic.color != targetColor)
            {
                gradientPosition -= ( Time.deltaTime * resetSpeedMultiplier);
                float gradientKey = Mathf.InverseLerp(0f, maximumDragDuration, gradientPosition);

                

                targetGraphic.color = colorOverDuration.Evaluate(gradientKey);
                yield return new WaitForSeconds(0.01f);
            }



            yield return null;
        }

    }
}
