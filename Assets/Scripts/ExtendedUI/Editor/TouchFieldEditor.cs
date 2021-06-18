using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UIAnimation;
[CustomEditor(typeof(TouchField)), CanEditMultipleObjects]
public class TouchFieldEditor : ImageEditor
{



    private bool _displayHelp;
    



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TouchField targetObject = (TouchField)target;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();

       
       targetObject.Animation_Controller = (Touch_Animation)EditorGUILayout.ObjectField("Animation Controller", targetObject.Animation_Controller, typeof(Touch_Animation), true);
        targetObject.Target = (GameObject)EditorGUILayout.ObjectField("Target", targetObject.Target, typeof(GameObject), true);


        targetObject.endOnTouchExit = EditorGUILayout.Toggle("End On Touch Exit", targetObject.endOnTouchExit);

        targetObject.dragIncrementMultiplier = EditorGUILayout.FloatField("Drag Increment Multiplier",targetObject.dragIncrementMultiplier);
        targetObject.maximumDragDuration = EditorGUILayout.FloatField("Maximum Drag Duration", targetObject.maximumDragDuration);
        targetObject.dragDuration = EditorGUILayout.FloatField("Drag Duration", targetObject.dragDuration);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        /*
        // Animation 
        EditorGUILayout.LabelField("Animation",GUI.skin.box);
        targetObject.loopGradient = EditorGUILayout.Toggle("Loop Animation",targetObject.loopGradient);
        targetObject.colorOverDuration = EditorGUILayout.GradientField("Colour Over Duration",targetObject.colorOverDuration, null);

        targetObject._gradientPosition = EditorGUILayout.FloatField("gradient Color", targetObject._gradientPosition);

    

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Swipe data
        */


        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Swipe_Data"), true);


    }
}
