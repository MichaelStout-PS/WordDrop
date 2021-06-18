using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StoreTabController : MonoBehaviour
{
    [SerializeField]private GameObject _storeObjectButton;

    [SerializeField] private StoreData _storeData;
    [SerializeField] private StoreTabData _tabData;
    [SerializeField] private Transform _tabContents;



    private void OnEnable()
    {
        if (_storeData != null && _storeData != null && Currency_Controller.instance != null)
        {
            PopulateTab();
            Currency_Controller.instance.PurchaseCompleted.AddListener(RefreshTab);
        }
    }

    private void OnDisable()
    {
        ClearTab();
        Currency_Controller.instance.PurchaseCompleted.RemoveListener(RefreshTab);
    }

    public void ClearTab()
    {
        foreach (Transform child in _tabContents.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void PopulateTab()
    {
        ClearTab();

        for (int i = 0; i < _tabData.tabItems.Count; i++)
        {
            var tabItem = _tabData.tabItems[i];
            int itemID = _storeData.RetrieveItemID(tabItem.identifier);


            // RetrieveItemID returns as -1 if it cannot find the appropriate value.
            if(itemID != -1)
            {
                var item = _storeData.ItemData[itemID];

                Button newButton = Instantiate(_storeObjectButton, _tabContents, true).GetComponent<Button>(); 
                print(i);

                //button setup 
                newButton.image.sprite = item.storeIcon;
                var buttonText = newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                newButton.transform.localScale = Vector3.one;

                newButton.name = item.Identifier;

                // quantity is not used in the examples but is still used in processing to make its addition simpler           
                int quantity = 1;

                if(item.purchasable == false)
                {
                    newButton.interactable = false;
                }
                

                PurchaseData purchaseData;
                // not overriding the price
                if (tabItem.overridePrice == false)
                {
                    // generate the cost string and format it to use the real currency symbol.
                    string costString = item.GetCostDisplay();
                    string.Format(costString, Currency_Controller.instance.RealCurrencySymbol);
                    

                    buttonText.text = costString;



                    purchaseData = new PurchaseData(quantity, item.Identifier, item.name, item.currencyType, item.cost * quantity, costString);

                    // destroy the 'sale' banner at the top of the button, she wont be needed
                    Destroy(newButton.transform.GetChild(1).gameObject);
                }


                // using the override price
                else
                {
                    // format string to use override prices
                    string costString = (tabItem.adjustedCurrencyType != CurrencyType.Real) ? $"<sprite={(int)tabItem.adjustedCurrencyType}>" + tabItem.adjustedPrice : "{0}" + tabItem.adjustedPrice;
                    string.Format(costString, Currency_Controller.instance.RealCurrencySymbol);

                    buttonText.text = costString;

                    purchaseData = new PurchaseData(quantity, item.Identifier, item.name, tabItem.adjustedCurrencyType, tabItem.adjustedPrice * quantity, costString);
                }


                newButton.onClick.AddListener(delegate { PurchaseManager.ProcessPurchase(purchaseData); });


            }



        }

        }



    /// <summary>
    /// will cycle through all loaded items in the tab and disable them if no longer purchasable
    /// </summary>
    public void RefreshTab()
    {
        Debug.Log("tab refreshed");
        foreach (Transform child in _tabContents)
        {
          bool isInteractable = _storeData.RetrieveItemData(child.gameObject.name).purchasable;
            child.gameObject.GetComponent<Button>().interactable = isInteractable;
            
        }
    }

}
