using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UIAnimation;

namespace UnityEngine.UI
{

    [AddComponentMenu("UI/ChargeButton", 32)]


    /// <summary>
    /// Created by: James Jordan
    /// created 14/04/2021
    /// 
    /// This class is an experiment as to whether the behaviour of the TouchField component can be added to buttons through a simple extension rather
    /// than a new class. Touch_Animation derived classes are designed to work with this class in the same way they do TouchFields.
    /// 
    /// </summary>
    public class ChargeButton : Button
    {

        private bool Touching;

        private float HoldTime;


        public float holdIncrement = 1f;
        public float maxHoldTime = 5f;

        public Touch_Animation Animation_Controller;


        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            Touching = true;

            Animation_Controller.StartAnimation(holdIncrement, maxHoldTime);
            StartCoroutine("HoldTimer");



        }


        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            Touching = false;

            Animation_Controller.EndAnimation();
        }




        /// <summary>
        /// Is called by onPointerDown and cancelled by OnPointerUp. Duration is passed off to the animation component each loop
        /// </summary>
        /// <returns></returns>
        private IEnumerator HoldTimer()
        {

            while (Touching == true)
            {
                HoldTime += holdIncrement;
                Animation_Controller.AnimationReciever(HoldTime);
                yield return new WaitForSeconds(0.01f * Time.deltaTime);
            }


            yield return null;
        }
    }
}
