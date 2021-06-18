using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class fromToScript : MonoBehaviour
{

    public GameObject targetObject;

    
}




#if UNITY_EDITOR

[CustomEditor(typeof(fromToScript), true)]
public class fromToScriptEditor : Editor
{
    fromToScript fts;
    void OnEnable()
    {
        //base.OnEnable();
        fts = target as fromToScript;


    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        fts = target as fromToScript;

        //Draw a line to show which object this is looking at
        if (fts.targetObject)
        {
            Debug.DrawLine(fts.transform.position, fts.targetObject.transform.position, Color.cyan);

        }

        //Reset to object
        if (GUILayout.Button("Reset"))
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.EndChangeCheck();
            Undo.RecordObject(fts, "Reset Positions");
            fts.transform.SetParent(fts.targetObject.transform.parent);

            fts.GetComponent<RectTransform>().anchorMin = fts.targetObject.GetComponent<RectTransform>().anchorMin;
            fts.GetComponent<RectTransform>().anchorMax = fts.targetObject.GetComponent<RectTransform>().anchorMax;
            fts.GetComponent<RectTransform>().pivot = fts.targetObject.GetComponent<RectTransform>().pivot;

            fts.transform.position = fts.targetObject.transform.position;
            fts.transform.rotation = fts.targetObject.transform.rotation;
            fts.transform.GetComponent<RectTransform>().sizeDelta = fts.targetObject.transform.GetComponent<RectTransform>().sizeDelta;
            fts.gameObject.GetComponent<UnityEngine.UI.MaskableGraphic>().color = fts.targetObject.transform.GetComponent<UnityEngine.UI.MaskableGraphic>().color;


        }
    }
}

#endif