using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StoreConfirmationWindow : MonoBehaviour
{

    public Canvas storeConfirmationCanvas;
    public TMP_Text confirmationWindowText;
    public string confirmationMessage;
    /*
    public void OnEnable()
    {
        Currency_Controller.instance.SelectedStoreItemUpdate.AddListener( delegate { StoreConfirmationCanvas.gameObject.SetActive(true); });

        Currency_Controller.instance.SelectedStoreItemUpdate.Invoke();
    }

    public void OnDisable()
    {
        Currency_Controller.instance.SelectedStoreItemUpdate.RemoveListener(delegate { StoreConfirmationCanvas.gameObject.SetActive(true); });
    }
    */


    public void UpdateConfirmationText()
    {
        var purchaseData = Currency_Controller.instance.SelectedStoreItem;
        confirmationWindowText.text = string.Format(confirmationMessage,purchaseData.name) + purchaseData.costDisplayString;
    }


    /// <summary>
    /// button method to purchase item
    /// </summary>
    public void ConfirmPurchase()
    {
        var purchaseData = Currency_Controller.instance.SelectedStoreItem;
        PurchaseManager.CompletePurchase(purchaseData);

        CloseWindow();
    }


    /// <summary>
    /// button method to close window
    /// </summary>
    public void RejectPurchase()
    {
        Currency_Controller.instance.SelectedStoreItem.Clear();
        CloseWindow();
    }


    public void CloseWindow()
    {
        storeConfirmationCanvas.gameObject.SetActive(false);
    }

    public void OpenWindow()
    {
        storeConfirmationCanvas.gameObject.SetActive(true);
    }

    public void ToggleWindow()
    {
        storeConfirmationCanvas.gameObject.SetActive(!storeConfirmationCanvas.gameObject.activeInHierarchy);
    }
}
