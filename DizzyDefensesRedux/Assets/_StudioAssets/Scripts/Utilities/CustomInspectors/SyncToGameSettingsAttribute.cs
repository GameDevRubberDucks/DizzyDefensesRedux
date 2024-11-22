/*
 * SyncToGameSettingsAttribute.cs
 * 
 * Description:
 * - Attribute that can be added to a variable to make it sync with the GameSettings class
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.CustomInspectors
{
	public class SyncToGameSettingsAttribute : PropertyAttribute
	{
		//--- Properties ---//

		//--- Public Variables ---//
		public string m_GameSettingsVariableName;
		
		//--- Protected Variables ---//
		
		//--- Private Variables ---//
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		public SyncToGameSettingsAttribute(string gameSettingsVariableName)
		{
			m_GameSettingsVariableName = gameSettingsVariableName;
		}
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
