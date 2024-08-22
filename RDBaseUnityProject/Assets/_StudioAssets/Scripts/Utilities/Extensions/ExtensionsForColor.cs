/*
 * ExtensionsForColor.cs
 * 
 * Description:
 * - Adds extensions to Unity's basic Color class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForColor
    {
		public static Color TransferRGB(this Color aValue, Color colorToTakeRGBFrom)
        {
            return new Color(colorToTakeRGBFrom.r, colorToTakeRGBFrom.g, colorToTakeRGBFrom.b, aValue.a);
        }
		
        public static Color TransferAlpha(this Color rgbValues, Color colorToTakeAlphaFrom)
        {
            return new Color(rgbValues.r, rgbValues.g, rgbValues.b, colorToTakeAlphaFrom.a);
        }
		
		public static Color CopyWithNewAlpha(this Color rgbValues, float newAlpha)
        {
            return new Color(rgbValues.r, rgbValues.g, rgbValues.b, newAlpha);
        }        
    }
}