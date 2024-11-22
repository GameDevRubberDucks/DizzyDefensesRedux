/*
 * UtilFuncs_DestroyAllChildren.cs
 * 
 * Description:
 * - Partial implementation of the UtilityFunctions class which houses a bunch of useful static methods
 * - Helper functions to destroy all of the children for a GameObject or Transform
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

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
        public static void DestroyChildren(Transform transform)
        {
            bool destroyImmediate = Application.isEditor;

            // Destroy the normal method or destroy immediate (immediate is usually used in editor)
            if (destroyImmediate)
            {
                // Immediate destruction removes the child from the list of children right away
                // So, we need to keep looping until the list is empty
                // Otherwise, we'll only remove half of the children since the childCount variable will keep decreasing
                while (transform.childCount > 0)
                    DestroyChild(transform, 0, true);
            }
            else
            {
                // Normal destruction does NOT remove the child from the list right away
                // So, we need to iterate through the list fully
                // Waiting until the list is empty will result in an infinite loop
                for (int i = 0; i < transform.childCount; i++)
                    DestroyChild(transform, i, false);
            }
        }

        public static void DestroyChildren(GameObject gameObject)
        {
            DestroyChildren(gameObject.transform);
        }

        //--- Protected Methods ---//

        //--- Private Methods ---//
        private static void DestroyChild(Transform _parent, int _childIndex, bool _destroyImmediate)
        {
            GameObject child = _parent.GetChild(_childIndex).gameObject;

            // Use the correct destruction method, either the immediate version or the default
            if (_destroyImmediate)
                DestroyImmediate(child);
            else
                Destroy(child);
        }
    }
}
