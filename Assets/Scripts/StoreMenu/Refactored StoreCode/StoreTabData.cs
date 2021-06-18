using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreTabData", menuName = "StoreObjects/StoreTabData", order = 1)]
public class StoreTabData : ScriptableObject
{
    public List<TabItem> tabItems = new List<TabItem>();







}

[System.Serializable]
public class TabItem
{
    public string identifier = "";
    public bool overridePrice = false;
    public CurrencyType adjustedCurrencyType;
    public float adjustedPrice;




}
