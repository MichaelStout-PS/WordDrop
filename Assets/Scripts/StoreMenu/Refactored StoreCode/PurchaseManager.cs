using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class PurchaseManager : MonoBehaviour
{
   



    /// <summary>
    /// Checks if the purchase is affordable to the player if it uses ingame currency
    /// </summary>
    /// <param name="purchaseData"></param>
    public static void ProcessPurchase(PurchaseData purchaseData)
    {

        // requires a purchase outside of the app. Unsure how to fill in as of yet
        if(purchaseData.currencyType == CurrencyType.Real)
        {
            Debug.LogWarning("This purchase requires real world money, this method does not yet perform the needed actions");

        }



        // uses ingame currency
        else
        {

            if (PriceCheck(purchaseData.totalCost, purchaseData.currencyType) == true)
            {
              
                //changing value invokes the confirmation message display at the currency controller
                Currency_Controller.instance.SelectedStoreItem = purchaseData;

              //  CompletePurchase(purchaseData);
            }
        }
     






    }



    /// <summary>
    /// This method performs the necessary action for an item purchase using its identifier only. If it cannot find the identifier listed the purchase is rejected
    /// </summary>
    /// <param name="purchaseData"></param>
    public static void CompletePurchase(PurchaseData purchaseData)
    {

        bool purchaseViable = true;


        
        switch (purchaseData.identifier)
        {
            default:
                Debug.Log($"The '{purchaseData.identifier}' Identifier does not correspond to any case given for purchase processing. Please check spelling given in the PurchaseManager");
                purchaseViable = false;
                break;


            case "F_100C":
                Debug.Log($" purchasing 100 coins for {purchaseData.totalCost}");
                Currency_Controller.instance.Coins += 100;
                break;

            case "C_goatee":
                Debug.Log($" purchasing goatee for {purchaseData.totalCost}");
                
                break;

            case "C_moustache":
                Debug.Log($" purchasing moustache for {purchaseData.totalCost}");

                break;

        }

        if(purchaseViable == true)
        {
            var storeData = Currency_Controller.instance.storeData;
            storeData.PuchasableCheck(purchaseData.identifier);


            /*
            //Is this a one time purchasable item?
            var storeData = Currency_Controller.instance.storeData;
            int Id = storeData.RetrieveItemID(purchaseData.identifier);
            bool oneTimePurchase = storeData.ItemData[Id].oneTimePurchase;

            if (oneTimePurchase == true)
            {
                storeData.SetPurchasable(purchaseData.identifier, false);
            }
            */

            Currency_Controller.instance.DeductPurchaseCost(purchaseData.totalCost,purchaseData.currencyType);
            print("purchase completed");
            Currency_Controller.instance.PurchaseCompleted.Invoke();
        }


    }




    /// <summary>
    /// Returns true if the item is affordable in its given currency, This method is not required for purchases made using real money.
    /// </summary>
    /// <param name="Identifier"></param>
    /// <param name="currencyType"></param>
    /// <returns></returns>
    public static bool PriceCheck(float price, CurrencyType currencyType)
    {

        var targetCurrency = 0f;

        switch (currencyType)
        {


            case CurrencyType.Ingame:
                targetCurrency = Currency_Controller.instance.Coins;
                break;

            case CurrencyType.Premium:
                targetCurrency = Currency_Controller.instance.PremiumCoins;
                break;


        }

        bool canAfford = (price <= targetCurrency) ? true : false;

        return canAfford;
    }



    // public void 




}
