using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
/// <summary>
/// A default SceneSettings script which becomes active when the scene is loaded. extend this script to include more if you like.
/// </summary>
public class SceneSettings : MonoBehaviour
{
    public static SceneSettings instance;


    [Tooltip("This event is called at the very start of the scene before the first render, use it to set up anything you wish, such as UI animations")]
    public UnityEvent onEnableEvent ;


    private void OnEnable()
    {
        // Check is unnecessary if you properly manage your scenes. Delete if game doesnt set on fire.
        if (instance == null)
        {
            instance = this;
            onEnableEvent.Invoke();
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
