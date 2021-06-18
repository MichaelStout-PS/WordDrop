using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
public class ShowcaseController : MonoBehaviour
{
    public static bool preloadedShowcase = false;
    [SerializeField] private ShowcaseData _showcaseData;
    [SerializeField] private Transform _ContentContainer;
    [SerializeField] private GameObject _showcaseButton;
    [SerializeField] private ShowcaseData _fallbackShowcaseData;
    [SerializeField] private string _showcaseFileURL = "https://github.com/MichaelStout-PS/PineappleShowcaseData/raw/3b0429fa3841bde613f105f14bb0e84bee8dc9a4/ShowcaseData.PSS";

    private void OnEnable()
    {
        if (preloadedShowcase == false)
        {
            StartCoroutine(DownloadShowcaseData());
            preloadedShowcase = true;
        }
        else
        {
            PopulateTabs();
        }

        
    }

    private void OnDisable()
    {
        
    }



    private IEnumerator CheckShowcaseVersion()
    {


        yield return null;
    }




    private IEnumerator DownloadShowcaseData()
    {
        UnityWebRequest www = new UnityWebRequest(_showcaseFileURL);
        www.downloadHandler = new DownloadHandlerFile(Application.persistentDataPath + "/ShowcaseData" + ".PSS");

        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("file unrecoverable");
            // file cannot be retrieved
        }

        else
        {
            // success and saved to path
            Debug.Log("filesaved");

            var newData = ProcessShowcaseData();
            _showcaseData.showcaseItems = newData;
        }
        PopulateTabs();
        yield return null;
    }


    



    private void CheckForFile()
    {

    }




    private List<ShowcaseItem> ProcessShowcaseData()
    {
        
        if (File.Exists(Application.persistentDataPath + "/ShowcaseData" + ".PSS"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ShowcaseData" + ".PSS", FileMode.Open);
            ShowcaseSaveData data = (ShowcaseSaveData)bf.Deserialize(file);
            file.Close();

            return data.showcaseItems;
        }


        else
        {
            return _fallbackShowcaseData.showcaseItems;
        }
        
    }




    private void PopulateTabs()
    {
        // instantiate all buttons and options
        for (int i = 0; i < _showcaseData.showcaseItems.Count; i++)
        {
            var item = _showcaseData.showcaseItems[i];
            Button newButton = Instantiate(_showcaseButton, _ContentContainer, true).GetComponent<Button>(); ;


            // convert and set the sprite
           Sprite icon = ImageConversion.GenerateTexture(item.icon);
            newButton.image.sprite = icon;

            // set onClick()
            switch (Application.platform)
            {


                case RuntimePlatform.Android:
                newButton.onClick.AddListener(delegate { Application.OpenURL(item.androidLink); }); 
                    break;

                case RuntimePlatform.IPhonePlayer:
                newButton.onClick.AddListener(delegate { Application.OpenURL(item.iosLink); });
                    break;

                case RuntimePlatform.WindowsPlayer:
                newButton.onClick.AddListener(delegate { Application.OpenURL(item.pcLink); });
                    break;

                case RuntimePlatform.WindowsEditor:
                    newButton.onClick.AddListener(delegate { Application.OpenURL(item.pcLink); });
                    break;

            }


            newButton.gameObject.SetActive(true);

        }


    }
}
