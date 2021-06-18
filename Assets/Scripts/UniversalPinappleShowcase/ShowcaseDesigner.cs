using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseDesigner : MonoBehaviour
{



    public List<ShowcaseDesignerTab> ShowcaseItems;



}


[System.Serializable]
public class ShowcaseDesignerTab
{

    public string Name;
    public string tagLine;
    [TextArea]
    public string description;
    public Texture2D icon;
    public string iosLink;
    public string androidLink;
    public string pcLink;
}