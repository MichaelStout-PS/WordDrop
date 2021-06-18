using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using UnityEditor.UI;

[CustomEditor(typeof(ExtendedButton)), CanEditMultipleObjects]
public class ExtendedButtonEditor : ButtonEditor
{

    // target

    protected static bool ShowExtraClicks = true;

    private Selectable.Transition transitionType;
    private ColorBlock Colours;


    public void OnInspectorUpdate()
    {
        this.Repaint();
    }







    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onDoubleClick"), true);


        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        ShowExtraClicks = EditorGUILayout.Foldout(ShowExtraClicks, "Extra Click Events", true, EditorStyles.foldout);
        if (ShowExtraClicks)
        {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onRightClick"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onMiddleClick"), true);
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onPointerEnter"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onPointerExit"), true);
        this.serializedObject.ApplyModifiedProperties();


    }




}
