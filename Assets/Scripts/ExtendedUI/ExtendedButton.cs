using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
namespace UnityEngine.UI {
    [AddComponentMenu("UI/ExtendedButton", 31)]
    public class ExtendedButton : Button, ISelectHandler, IDeselectHandler
    {

        private float LastClicked = 0;


        public ButtonClickedEvent onPointerEnter;

        public ButtonClickedEvent onPointerExit;

        public ButtonClickedEvent onRightClick;

        public ButtonClickedEvent onMiddleClick;

        public ButtonClickedEvent onDoubleClick;

        [Tooltip("The object that will recieve the buttons messages throughout processing. Useful for animators")]
        public GameObject MessageTarget;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onPointerEnter.Invoke();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            onPointerExit.Invoke();
        }


        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            onPointerEnter.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            onPointerExit.Invoke();
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {

                case PointerEventData.InputButton.Left:


                    switch (eventData.clickCount)
                    {
                        case 1:

                            Debug.Log("single click");
                            onClick.Invoke();

                            break;


                        case 2:
                            onRightClick.Invoke();
                            if (!IsActive() || !IsInteractable())
                                return;
                            Debug.Log("Double clicked");
                            DoStateTransition(SelectionState.Pressed, false);
                            StartCoroutine("OnFinishSubmit");
                            break;
                    }






                    break;

                case PointerEventData.InputButton.Right:
                    onRightClick.Invoke();

                    // if we get set disabled during the press
                    // don't run the coroutine.
                    if (!IsActive() || !IsInteractable())
                        return;

                    DoStateTransition(SelectionState.Pressed, false);
                    StartCoroutine("OnFinishSubmit");
                    break;


                case PointerEventData.InputButton.Middle:
                    onMiddleClick.Invoke();
                    // if we get set disabled during the press
                    // don't run the coroutine.
                    if (!IsActive() || !IsInteractable())
                        return;

                    DoStateTransition(SelectionState.Pressed, false);
                    StartCoroutine("OnFinishSubmit");
                    break;


            }
        }



    }
}