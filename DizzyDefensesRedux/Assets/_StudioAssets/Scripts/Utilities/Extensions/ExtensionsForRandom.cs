/*
 * ExtensionsForRandom.cs
 * 
 * Description:
 * - Adds extensions to the basic Random class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForRandom
    {
        //--- Fake Extensions (Not really extensions since static extensions are impossible) ---//
        public static bool Random_Bool()
        {
            return Random.value >= 0.5f;
        }
    }
}