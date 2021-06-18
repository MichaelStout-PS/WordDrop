using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
/// <summary>
/// created by: James Jordan
/// Created on: 12/04/2021
/// 
/// Text animation component to be used with a TextAnimationController. Types each character out individually while allowing for custom formatting through tags
/// </summary>
public class TextBehaviour_Type : TextBehaviour
{

    public float CharacterScaleMultiplier = 1;
    public string CurrentCharacterFormat = "<size=110%>";
    public string completeAnimationFormat = "<size=110%>";
    public override void PlayAnimation(TextMeshProUGUI textObject,string newText)
    {

        StopAllCoroutines();
        StartCoroutine(TypeText(textObject, newText));

    }



    public override void StopAnimation(TextMeshProUGUI textObject)
    {     
        StopCoroutine("TypeText");

    }



    private IEnumerator TypeText(TextMeshProUGUI textObject,string DisplayText)
    { 
        IsAnimating = true;

        //padding is added to end of string to keep size consistent when using best fit
        string padding =  new string(' ', DisplayText.Length);
        var DisplayTextArray = DisplayText.ToCharArray();
        bool checkingTag = false;


        for (int i = 1; i < DisplayText.Length; i++)
        {      
            
            //check for tag
            if(DisplayTextArray[i] == '<')
            {
                checkingTag = true;
                print("found start of tag = " + i + " Character is " + DisplayTextArray[i]);
                int NewIndex = DetectTagEnd(DisplayTextArray, i);
              int tagSize = NewIndex - i;
                print("tagsize = " + tagSize);
                padding.Remove(padding.Length - tagSize);

                i = NewIndex;

                print("new index = " + i);
                checkingTag = false;
                
            }
            yield return new WaitUntil(() => checkingTag == false);
            



            string AnimationText = DisplayText.Substring(0, i);

          //  AnimationText = Regex.Replace(AnimationText, "<.*?>", string.Empty);
            // Inserts character formatter
            AnimationText = AnimationText.Insert(AnimationText.Length-1, CurrentCharacterFormat);
           
            //Add padding and decrease padding length for next loop
            AnimationText += padding;
            padding.Remove(padding.Length -1);

            //set text
           textObject.text = AnimationText;
            
            yield return new WaitForSecondsRealtime(0.02f);
        }

        // Display completed string with complete format tags applied
        string CompleteText = DisplayText.Insert(0, completeAnimationFormat);
        textObject.text = CompleteText;


        IsAnimating = false;


        if(LoopAnimation == true)
        {
            StartCoroutine(TypeText(textObject, DisplayText));
        }
        yield return null;
    }



    private int DetectTagEnd(char[] charArray,int currentIndex)
    {
        int tagEndIndex = currentIndex;
        for (int i = currentIndex; i < charArray.Length; i++)
        {
            if(charArray[i] == '>')
            {
               // Debug.Break();
                tagEndIndex = i + 1;
                break;
            }
        }



            return tagEndIndex;
    }
}
