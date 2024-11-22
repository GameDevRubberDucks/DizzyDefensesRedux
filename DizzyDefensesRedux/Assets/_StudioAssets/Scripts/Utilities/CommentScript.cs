/*
 * CommentScript.cs
 * 
 * Description:
 * - Just a simple script that can be used to add a comment as a component to describe something on a GameObject
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities
{
	public class CommentScript : MonoBehaviour
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[TextArea(minLines: 5, maxLines: 10)]
		[SerializeField] private string m_Comment;
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}