/*
 * UtilFuncs_TypeFromString.cs
 * 
 * Description:
 * - Partial implementation of the UtilityFunctions class which houses a bunch of useful static methods
 * - Helper functions to convert strings to actual types using reflection
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using System;

namespace RubberDucks.Utilities
{
	public partial class UtilityFunctions : MonoBehaviour
	{
        //--- Properties ---//

        //--- Public Variables ---//

        //--- Protected Variables ---//

        //--- Private Variables ---//

        //--- Unity Methods ---//

        //--- Public Methods ---//
        public static Type GetTypeFromString(string str)
        {
            // Based off this: https://stackoverflow.com/questions/11107536/convert-string-to-type-in-c-sharp
            // Try to get the type from the current assembly
            Type objType = Type.GetType(str);

            // If not found in this assembly, we need go to through others
            if (objType == null)
            {
                // Loop through all of the assemblies
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    // Check the iterated assembly for the type
                    objType = asm.GetType(str);

                    // If the type was found, exit the tloop
                    if (objType != null)
                    {
                        break;
                    }
                }
            }

            // Return the type
            return objType;
        }
    }
}
