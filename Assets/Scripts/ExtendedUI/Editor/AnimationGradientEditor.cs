using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UIAnimation;
using UnityEngine.UI;
/// <summary>
/// created by: James Jordan
/// Created 15/04/2021
/// </summary>
/// 
[CustomEditor(typeof(Animation_Gradient)), CanEditMultipleObjects]
public class AnimationGradientEditor : Editor
{
    private Animation_Gradient _targetObject;

    private string ComponentDescription = "This component will process TouchField and ChargeButton events and animate target UI graphics accordingly.";


    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        _targetObject = (Animation_Gradient)target;


        EditorGUILayout.TextField("How to use", EditorStyles.whiteBoldLabel);
        EditorGUILayout.SelectableLabel(ComponentDescription, EditorStyles.helpBox);
        EditorGUILayout.Space();

        //Editor settings
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
            EditorGUILayout.TextField("Animation Settings", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space();

            // Display settings if not using the touchfields values.
            _targetObject.UseControllerValues = EditorGUILayout.Toggle("Use TouchField Values", _targetObject.UseControllerValues);
            if (!_targetObject.UseControllerValues)
            {
                _targetObject.DragIncrementMultiplier = EditorGUILayout.FloatField("Drag Increment Multiplier", _targetObject.DragIncrementMultiplier);
                _targetObject.maximumDragDuration = EditorGUILayout.FloatField("maximum Drag Duration", _targetObject.maximumDragDuration);
            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        }


        //Gradient settings
        {

            EditorGUILayout.TextField("Gradient Settings", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space();
            _targetObject.targetGraphic = (Graphic)EditorGUILayout.ObjectField("Target Graphic ", _targetObject.targetGraphic, typeof(Graphic), true);

            _targetObject.loopGradient = EditorGUILayout.Toggle("Loop Animation ", _targetObject.loopGradient);
            _targetObject.colorOverDuration = EditorGUILayout.GradientField("Colour Over Duration ", _targetObject.colorOverDuration);

            _targetObject.GradientTransition = (TouchField_TransitionType)EditorGUILayout.EnumPopup(new GUIContent("Gradient Transition type", "How the animator will process the animation end"), _targetObject.GradientTransition);


            if (_targetObject.GradientTransition == TouchField_TransitionType.ResetLerp)
            {
                _targetObject.resetSpeedMultiplier = EditorGUILayout.FloatField(new GUIContent("Reset Speed Multiplier", "Increases the speed of the reset animation"), _targetObject.resetSpeedMultiplier);
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }




    }

}

