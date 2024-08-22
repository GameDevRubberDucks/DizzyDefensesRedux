/*
 * ExtensionsForEnum.cs
 * 
 * Description:
 * - Adds extensions to the enum base type
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using System;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForEnum
    {
        // Based off https://stackoverflow.com/questions/15388072/how-to-add-extension-methods-to-enums
        // Answer from Basheer Al-Momani
        public static int ToInt<T>(this T enumVal) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Invalid cast to int! <T> must be an enum!");
            }

            return (int)(IConvertible)enumVal;
        }
    }
}