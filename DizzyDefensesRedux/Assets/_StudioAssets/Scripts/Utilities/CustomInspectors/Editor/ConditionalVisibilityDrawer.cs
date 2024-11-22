/*
 * ConditionalVisibilityDrawer.cs
 * 
 * Description:
 * - Class that controls how variables tagged with [ConditionalVisibility] display
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEditor;

using RubberDucks.Utilities.Extensions;

namespace RubberDucks.Utilities.CustomInspectors
{
	[CustomPropertyDrawer(typeof(ConditionalVisibilityAttribute))]
	public class ConditionalVisibilityDrawer : PropertyDrawer
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		private bool isVisible = true;

		//--- Unity Methods ---//
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			ConditionalVisibilityAttribute attribRef = attribute as ConditionalVisibilityAttribute;

			if (!string.IsNullOrEmpty(attribRef.m_ControlBoolName))
			{
				SerializedProperty controlBoolProperty = property.serializedObject.FindProperty(attribRef.m_ControlBoolName);

				if (controlBoolProperty == null)
				{
					EditorGUI.HelpBox(position, $"Cannot find property '{attribRef.m_ControlBoolName}'", MessageType.Error);
					return;
				}
				else if (controlBoolProperty.propertyType != SerializedPropertyType.Boolean)
				{
					EditorGUI.HelpBox(position, $"Property '{attribRef.m_ControlBoolName}' is not of type 'bool'", MessageType.Warning);
					return;
				}
				else
				{
					if (controlBoolProperty.TryGetValueAsType<bool>(out var controlBoolState))
					{
						bool visibileWhenControlIsTrue = attribRef.m_IsVisibleWhenControlBoolIsTrue;
						isVisible = (visibileWhenControlIsTrue) ? controlBoolState : !controlBoolState;
					}
					else
					{
						EditorGUI.HelpBox(position, $"Could not convert the value of property '{attribRef.m_ControlBoolName}' to a bool", MessageType.Warning);
					}
				}
			}

			if (isVisible)
			{
				EditorGUI.PropertyField(position, property, label, true);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (isVisible)
			{
				return EditorGUI.GetPropertyHeight(property);
			}
			else
			{
				return 0.0f;
			}
		}

		//--- Public Methods ---//

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
