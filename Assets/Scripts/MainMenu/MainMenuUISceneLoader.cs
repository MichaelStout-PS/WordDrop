using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

/*
Cleverly authored by Michael "World's Greatest Genius" Stout
Copyright 2021
traineegenius.com
*/

public class MainMenuUISceneLoader : MonoBehaviour
{
    
    public bool finalLoad = false;
    public void SetFinalLoad(bool set) { finalLoad = set; }

    
    public void LoadScene()
    {
        LoadScene(0);
    }
    public void LoadScene(int sceneIndex)
    {
        //Doesn't work
        //StartCoroutine(LoadScene(SceneManager.GetSceneByBuildIndex(sceneIndex).name));
    }
    public void LoadScene(Scene scene)
    {
        StartCoroutine(LoadScene(scene.name));
    }
    public void LoadSceneName(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        //Unload all scenes apart from the first scene (MainMenu/0), and the scene that's supposed to be loading now if it's already loaded
        for (int i = 1; i < SceneManager.sceneCount; i++)
        {
            //Check if there are many scenes, keep one and unload the others
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                //Scene that is loaded and we want to stay
                //SceneManager.GetSceneAt(i)
                //If the scene is already loaded, don't reload it (just leave it)
                yield break;
            }
            else
            {
                if (finalLoad)
                {
                    //Scenes that are either not the first scene or have the wrong name
                    //If the scene is gonna change after rather than during then wait for these unloadings to happen first
                    yield return StartCoroutine(UnloadScene(SceneManager.GetSceneAt(i)));
                }
                else
                {
                    //Scenes that are either not the first scene or have the wrong name
                    StartCoroutine(UnloadScene(SceneManager.GetSceneAt(i)));
                }
            }
        }




        //This variable says whether this scene being loaded replaces the MainMenu (i.e. playing the game) 
        if (finalLoad)
        {
            //Unload this scene
            yield return StartCoroutine(UnloadScene(SceneManager.GetActiveScene()));
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        yield return null;
    }

    public IEnumerator UnloadScene(Scene scene)
    {
        //Wait for out-animation to finish before unloading (if we find one)

        //Get the object in the new scene that will tell the elements of that scene to hide
        TweenGrouper[] tweenIdeasGroupers = GameObject.FindObjectsOfType<TweenGrouper>();
        TweenGrouper tweenIdeasGrouper = null;
        foreach (TweenGrouper tig in tweenIdeasGroupers)
        {
            if (tig.gameObject.scene == scene)
            {
                tweenIdeasGrouper = tig;
                break;
            }
        }

        //If a grouper was found to hide the elements of the scene
        if (tweenIdeasGrouper != null)
        {
            //Group has already been told to hide, duplicate coroutine
            if (tweenIdeasGrouper.shown == false)
            {
                //yield break;
            }
            //Tell the grouper to hide everything
            tweenIdeasGrouper.Out();

            //Wait for the hiding tween to finish
            yield return new WaitUntil(() => tweenIdeasGrouper.GetTweensFinished());
        }
        DOTween.KillAll(true);
        //You can't unload the only active scene so don't even try
        if (scene != SceneManager.GetActiveScene())
        {
            //Scene should be offscreen, unload
            SceneManager.UnloadSceneAsync(scene);
        }

    }

}
