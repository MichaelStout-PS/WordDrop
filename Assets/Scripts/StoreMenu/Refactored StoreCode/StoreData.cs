using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreData", menuName = "StoreObjects/StoreData", order = 1)]
public class StoreData : ScriptableObject
{


    public List<StoreItemData> ItemData;
    public Dictionary<string, int> ItemDictionary = new Dictionary<string, int>();



    /// <summary>
    /// Generates a dictionary to allow for the recovery of Item IDs which could be subject to change over time.
    /// </summary>
    public void GenerateItemDictionary()
    {
        ItemDictionary.Clear();
        for(int i = 0; i < ItemData.Count; i++)
        {
            ItemDictionary.Add(ItemData[i].Identifier, i);

        }
    }


    public int RetrieveItemID(string ItemName)
    {
        int ReturnID = -1;
        ItemDictionary.TryGetValue(ItemName, out ReturnID);

        return ReturnID;
    }

    public StoreItemData RetrieveItemData(int ItemID)
    {

        return ItemData[ItemID];
    }

    public StoreItemData RetrieveItemData(string Identifier)
    {
        int itemId = RetrieveItemID(Identifier);

        if (itemId == -1)
        {
           return ItemData[0];
        }

           return ItemData[itemId];
        
    }



    public void SetPurchasable(string Identifier, bool isPurchasable)
    {
        int itemId = RetrieveItemID(Identifier);

        if(itemId != -1)
        {
            ItemData[itemId].purchasable = isPurchasable;
        }
    }

    public void SetPurchasable(int itemId, bool isPurchasable)
    {

        if (itemId != -1)
        {
            ItemData[itemId].purchasable = isPurchasable;
        }
    }

    /// <summary>
    /// performs a check on the item and disables it if it is a one time purchase
    /// </summary>
    public void PuchasableCheck(string Identifier)
    {
        int itemId = RetrieveItemID(Identifier);

        if (itemId != -1 && ItemData[itemId].oneTimePurchase == true)
        {
            
            ItemData[itemId].purchasable = false;
        }
    }




}
