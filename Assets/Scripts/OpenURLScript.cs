using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenURLScript : MonoBehaviour, IPointerDownHandler
{
    public string url = "https://www.google.com/";

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Opening URL: " + url);
        Application.OpenURL(url);
    }


}
