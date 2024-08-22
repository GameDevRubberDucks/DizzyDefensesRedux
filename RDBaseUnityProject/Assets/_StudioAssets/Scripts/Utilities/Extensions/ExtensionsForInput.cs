/*
 * ExtensionsForGameObject.cs
 * 
 * Description:
 * - Adds extensions to the basic Input class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForInput
    {
        //--- Fake Extensions (Not really extensions since static extensions are impossible) ---//
        public static Vector2 Input_NormalizedMousePosition()
        {
            Vector2 pixelMousePos = Input.mousePosition;

            float mouseRatioX = pixelMousePos.x / Screen.width;
            float mouseRatioY = pixelMousePos.y / Screen.height;

            float mouseRatioHalfRangeX = mouseRatioX - 0.5f;
            float mouseRatioHalfRangeY = mouseRatioY - 0.5f;

            return new Vector2(mouseRatioHalfRangeX * 2.0f, mouseRatioHalfRangeY * 2.0f);
        }
    }
}