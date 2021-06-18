using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UIMenus;
using TMPro;

public class SettingsController : UIMenu_Abstract
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _languageDropdown;

    private void OnEnable()
    {
        //StartCoroutine("PopulateLanguageDropDown");
    }



    public override void OpenMenu()
    {        
        base.OpenMenu();
    }









    /// These methods are simple ways to affect the audiomixer from the UI using a slider.
    #region Targeted UI Methods

    #region Master Volume Methods
    public void SetMasterVolume(Slider slider)
    {
        _audioMixer.SetFloat("Master", slider.value);

    }

    public void SetMasterVolume(float value)
    {
        _audioMixer.SetFloat("Master", value);

    }
    #endregion



    #region BGM Volume Methods
    public void SetBGMVolume(Slider slider)
    {
        _audioMixer.SetFloat("BGM", slider.value);

    }

    public void SetBGMVolume(float value)
    {
        _audioMixer.SetFloat("BGM", value);

    }
    #endregion



    #region ES Volume Methods
    public void SetSEVolume(Slider slider)
    {
        _audioMixer.SetFloat("SE", slider.value);

    }

    public void SetBSEVolume(float value)
    {
        _audioMixer.SetFloat("SE", value);

    }
    #endregion

    #endregion
}
