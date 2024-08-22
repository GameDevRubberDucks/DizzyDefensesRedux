/*
 * ExtensionsForInt.cs
 * 
 * Description:
 * - Adds extensions to the int base type
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForInt
    {
        public static int PreventNegative(this int targetInt)
        {
            return (targetInt < 0) ? 0 : targetInt;
        }
    }
}