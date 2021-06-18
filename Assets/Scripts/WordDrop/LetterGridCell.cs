using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterGridCell : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    static GridManager gridManager;

    public GameObject letterObject;

    public char letter;

    public bool selected = false;

    private void Start()
    {
        if (gridManager == null)
        {
            gridManager = GameObject.Find("Grid").GetComponent<GridManager>();
        }

    }


    public void KillLetterObject()
    {
        letterObject.GetComponent<UITweener>().Out(true);
        Destroy(letterObject, 2);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        CheckClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            CheckClick();
        }
    }

    void CheckClick()
    {
        if (selected)
        {
            return;
        }
        if (gridManager.CheckCellClick(transform)) {
            selected = true;
            GetComponent<MaskableGraphic>().color = Color.yellow * new Color(1, 1, 1, 0.2f);
        }
    }
}
