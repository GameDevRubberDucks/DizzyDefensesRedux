/*
 * SceneLoader.cs
 * 
 * Description:
 * - Simple utility that loads a scene by name or index, or alternatively reloads the active scene
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.SceneManagement;

namespace RubberDucks.Utilities
{
	public class SceneLoader : MonoBehaviour
	{
        //--- Properties ---//

        //--- Public Variables ---//

        //--- Protected Variables ---//

        //--- Private Variables ---//

        //--- Unity Methods ---//

        //--- Public Methods ---//
        public void LoadSceneByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneByIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadNextSceneByIndex()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            LoadSceneByIndex(currentIndex + 1);
        }

        public void ReloadCurrentScene()
        {
            LoadSceneByName(SceneManager.GetActiveScene().name);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        //--- Protected Methods ---//

        //--- Private Methods ---//
    }
}
