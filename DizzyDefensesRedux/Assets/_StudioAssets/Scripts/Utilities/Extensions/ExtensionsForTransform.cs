/*
 * ExtensionsForTransform.cs
 * 
 * Description:
 * - Adds extensions to the basic Transform class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForTransform
    {
        public static void DestroyChildren(this Transform transform)
        {
            UtilityFunctions.DestroyChildren(transform);
        }

        public static void ParentAndAlign(this Transform transform, Transform newParent, bool alignPosition = true, bool alignRotation = true, bool alignScale = true)
        {
            transform.parent = newParent;

            if (alignPosition)  transform.localPosition = Vector3.zero;
            if (alignRotation)  transform.localRotation = Quaternion.identity;
            if (alignScale)     transform.localScale    = Vector3.one;
        }
		
		public static void ResetLocalValues(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static T GetComponentFromAnySource<T>(this Transform transform)
        {
            T component = transform.GetComponent<T>();

            if (component == null)
            {
                component = transform.GetComponentInParent<T>();

                if (component == null)
                {
                    component = transform.GetComponentInChildren<T>();
                }
            }

            return component;
        }

        public static bool TryGetComponentFromAnySource<T>(this Transform transform, out T outComponent)
        {
            outComponent = transform.GetComponentFromAnySource<T>();
            return (outComponent != null);
        }

        #region DoTween Extensions
        public static TweenerCore<Vector3, Vector3, VectorOptions> DoMoveWithSpeed(this Transform transform, Vector3 destination, float speed, Vector3? startLocationOverride = null, bool snapping = false)
        {
            Vector3 startPosition = (startLocationOverride.HasValue) ? startLocationOverride.Value : transform.position;

            float distance = Vector3.Magnitude(destination - startPosition);
            float duration = distance / speed;

            return transform.DOMove(destination, duration, snapping);
        }
#endregion
    }
}
