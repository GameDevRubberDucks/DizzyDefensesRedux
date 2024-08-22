/*
 * ConditionalVisibilityAttribute.cs
 * 
 * Description:
 * - An attribute that can be added to a property to make it hide or unhide depending on the condition
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.CustomInspectors
{
	public class ConditionalVisibilityAttribute : PropertyAttribute
	{
		//--- Properties ---//

		//--- Public Variables ---//
		public string m_ControlBoolName = default;
		public bool m_IsVisibleWhenControlBoolIsTrue = false;
		
		//--- Protected Variables ---//
		
		//--- Private Variables ---//
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		public ConditionalVisibilityAttribute()
		{
			m_ControlBoolName = null;
			m_IsVisibleWhenControlBoolIsTrue = false;
		}

		public ConditionalVisibilityAttribute(string controlBoolName, bool isVisibileWhenControlBoolIsTrue = true)
		{
			m_ControlBoolName = controlBoolName;
			m_IsVisibleWhenControlBoolIsTrue = isVisibileWhenControlBoolIsTrue;
		}
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
