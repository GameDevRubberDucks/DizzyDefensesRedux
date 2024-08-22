/*
 * DebugLogScript.cs
 * 
 * Description:
 * - Simple script that wraps a debug log
 * - Mainly intended so it can be used to test events
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.RDDebug
{
	public class DebugLogScript : MonoBehaviour
	{
		//--- Properties ---//
		
		//--- Public Variables ---//
		
		//--- Protected Variables ---//
		
		//--- Private Variables ---//
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		public void Log(string messageToLog)
		{
			Debug.Log(messageToLog);
		}

		public void LogWarning(string messageToLog)
		{
			Debug.LogWarning(messageToLog);
		}

		public void LogError(string messageToLog)
		{
			Debug.LogError(messageToLog);
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
