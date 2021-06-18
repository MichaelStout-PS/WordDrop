using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _textDisplay;
    [SerializeField] private CurrencyType _currencyTarget;

    public CurrencyType CurrencyTarget
    {
        get { return _currencyTarget; }
        set
        {
            _currencyTarget = value;
            SetValueTag();
        }

    }
    private string _currencyIconTag = "<sprite=0>";

    private void OnEnable()
    {
        SetValueTag();
       Currency_Controller.instance.CurrencyDisplayUpdate.AddListener(delegate { UpdateDisplay(); });
        UpdateDisplay();

    }


    ///<summary>
    /// This method generates the tag needed to display the sprite coresponding to the _currencyTarget
    ///</summary>
    private void SetValueTag()
    {
        _currencyIconTag = string.Format("<sprite={0}>", (int)_currencyTarget);
    }





    public void UpdateDisplay()
    {
        print("display update called");
        float displayValue = 0;

        // retrieve display value
        switch (_currencyTarget)
        {

            case CurrencyType.Ingame:
                displayValue = Currency_Controller.instance.Coins;
                break;

            case CurrencyType.Premium:
                displayValue = Currency_Controller.instance.PremiumCoins;
                break;

            default:

                break;
        }

        // set display
        _textDisplay.text = " " + _currencyIconTag + displayValue;
    }


    private void OnDisable()
    {
        Currency_Controller.instance.CurrencyDisplayUpdate.RemoveListener(delegate { UpdateDisplay(); });
    }
}
