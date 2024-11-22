/*
 * ExtensionsForSerializedProperty.cs
 * 
 * Description:
 * - Adds extensions to the basic SerializedProperty class
 * 
 * Author(s): 
 * - Dan
*/

using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForSerializedProptery
    {
#if UNITY_EDITOR
        public static object GetValueAsRawObject(this SerializedProperty serializedProperty, out System.Type outRuntimeType)
        {
            var propertyType = serializedProperty.propertyType;

            switch (propertyType)
            {
                case SerializedPropertyType.Integer:
                    outRuntimeType = typeof(int);
                    return serializedProperty.intValue;

                case SerializedPropertyType.Boolean:
                    outRuntimeType = typeof(bool);
                    return serializedProperty.boolValue;

                case SerializedPropertyType.Float:
                    outRuntimeType = typeof(float);
                    return serializedProperty.floatValue;

                case SerializedPropertyType.String:
                    outRuntimeType = typeof(string);
                    return serializedProperty.stringValue;

                case SerializedPropertyType.Color:
                    outRuntimeType = typeof(Color);
                    return serializedProperty.colorValue;

                case SerializedPropertyType.ObjectReference:
                    outRuntimeType = typeof(UnityEngine.Object);
                    return serializedProperty.objectReferenceValue;

                //case SerializedPropertyType.LayerMask:
                //    return serializedProperty.layerMaskValue;

                case SerializedPropertyType.Enum:
                    outRuntimeType = typeof(System.Enum);
                    return serializedProperty.enumValueIndex;

                case SerializedPropertyType.Vector2:
                    outRuntimeType = typeof(Vector2);
                    return serializedProperty.vector2Value;

                case SerializedPropertyType.Vector3:
                    outRuntimeType = typeof(Vector3);
                    return serializedProperty.vector3Value;

                case SerializedPropertyType.Vector4:
                    outRuntimeType = typeof(Vector4);
                    return serializedProperty.vector4Value;

                case SerializedPropertyType.Rect:
                    outRuntimeType = typeof(Rect);
                    return serializedProperty.rectValue;

                //case SerializedPropertyType.ArraySize:
                //    return serializedProperty.arraySize;

                //case SerializedPropertyType.Character:
                //    return serializedProperty.characterValue;

                case SerializedPropertyType.AnimationCurve:
                    outRuntimeType = typeof(AnimationCurve);
                    return serializedProperty.animationCurveValue;

                case SerializedPropertyType.Bounds:
                    outRuntimeType = typeof(Bounds);
                    return serializedProperty.boundsValue;

                //case SerializedPropertyType.Gradient:
                //    return serializedProperty.gradientValue;

                case SerializedPropertyType.Quaternion:
                    outRuntimeType = typeof(Quaternion);
                    return serializedProperty.quaternionValue;

                case SerializedPropertyType.ExposedReference:
                    outRuntimeType = serializedProperty.exposedReferenceValue.GetType();
                    return serializedProperty.exposedReferenceValue;

                //case SerializedPropertyType.FixedBufferSize:
                //    return serializedProperty.fixedBufferSize;

                case SerializedPropertyType.Vector2Int:
                    outRuntimeType = typeof(Vector2Int);
                    return serializedProperty.vector2IntValue;

                case SerializedPropertyType.Vector3Int:
                    outRuntimeType = typeof(Vector3Int);
                    return serializedProperty.vector3IntValue;

                case SerializedPropertyType.RectInt:
                    outRuntimeType = typeof(RectInt);
                    return serializedProperty.rectIntValue;

                case SerializedPropertyType.BoundsInt:
                    outRuntimeType = typeof(BoundsInt);
                    return serializedProperty.boundsIntValue;

                //case SerializedPropertyType.ManagedReference:
                //    return serializedProperty.managedReferenceValue;
            }

            outRuntimeType = null;
            return null;
        }

        public static object GetValueAsRawObject(this SerializedProperty serializedProperty)
        {
            return GetValueAsRawObject(serializedProperty, out var type);
        }

        public static bool TryGetValueAsType<T>(this SerializedProperty serializedProperty, out T outValue)
        {
            object rawValue = GetValueAsRawObject(serializedProperty);
            
            outValue = (T)rawValue;
            return outValue != null;
        }
#endif
    }
}
