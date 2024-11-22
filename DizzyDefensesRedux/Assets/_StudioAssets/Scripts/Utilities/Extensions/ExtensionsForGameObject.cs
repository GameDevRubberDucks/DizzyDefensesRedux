/*
 * ExtensionsForGameObject.cs
 * 
 * Description:
 * - Adds extensions to the basic GameObject class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForGameObject
    {
        public static void DestroyChildren(this GameObject gameObject)
        {
            UtilityFunctions.DestroyChildren(gameObject);
        }

        // Try to grab a component off of this object
        // If it doesn't have the component, try to get it from the parent instead
        // If it still hasn't found the component, try from the children instead
        // Return null if still hasn't found the component
        public static T GetComponentFromAnySource<T>(this GameObject gameObject)
        {
            T component = gameObject.GetComponent<T>();

            if (component == null)
            {
                component = gameObject.GetComponentInParent<T>();

                if (component == null)
                {
                    component = gameObject.GetComponentInChildren<T>();
                }
            }

            return component;
        }

        public static bool TryGetComponentFromAnySource<T>(this GameObject gameObject, out T outComponent)
        {
            outComponent = gameObject.GetComponentFromAnySource<T>();
            return (outComponent != null);
        }
    }
}