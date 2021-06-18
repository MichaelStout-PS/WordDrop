using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Created by James Jordan
/// Created on 24/05/2021
/// 
/// Converts textures to strings and strings to sprites, used in the pineapple showcase 
/// 
/// </summary>
public class ImageConversion : MonoBehaviour
{
    public static string GenerateImageString(Texture2D texture)
    {
        byte[] textByte = texture.EncodeToPNG();
        string imageString = System.Convert.ToBase64String(textByte);


        return imageString;
    }



    /// <summary>
    /// Creates a
    /// </summary>
    /// <param name="TextureString">The string you are converting to texture</param>
    /// <returns></returns>
    public static Sprite GenerateTexture(string TextureString)
    {

        byte[] textByte = System.Convert.FromBase64String(TextureString);
        Texture2D newTexture = new Texture2D(256, 256);
        newTexture.LoadImage(textByte, false);
        newTexture.Apply();
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), 100.0f);


        return newSprite;

    }
}