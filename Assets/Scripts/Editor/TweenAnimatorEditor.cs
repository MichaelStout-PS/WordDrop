using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

[CustomEditor(typeof(TweenAnimator)), CanEditMultipleObjects]
public class TweenEdtorAnimator : Editor
{
    private bool m_ExpandUIOptions = false;

    private Ease stuff;
    private AnimationCurve _demoCurve = new AnimationCurve();
    public override void OnInspectorGUI()
    {
        TweenAnimator targetObject = (TweenAnimator)target;
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("");

        m_ExpandUIOptions = EditorGUILayout.BeginFoldoutHeaderGroup(m_ExpandUIOptions, "UI animations");

        if (m_ExpandUIOptions)
        {

           

            DisplayUITools(targetObject);



        }
    }

    void DisplayUITools(TweenAnimator targetObject)
    {

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Pos"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UIPosition());
        }

        if (GUILayout.Button("Scale"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UIScale());
        }

        if (GUILayout.Button("Rotation"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UIRotation());
        }

        EditorGUILayout.EndHorizontal();




        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("TargetPos"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UITargetPosition());
        }
        if (GUILayout.Button("Fade"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UIFade());
        }
        if (GUILayout.Button("Color"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UIColor());
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Shake"))
        {
            targetObject.animationCommands.Add(new TweenAnimation_UIShake());
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndFoldoutHeaderGroup();



        EditorGUILayout.LabelField("");
        EditorGUILayout.Space();
        stuff = (Ease)EditorGUILayout.EnumPopup("Gradient Generator", stuff);
        
        EditorGUILayout.CurveField("Example curve", _demoCurve);
      
        
        EditorGUILayout.LabelField("");

    }

    Tween tsts;
    
}



