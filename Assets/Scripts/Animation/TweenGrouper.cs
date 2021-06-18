using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

/*
Cleverly authored by Michael "World's Greatest Genius" Stout
Copyright 2021
traineegenius.com
*/

public class TweenGrouper : MonoBehaviour
{


    public bool shown = true;

    public MonoBehaviour[] manualObjects = null;

    public List<ITweenManager> tis = new List<ITweenManager>();

    List<Tween> _tweens = new List<Tween>();

    // Start is called before the first frame update
    void Start()
    {
        //Put all the children into the list
        tis.AddRange(GetComponentsInChildren<SimpleMover>());
        tis.AddRange(GetComponentsInChildren<SimpleScaler>());
        tis.AddRange(GetComponentsInChildren<SimpleRotater>());
        tis.AddRange(GetComponentsInChildren<UITweener>());


        if (manualObjects != null)
        {
            foreach (MonoBehaviour _object in manualObjects)
            {
                if ((_object as ITweenManager) != null)
                {
                    tis.Add(_object as ITweenManager);
                }
                else
                {
                    Debug.Log("Object not valid as a ITweenIdeas");
                }
            }
        }

        Show(shown);
    }


    public void Toggle()
    {
        Show(!shown);
    }

    public void In()
    {
        Show(true);
    }
    public void Out()
    {
        Show(false);
    }

    public void Show(bool show)
    {
        shown = show;
        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        bool _shown = shown;

        //Remember all the tweens that are running
        foreach (ITweenManager ti in tis)
        {
            if (_shown)
            {
                _tweens.Add(ti.In());
            } else
            {
                _tweens.Add(ti.Out());
            }
            yield return new WaitForSeconds(Random.value/3);
        }

        //Wait for all tweens to finish before confirming tweenRunning
        for (int i = _tweens.Count-1; i >= 0; i--)
        {
            //If this isn't available any more, skip this number
            if (i >= _tweens.Count)
            {
                continue;
            }
            Tween t = _tweens[i];

            if (t.IsActive())
            {
                yield return t.WaitForCompletion();
            }
            t.Kill(true);
            _tweens.Remove(t);
        }
        
    }

    public bool GetTweensFinished()
    {
        return _tweens.Count == 0;
    }

    //This should be when the window is resized/when the phone is turned around
    void OnRectTransformDimensionsChange()
    {
        //Tell everything to go to where it should be
        //Debug.Log("Canvas resized!");
        CheckPositions();
    }

    public void CheckPositions()
    {
        foreach (ITweenManager ti in tis)
        {
            if (shown)
            {
                ti.In(true);
            }
            else
            {
                ti.Out(true);
            }
        }
    }
}


#if UNITY_EDITOR

////////Custom Inspector


[CustomEditor(typeof(TweenGrouper))]
public class TweenIdeasGrouperEditor : Editor
{
    TweenGrouper tig;
    void OnEnable()
    {
        //base.OnEnable();
        tig = target as TweenGrouper;
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        tig = target as TweenGrouper;

        tig.shown = EditorGUILayout.Toggle("Shown now", tig.shown);

        EditorGUILayout.LabelField(tig.tis.Count + " TweenIdeas objects");
        if (GUILayout.Button("Toggle InOut"))
        {
            tig.Toggle();
        }

        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.Toggle("Tweens are playing now: ", !tig.GetTweensFinished());
        } else
        {

            EditorGUILayout.PropertyField(serializedObject.FindProperty("manualObjects"));

        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif