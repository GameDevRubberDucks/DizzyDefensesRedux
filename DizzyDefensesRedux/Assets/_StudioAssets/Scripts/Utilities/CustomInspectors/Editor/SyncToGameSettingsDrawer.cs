/*
 * SyncToGameSettingsDrawer.cs
 * 
 * Description:
 * - Class that controls how variables tagged with the [SyncToGameSettings] attribute display in inspectors
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEditor;

using RubberDucks.Utilities.Extensions;

namespace RubberDucks.Utilities.CustomInspectors
{
	[CustomPropertyDrawer(typeof(SyncToGameSettingsAttribute))]
	public class SyncToGameSettingsDrawer : PropertyDrawer
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//base.OnGUI(position, property, label);

			EditorGUI.BeginProperty(position, label, property);

			Rect fullRect = new Rect(position.x, position.y, position.width, 30.0f);

			SyncToGameSettingsAttribute syncAttrib = attribute as SyncToGameSettingsAttribute;
			
			SerializedObject gameSettingSerialized = new SerializedObject(GameSettings.Instance);
			if (gameSettingSerialized == null)
			{
				EditorGUI.HelpBox(fullRect, "Could not make a SerializedObject from GameSettings.Instance", MessageType.Error);
			}
			else
			{
				SerializedProperty gameSettingsVar = gameSettingSerialized.FindProperty(syncAttrib.m_GameSettingsVariableName);
				
				if (gameSettingsVar != null)
				{
					if (SerializedProperty.DataEquals(gameSettingsVar, property))
					{
						EditorGUI.HelpBox(fullRect, "This property is successfully synced to GameSettings.cs", MessageType.Info);
					}
					else
					{
						//bool test = gameSettingsVar.TryGetValueAsType<float>(out var testFloat);
						var settingsValue = gameSettingsVar.GetValueAsRawObject();
						float settingsValFloat = (float)settingsValue;

						gameSettingsVar.TryGetValueAsType<float>(out float settingsTest);

						//object settingsValue = gameSettingsVar.value
						EditorGUI.HelpBox(fullRect, $"This property's value is desynced from GameSettings.cs. GameSetting's value is {settingsValue.ToString()}", MessageType.Info);
					}
				}
				else
				{
					EditorGUI.HelpBox(fullRect, "Could not find a matching property in GameSettings.cs", MessageType.Warning);
				}
			}

			// Store the attribute

			// Show the label
			var labelRect = new Rect(position.x, position.y, position.width, position.height);
			//EditorGUI.LabelField(labelRect, "This property is synced to the GameSettings.cs");

			// Show the field
			var fieldRect = new Rect(position.x, position.y + 30, position.width, position.height);
			//EditorGUI.PropertyField(fieldRect, property, new GUIContent(property.name));

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 100.0f;
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
