/*
 * ExtensionsForFloat.cs
 * 
 * Description:
 * - Adds extensions to the float base type
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForFloat
    {
        public static float RoundAndClipToDecimalPlace(this float targetFloat, int numDecimalPlaces)
        {
            // TODO: Figure out a better math-based way to do this instead of just converting to strings and back
            string floatStrNDecimals = targetFloat.ToString($"F{numDecimalPlaces}");
            return float.Parse(floatStrNDecimals);
        }

        public static float PreventNegative(this float targetFloat)
        {
            return (targetFloat < 0.0f) ? 0.0f : targetFloat;
        }

        public static bool IsInRange(this float targetFloat, float min, float max)
        {
            return targetFloat >= min && targetFloat <= max;
        }

        public static bool IsInRange(this float targetFloat, Vector2 range)
        {
            return IsInRange(targetFloat, range.x, range.y);
        }
    }
}