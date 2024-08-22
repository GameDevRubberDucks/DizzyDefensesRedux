/*
 * AudioManager.cs
 * 
 * Description:
 * - Generic AudioManager that will be used across most of our projects
 * 
 * Author(s): 
 * - Dan
*/

using System;
using System.Collections.Generic;

using UnityEngine;

namespace RubberDucks.Utilities.Audio
{
	public class AudioManager : PersistentSingleton<AudioManager>
	{
		//--- Constants ---//
		private const string SFX_VOLUME_SAVE_KEY = "SAVED_SFX_VOLUME";
		private const string MUSIC_VOLUME_SAVE_KEY = "SAVED_MUSIC_VOLUME";
		
		//--- Helper Structures ---//
		public class RuntimeAudioSource
		{
			public RuntimeAudioSource(AudioSource source, float baseVolume, AudioChannel channel)
			{
				m_Source = source;
				m_BaseVolume = baseVolume;
				m_Channel = channel;
			}

			public AudioSource m_Source = default;
			public float m_BaseVolume = default;
			public AudioChannel m_Channel = AudioChannel.SFX;
		}

		//--- Properties ---//
		public float MasterVolume 
		{ 
			get => m_MasterVolume;
			set
			{
				float previousMasterVolume = MasterVolume;
				m_MasterVolume = value;

				ApplyVolumeToSources();
			}
		}

		public float SFXVolume
		{
			get => m_SFXVolume;
			set
			{
				float previousSFXVolume = SFXVolume;
				m_SFXVolume = value;

				ApplyVolumeToSources();
			}
		}

		public float MusicVolume
		{
			get => m_MusicVolume;
			set
			{
				float previousMusicVolume = MusicVolume;
				m_MusicVolume = value;

				ApplyVolumeToSources();
			}
		}

		public bool IsSFXMuted 
		{ 
			get => m_IsSFXMuted;
			set
			{
				m_IsSFXMuted = value;

				ApplyMutingToSources();
			}
		}

		public bool IsMusicMuted
		{
			get => m_IsMusicMuted;
			set
			{
				m_IsMusicMuted = value;

				ApplyMutingToSources();
			}
		}

		public List<AudioClip> CopyOfAudioClips => new List<AudioClip>(m_AudioClips);

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[Header("Initialization Controls")]
		[SerializeField] private int m_InitialAudioSourceCount = 10;
		[SerializeField] private AudioClip[] m_AudioClips = default;

		[Header("Runtime Controls")]
		[Range(0.0f, 1.0f)][SerializeField] private float m_MasterVolume = 1.0f;
		[Range(0.0f, 1.0f)][SerializeField] private float m_SFXVolume = 1.0f;
		[Range(0.0f, 1.0f)][SerializeField] private float m_MusicVolume = 1.0f;
		[SerializeField] private bool m_IsSFXMuted = false;
		[SerializeField] private bool m_IsMusicMuted = false;

		private List<RuntimeAudioSource> m_RuntimeAudioSources = new List<RuntimeAudioSource>();
		private Dictionary<string, RuntimeAudioSource> m_TaggedAudioSources = new Dictionary<string, RuntimeAudioSource>();
		private Dictionary<AudioConstant, AudioClip> m_CachedClips = default;

		//--- Unity Methods ---// 
		private void Awake()
		{
			CheckForListener();
			GenerateClipDictionary();
			GenerateInitialSources();
			
			SFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_SAVE_KEY, 1.0f);
			MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_SAVE_KEY, 1.0f);
		}
		
		private void OnApplicationQuit()
		{
			PlayerPrefs.SetFloat(SFX_VOLUME_SAVE_KEY, m_SFXVolume);
			PlayerPrefs.SetFloat(MUSIC_VOLUME_SAVE_KEY, m_MusicVolume);
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			ApplyVolumeToSources();
			ApplyMutingToSources();
		}
#endif

		//--- Public Methods ---//
		public bool PlayLoopingAudio(string loopTag, AudioConstant clipName, AudioChannel channel = AudioChannel.SFX, float playVolume = 1.0f)
		{
			if (m_TaggedAudioSources.ContainsKey(loopTag))
			{
				Debug.LogWarning($"<color=blue>AudioManager: </color>Already playing a looping clip tagged '{loopTag}'");
				return false;
			}

			RuntimeAudioSource loopingSource = PlayClip(clipName, true, channel, playVolume);

			m_TaggedAudioSources.Add(loopTag, loopingSource);

			return true;
		}

		public void StopLoopingAudio(string loopTag)
		{
			if (!m_TaggedAudioSources.ContainsKey(loopTag))
			{
				Debug.LogWarning($"<color=blue>AudioManager: </color>No current loop clip has the tag '{loopTag}'");
				return;
			}

			RuntimeAudioSource loopedSource = m_TaggedAudioSources[loopTag];
			loopedSource.m_Source.Stop();

			m_TaggedAudioSources.Remove(loopTag);
		}

		public void PlayOneShotAudio(AudioConstant clipName, AudioChannel channel = AudioChannel.SFX, float playVolume = 1.0f)
		{
			PlayClip(clipName, false, channel, playVolume);
		}

		//--- Protected Methods ---//
		protected virtual void CheckForListener()
		{
			if (FindObjectOfType<AudioListener>() == null)
			{
				gameObject.AddComponent<AudioListener>();
			}
		}

		protected virtual void GenerateClipDictionary()
		{
			m_CachedClips = new Dictionary<AudioConstant, AudioClip>();

			foreach (var clip in m_AudioClips)
			{
				AudioConstant clipNameAsConstant = (AudioConstant)Enum.Parse(typeof(AudioConstant), clip.name);
				m_CachedClips.Add(clipNameAsConstant, clip);
			}
		}

		protected virtual void GenerateInitialSources()
		{
			for (int i = 0; i < m_InitialAudioSourceCount; ++i)
			{
				AddAudioSource();
			}
		}

		protected virtual RuntimeAudioSource AddAudioSource(AudioChannel channel = AudioChannel.SFX, bool shouldLoop = false)
		{
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.mute = (channel == AudioChannel.SFX) ? m_IsSFXMuted : m_IsMusicMuted;

			RuntimeAudioSource runtimeSource = new RuntimeAudioSource(audioSource, 1.0f, channel);
			m_RuntimeAudioSources.Add(runtimeSource);
			return runtimeSource;
		}

		protected virtual RuntimeAudioSource FindFreeSource(AudioChannel channel = AudioChannel.SFX, bool shouldLoop = false)
		{
			// Try to find a free source in the existing list
			foreach(var runtimeSource in m_RuntimeAudioSources)
			{
				if (!runtimeSource.m_Source.isPlaying)
				{
					runtimeSource.m_Channel = channel;
					runtimeSource.m_Source.loop = shouldLoop;
					return runtimeSource;
				}
			}

			// Create a new source if there are none free
			return AddAudioSource(channel, shouldLoop);
		}

		protected RuntimeAudioSource PlayClip(AudioConstant clipName, bool shouldLoop, AudioChannel channel = AudioChannel.SFX, float volume = 1.0f)
		{
			AudioClip clip = m_CachedClips[clipName];
			
			float channelVolume = (channel == AudioChannel.SFX) ? m_SFXVolume : m_MusicVolume;

			RuntimeAudioSource runtimeSource = FindFreeSource(channel, shouldLoop);
			runtimeSource.m_Source.clip = clip;
			runtimeSource.m_BaseVolume = volume;
			runtimeSource.m_Source.volume = runtimeSource.m_BaseVolume * MasterVolume * channelVolume;
			runtimeSource.m_Source.Play();

			return runtimeSource;
		}

		protected void ApplyVolumeToSources()
		{
			foreach (var runtimeSource in m_RuntimeAudioSources)
			{
				float channelVolume = (runtimeSource.m_Channel == AudioChannel.SFX) ? m_SFXVolume : m_MusicVolume;
				runtimeSource.m_Source.volume = runtimeSource.m_BaseVolume * MasterVolume * channelVolume;
			}
		}

		protected void ApplyMutingToSources()
		{
			foreach (var runtimeSource in m_RuntimeAudioSources)
			{
				bool isMuted = (runtimeSource.m_Channel == AudioChannel.SFX) ? m_IsSFXMuted : m_IsMusicMuted;
				runtimeSource.m_Source.mute = isMuted;
			}
		}

		//--- Private Methods ---//
	}
}