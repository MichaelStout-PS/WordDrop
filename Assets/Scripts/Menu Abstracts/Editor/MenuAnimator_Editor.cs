using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MenuAnimator)), CanEditMultipleObjects]
public class MenuAnimator_Editor :Editor
{

    SerializedProperty m_menuTransform;
    SerializedProperty m_canvasGroup;

    bool m_displayOpenSettings = false;


    private void OnEnable()
    {
        m_menuTransform = serializedObject.FindProperty("menuContainer");
        m_canvasGroup = serializedObject.FindProperty("canvasGroup");
    }




    public override void OnInspectorGUI()
    {
        MenuAnimator targetObject = (MenuAnimator)target;
        targetObject.openOnEnable = EditorGUILayout.ToggleLeft("Open On Enable", targetObject.openOnEnable);



        EditorGUILayout.PropertyField(m_menuTransform, new GUIContent("Menu Container"));
        EditorGUILayout.PropertyField(m_canvasGroup, new GUIContent("Canvas Group"));
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


       
        m_displayOpenSettings = EditorGUILayout.ToggleLeft("Expand Open Animation Settings", m_displayOpenSettings);
        EditorGUILayout.Space();
        if (m_displayOpenSettings == true)
        {


            targetObject.enableScale = EditorGUILayout.Toggle("Enable Scaling", targetObject.enableScale);
           
            if (targetObject.enableScale == true)
            {
                targetObject.scaleCurve = EditorGUILayout.CurveField("Scale Curve", targetObject.scaleCurve);
                targetObject.targetScale = EditorGUILayout.Vector3Field("Target Scale", targetObject.targetScale);
                targetObject.ScaleDuration = EditorGUILayout.FloatField("Scale Duration", targetObject.ScaleDuration);
            }
            



            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            //fade settings

            targetObject.enableFade = EditorGUILayout.Toggle("Enable Fading", targetObject.enableFade);
            if (targetObject.enableFade == true)
            {
                targetObject.fadeCurve = EditorGUILayout.CurveField("Fade Curve", targetObject.fadeCurve);
                targetObject.targetOpacity = EditorGUILayout.FloatField("Target Opacity", targetObject.targetOpacity);
                targetObject.fadeDuration = EditorGUILayout.FloatField("Fade Duration", targetObject.fadeDuration);
            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


            // positioning settings
            targetObject.enablePositioning = EditorGUILayout.Toggle("Enable Positioning", targetObject.enablePositioning);

            if (targetObject.enablePositioning == true)
            {
                targetObject.positioningCurve = EditorGUILayout.CurveField("Positioning Curve", targetObject.positioningCurve);
                targetObject.targetPosition = EditorGUILayout.Vector3Field("Target Position", targetObject.targetPosition);
                targetObject.positioningDuration = EditorGUILayout.FloatField("Positioning Duration", targetObject.positioningDuration);
            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);




        }




        serializedObject.ApplyModifiedProperties();

    }
}
