using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UniversalShowcaseGetter : MonoBehaviour
{

    [SerializeField] string uri = "https://michaelstout-ps.github.io/PineappleShowcaseData/";

    public ShowcaseItem[] showcaseItems;

    // Start is called before the first frame update
    void Start()
    {
        //Make a request to the uri
        //Debug.Log("HTTP Request to  " + uri);
        StartCoroutine(GetRequest(uri ?? "https://michaelstout-ps.github.io/PineappleShowcaseData/"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    //Print successful request
                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                    //Get showcaseItems (an array of ShowcaseItem objects) from JSON (using wrapper class "ShowcaseItems")
                    showcaseItems = JsonUtility.FromJson<ShowcaseItems>(webRequest.downloadHandler.text).showcaseItems;

                    //Debug print the items to see if we got 'em
                    foreach (ShowcaseItem newItem in showcaseItems)
                    {
                        Debug.Log(newItem);
                    }
                    break;

                default:
                    //Print the error from the http request (no pineapple showcase today guys)
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
            }
        }
    }

    public class ShowcaseItems {

        public ShowcaseItem[] showcaseItems;

    }

}
