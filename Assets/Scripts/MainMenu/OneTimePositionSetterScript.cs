using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimePositionSetterScript : MonoBehaviour
{
    public Vector3 positionToSetTo;

    public void SetToPosition()
    {
        transform.position = positionToSetTo;
        GetComponent<RectTransform>().anchoredPosition = positionToSetTo;
    }

}
