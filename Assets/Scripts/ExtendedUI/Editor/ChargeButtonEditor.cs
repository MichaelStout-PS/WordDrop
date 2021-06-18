using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using UnityEditor.UI;
using UIAnimation;

[CustomEditor(typeof(ChargeButton)), CanEditMultipleObjects]
public class ChargeButtonEditor : ButtonEditor
{









    public override void OnInspectorGUI()
    {
        ChargeButton targetObject = (ChargeButton)target;

        base.OnInspectorGUI();
      
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();
        targetObject.Animation_Controller = (Touch_Animation)EditorGUILayout.ObjectField("Animation Controller", targetObject.Animation_Controller, typeof(Touch_Animation), true);

    }
}
