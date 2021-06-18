using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UIMenus
{
    public abstract class UIMenu_Abstract : MonoBehaviour
    {
        [SerializeField] private GameObject _menuObject;

        public virtual void OpenMenu()
        {
            _menuObject.SetActive(true);
        }

        public virtual void CloseMenu()
        {
            _menuObject.SetActive(false);

        }

        // Switches from current state. Uses invoke so that overrides dont have to override toggle to function properly.
        public virtual void ToggleMenu()
        {

         string trigger = (_menuObject.activeInHierarchy) ? "CloseMenu" : "OpenMenu";

            Invoke(trigger,0);


          //  _menuObject.SetActive(!_menuObject.activeInHierarchy);

        }

        // switches to given state without the hastle of calling open/close seperately
        public virtual void ToggleMenu(bool SetState)
        {
            _menuObject.SetActive(SetState);
        }




    }
}