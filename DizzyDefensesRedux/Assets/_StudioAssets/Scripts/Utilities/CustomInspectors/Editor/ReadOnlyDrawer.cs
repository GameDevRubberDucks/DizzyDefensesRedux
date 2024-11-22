/*
 * ReadOnlyDrawer.cs
 * 
 * Description:
 * - Class that controls how variables tagged with [ReadOnly] display
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEditor;

using RubberDucks.Utilities.Extensions;

namespace RubberDucks.Utilities.CustomInspectors
{
	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyDrawer : PropertyDrawer
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//

		//--- Unity Methods ---//
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			ReadOnlyAttribute attribRef = attribute as ReadOnlyAttribute;
			bool isLocked = true;

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
						bool editWhenControlIsTrue = attribRef.m_IsEditableWhenControlBoolIsTrue;
						isLocked = (editWhenControlIsTrue) ? !controlBoolState : controlBoolState;
					}
					else
					{
						EditorGUI.HelpBox(position, $"Could not convert the value of property '{attribRef.m_ControlBoolName}' to a bool", MessageType.Warning);
					}
				}
			}
			
			if (isLocked)
			{
				GUI.enabled = false;
				EditorGUI.PropertyField(position, property, label, true);
				GUI.enabled = true;
			}
			else
			{
				EditorGUI.PropertyField(position, property, label, true);
			}
		}

		//--- Public Methods ---//
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// use the default property height, which takes into account the expanded state
			return EditorGUI.GetPropertyHeight(property);
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
