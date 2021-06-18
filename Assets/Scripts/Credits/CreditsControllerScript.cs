using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static CreditsControllerScript;
using DG.Tweening;

/*
Cleverly authored by Michael "World's Greatest Genius" Stout
Copyright 2021
traineegenius.com
*/

public class CreditsControllerScript : MonoBehaviour, ITweenManager
{
    //[Serializable]
    public List<CreditsEntry> credits = new List<CreditsEntry>(1);

    List<CreditEntryLerp> creditsObjects = new List<CreditEntryLerp>();

    [Serializable]
    public class CreditsEntry
    {
        public string name;
        public string role;

        public Color color;
        public CreditsEntry(string name,string role) : this(name, role, Color.black)
        {
        }
        public CreditsEntry(string name, string role, Color color)
        {
            this.name = name;
            this.role = role;

            this.color = color;
            this.color.a = 1;
        }
    }

    [SerializeField] Canvas canvas;
    [SerializeField] GameObject creditsPrefab;

    [SerializeField][Range(0.01f, 10)] float durationEach;

    public Font font;

    // Start is called before the first frame update
    void Start()
    {
        //credits.Add(new CreditsEntry("MICHAEL STOUT", "CREDITS PROGRAMMER"));

        //In() on start should be done by the Grouper this is attached to
        //In();
    }

    public Tween In(bool force = false)
    {
        if (creditsObjects.Count > 0)
        {
            Out();
        }
        StartCoroutine(Credits());
        return null;
    }

    public Tween Out(bool force = false)
    {
        Tween outTween = null;
        StopAllCoroutines();
        //Move all existing credits objects off screen and destroy them 
        foreach (CreditEntryLerp c in creditsObjects)
        {
            outTween = c.Kill();
        }
        creditsObjects.Clear();
        return outTween;
    }

    public IEnumerator Credits()
    {
        //Debug.Log("Credits are GO!");
        for (int i = 0; i < credits.Count; i++)
        {
            //Make new credits entry object
            GameObject newCreditsEntry = Instantiate(creditsPrefab, canvas.transform);
            creditsObjects.Add(newCreditsEntry.GetComponent<CreditEntryLerp>());
            newCreditsEntry.GetComponent<CreditEntryLerp>().canvas = canvas;
            //Set text to text from credits controller list
            newCreditsEntry.transform.GetChild(0).GetComponent<Text>().text = credits[i].name;
            newCreditsEntry.transform.GetChild(1).GetComponent<Text>().text = credits[i].role;
            newCreditsEntry.transform.GetChild(0).GetComponent<Text>().color = credits[i].color;
            newCreditsEntry.transform.GetChild(1).GetComponent<Text>().color = credits[i].color;
            newCreditsEntry.transform.GetChild(0).GetComponent<Text>().font = font;
            newCreditsEntry.transform.GetChild(1).GetComponent<Text>().font = font;
            
            newCreditsEntry.transform.position = new Vector3(newCreditsEntry.transform.position.x, 0);

            //Set times for duration and wait time (wait for other credits before looping again)
            newCreditsEntry.GetComponent<CreditEntryLerp>().duration = durationEach;
            newCreditsEntry.GetComponent<CreditEntryLerp>().waitTime = credits.Count-2*(durationEach/2);

            //Tell credit to go
            newCreditsEntry.GetComponent<CreditEntryLerp>().Go();

            //Wait for credit that just went before sending next one
            yield return new WaitForSeconds(durationEach/2);
        }
    }

}


#if UNITY_EDITOR

[CustomEditor(typeof(CreditsControllerScript))]
public class CreditsControllerEditor : Editor
{

    CreditsControllerScript ccs;

    void OnEnable()
    {
        ccs = target as CreditsControllerScript;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ccs = target as CreditsControllerScript;

        //Display number of elements, add and remove buttons
        GUILayout.BeginHorizontal();

        GUILayout.Label("Length: " + ccs.credits.Count);
        if (GUILayout.Button("Add"))
        {
            ccs.credits.Add(new CreditsControllerScript.CreditsEntry(" ", " "));
        }
        
        GUILayout.EndHorizontal();

        if (ccs.credits.Count > 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("i");
            GUILayout.Label("Name");
            GUILayout.Label("Role");
            GUILayout.EndHorizontal();
        }


        for (int i = 0; i < ccs.credits.Count; i++)
        {
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(i.ToString());
            ccs.credits[i].name = GUILayout.TextField(ccs.credits[i].name);
            ccs.credits[i].role = GUILayout.TextField(ccs.credits[i].role);
            if (GUILayout.Button("X"))
            {
                ccs.credits.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
            ccs.credits[i].color = EditorGUILayout.ColorField(ccs.credits[i].color);

            EditorUtility.SetDirty(ccs);
            //EditorUtility.SetDirty(ccs.credits);
            //Undo.RecordObject(ccs, "credits");
            //serializedObject.ApplyModifiedProperties();
        }


    }
}

#endif