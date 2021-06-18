using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum CurrencyType {Ingame,Premium,Real }


public class Currency_Controller : MonoBehaviour
{
    public static Currency_Controller instance;
    public UnityEvent CurrencyDisplayUpdate = new UnityEvent();
    public UnityEvent SelectedStoreItemUpdate = new UnityEvent();
    public UnityEvent PurchaseCompleted = new UnityEvent();

    private PurchaseData _selectedStoreItem = new PurchaseData();
    public PurchaseData SelectedStoreItem
    {
        get { return _selectedStoreItem; }
        set
        {           

            _selectedStoreItem = value;

            if (value.identifier != "")
            {
                SelectedStoreItemUpdate.Invoke();
            }
        }
    }

    public StoreData storeData;
    public string RealCurrencySymbol = "£";

    private float _coins = 250;
    public float Coins
    {
        get { return _coins; }
        set
        {
            print("coins are working");
            _coins = value;
            CurrencyDisplayUpdate.Invoke();
        }
    }
    private float _premiumCoins = 50;
    public float PremiumCoins
    {
        get { return _premiumCoins; }
        set
        {
            _premiumCoins = value;
            CurrencyDisplayUpdate.Invoke();
        }
    }

    public AudioClip purchaseSound;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("two currency controllers exist in this scene");
        }
    }

    private void Awake()
    {
        storeData.GenerateItemDictionary();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrencyDisplayUpdate.Invoke();
    }




    public void DeductPurchaseCost(float cost, CurrencyType currencyType)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySoundEffect(purchaseSound);
        }


        switch (currencyType)
        {
            case CurrencyType.Ingame:
                Coins -= cost;              
                break;


            case CurrencyType.Premium:
                PremiumCoins -= cost;
                break;
        }
    }
}


public struct SelectedStoreItem
{
    public float price;
    public CurrencyType currencyType;
    public string identifier;


    public SelectedStoreItem(string identifier, CurrencyType currencyType, float price)
    {
        this.identifier= identifier;
        this.currencyType = currencyType;
        this.price = price;

    }

    public void Blank()
    {
        this.identifier = "";
        this.currencyType = CurrencyType.Ingame;
        this.price = 0f;
    }



}









[System.Serializable]
///<summary>
/// Purchase data is a struct used to contain information relevant to a purchase without needing to distinguish between sale and regular prices 
///</summary>
public struct PurchaseData
{
    public int quantity;
    public string name;
    public string identifier;
    public CurrencyType currencyType;
    public float totalCost;



    /// <summary> The cost of the object displayed as a string containing the necessary tag data to display the currency icon </summary>
    public string costDisplayString;

    public PurchaseData(int quantity,string identifier,string name, CurrencyType currencyType,float totalCost, string costDisplayString)
    {
        this.name = name;
        this.quantity = quantity;
        this.identifier = identifier;
        this.currencyType = currencyType;
        this.totalCost = totalCost;
        this.costDisplayString = costDisplayString;
    }

    public void Clear()
    {
        this.name = "";
        this.quantity = 0;
        this.identifier = "";
        this.totalCost = 0f;
        this.costDisplayString = "";

    }

}



[System.Serializable]
public class StoreItemData
{
    public string name;
 [Tooltip("The Identifier is used after purchase to determine what action the game needs to perform")]
    public string Identifier;
    public Sprite storeIcon;
    public CurrencyType currencyType;
    public float cost;

    public bool oneTimePurchase = false;
    public bool purchasable = true;

    public StoreItemData(string name, string identifier, Sprite storeIcon, CurrencyType currency, float cost,bool oneTimePurchase, bool purchasable)
    {
        this.name = name;
        this.Identifier = identifier;
        this.storeIcon = storeIcon;
        this.currencyType = currency;
        this.cost = cost;
        this.oneTimePurchase = oneTimePurchase;
        this.purchasable = purchasable;


    }

    public string GetCostDisplay()
    {
        return (currencyType != CurrencyType.Real) ? $"<sprite={(int)currencyType}>" + cost : "{0}" + cost;
    }



    /// <summary>
    /// Items marked as not purchasable will still populate store menus but remain uninteractable 
    /// </summary>
    /// <param name="canPurchase"></param>
    public void SetPuchasable(bool canPurchase)
    {
       
        this.purchasable = canPurchase;
    }

}