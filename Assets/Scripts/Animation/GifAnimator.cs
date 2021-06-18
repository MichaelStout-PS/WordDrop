using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GifAnimator : MonoBehaviour
{
    public Sprite[] frames;
    Image image;
    public float delayTime = 0.1f;
    private int currentFrame = 0;
    
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();

        StartCoroutine(ChangeFrame());
    }


    IEnumerator ChangeFrame()
    {
        while (true) {
            currentFrame++;
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }
            image.sprite = frames[currentFrame];

            yield return new WaitForSeconds(delayTime);
        }
    }


}
