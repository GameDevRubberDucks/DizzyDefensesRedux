/*
 * GameSettings.cs
 * 
 * Description:
 * - Base class for all games to inherit their settings from
 * - Allows for a single centralized method of changing all of the settings in a game
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using RubberDucks.Utilities;

namespace RubberDucks.Utilities
{
	public class GameSettings : Singleton<GameSettings>
	{
		//--- Properties ---//

		//--- Public Variables ---//
		public float m_GridSize = 10.0f;
		
		//--- Protected Variables ---//
		
		//--- Private Variables ---//
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
