/*
 * InputManager.cs
 * 
 * Description:
 * - Handles various inputs and converts them to events that can be used in multiple places
 * - This is a partial class, so it is meant to be expanded for each game that uses it
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using System.Collections.Generic;

namespace RubberDucks.Utilities.RDInput
{
	public partial class InputManager : Singleton<InputManager>
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//

		//--- Unity Methods ---//

		//--- Public Methods ---//

		//--- Protected Methods ---//

		//--- Private Methods ---//
		private bool CheckAnyKeys(List<KeyCode> keys, bool checkingForIsKeyDown = true)
		{
			return CheckAnyKeys(keys, out var key, checkingForIsKeyDown);
		}

		private bool CheckAnyKeys(List<KeyCode> keys, out KeyCode outFirstKeyChecked, bool checkingForIsKeyDown = true)
		{
			foreach (var key in keys)
			{
				if ((checkingForIsKeyDown && Input.GetKeyDown(key)) || (!checkingForIsKeyDown && Input.GetKeyUp(key)))
				{
					outFirstKeyChecked = key;
					return true;
				}
			}

			outFirstKeyChecked = default;
			return false;
		}
	}
}
