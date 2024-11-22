/*
 * AudioBackgroundMusic.cs
 * 
 * Description:
 * - Simple utility to play some background music and automatically stop playing it
 * 
 * Author(s): 
 * - Kody
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Audio
{
	public class AudioBackgroundMusic : MonoBehaviour
	{
        //--- Properties ---//

        //--- Public Variables ---//

        //--- Protected Variables ---//

        //--- Private Variables ---//
        [SerializeField] private AudioConstant m_MusicConstant = AudioConstant.None;
        [SerializeField] private string m_LoopingTag = "BackgroundMusic";
        [SerializeField] private float m_MusicVolume = 1.0f;
		
		private bool m_IsActive = false;

        //--- Unity Methods ---//
        private void Start()
        {
			 m_IsActive = AudioManager.Instance.PlayLoopingAudio(m_LoopingTag, m_MusicConstant, AudioChannel.Music, m_MusicVolume);
        }

        private void OnDestroy()
        {
            if (m_IsActive)
            {
                AudioManager.Instance.StopLoopingAudio(m_LoopingTag);
            }
        }

        //--- Public Methods ---//

        //--- Protected Methods ---//

        //--- Private Methods ---//
    }
}
