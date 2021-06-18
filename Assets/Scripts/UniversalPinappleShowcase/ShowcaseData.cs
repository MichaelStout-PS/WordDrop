using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "ShowcaseData", menuName = "Showcase/ShowcaseData", order = 1)]
public class ShowcaseData : ScriptableObject
{
    public List<ShowcaseItem> showcaseItems;


}







[Serializable]
public class ShowcaseSaveData
{
    public List<ShowcaseItem> showcaseItems = new List<ShowcaseItem>();
}



[Serializable]
public class ShowcaseItem
{
    public string name;
    public string tagLine;
    public string description;
    public string icon;
    public string iosLink;
    public string androidLink;
    public string pcLink;
 
    
    //Generates Item from showcase tab - used by designer
    public ShowcaseItem (ShowcaseDesignerTab tab)
    {
        this.name = tab.Name;
        this.tagLine = tab.tagLine;
        this.description = tab.description;
        this.icon = ImageConversion.GenerateImageString(tab.icon);
        this.iosLink = tab.iosLink;
        this.androidLink = tab.androidLink;
        this.pcLink = tab.pcLink;

    }
}
