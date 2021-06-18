using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuAnimator : MonoBehaviour
{
    public RectTransform menuContainer;
    public CanvasGroup canvasGroup;


    public Sequence openSequence;
    public Sequence closeSequence;
    Sequence s;
    Sequence t;


    public bool openOnEnable = false;

    [Header("Animation settings")]

    //scaling
    public bool enableScale = false;
    public AnimationCurve scaleCurve;
    public Vector3 targetScale = new Vector3(1,1,1);
    public float ScaleDuration = 1f;


    // fading
    public bool enableFade = false;
    public AnimationCurve fadeCurve;
    public float targetOpacity = 1f;
    public float fadeDuration = 1f;

    // positioning
    public bool enablePositioning = false;
    public AnimationCurve positioningCurve;
    public Vector3 targetPosition = new Vector3(1, 1, 1);
    public float positioningDuration = 1f;


    private void OnEnable()
    {
        s = DOTween.Sequence();

        if(enableScale == true)
        {
            s.Append(menuContainer.DOScale(targetScale, ScaleDuration).SetEase(scaleCurve));
        }

        if(enableFade == true && canvasGroup != null)
        {
            s.Append(canvasGroup.DOFade(targetOpacity, fadeDuration).SetEase(fadeCurve));

        }

        if(enablePositioning == true && menuContainer != null)
        {
            s.Append(menuContainer.DOAnchorPos3D(targetPosition, positioningDuration).SetEase(positioningCurve));

        }




        if (openOnEnable == true)
        {           
            s.Play();
        }
    }






    // Start is called before the first frame update
    IEnumerator OpenMenu()
    {
         

        var min = menuContainer.anchorMin;
        var max = menuContainer.anchorMax;
      

     //   s.Append(menuContainer.DOAnchorMin(new Vector2(0,1),0,true));
     //   s.Append(menuContainer.DOAnchorMin(new Vector2(1, 1), 0, true));
     //   menuContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,1);
      //  menuContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1);

        s.Append(menuContainer.DOPivot(new Vector2(0.5f, 0f), 10));

       // yield return new WaitUntil(() => s.IsComplete() == true);

     //   t.Append(menuContainer.DOAnchorMin(min, 0, true));
      //  t.Append(menuContainer.DOAnchorMin(max, 0, true));



        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            s.Restart();

        }
    }

}
