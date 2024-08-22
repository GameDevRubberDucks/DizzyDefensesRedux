/*
 * AudioManagerCustomInspector.cs
 * 
 * Description:
 * - Custom inspector for the AudioManager class that enables support for generating a list of audio constants
 * 
 * Author(s): 
 * - Dan
*/

using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

using RubberDucks.Utilities.Audio;

namespace RubberDucks.Utilities.CustomInspectors
{
	[CustomEditor(typeof(AudioManager))]
	public class AudioManagerCustomInspector : Editor
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		private string m_ConstantsFilePath = "/_StudioAssets/Scripts/Utilities/Audio/"; // TODO: Add support to change this through the inspector
		private string m_ConstantsFileName = "AudioConstants.cs";

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public override void OnInspectorGUI()
		{
			string fullPath = Application.dataPath + m_ConstantsFilePath + m_ConstantsFileName;

			if (GUILayout.Button("Regenerate AudioConstants.cs"))
			{
				GenerateFile(fullPath);

				Debug.Log("<color=green>AudioConstant.cs has been regenerated...</color>");
			}

			base.OnInspectorGUI();
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
		private void GenerateFile(string fullPath)
		{
			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}

			FileStream fileStream = File.Create(fullPath);
			StreamWriter fileWriter = new StreamWriter(fileStream);

			fileWriter.WriteLine("// This script is generated automatically by pressing the button at the top of the AudioManager inspector");
			fileWriter.WriteLine();
			fileWriter.WriteLine("namespace RubberDucks.Utilities.Audio");
			fileWriter.WriteLine("{");
			fileWriter.WriteLine("\tpublic enum AudioConstant");
			fileWriter.WriteLine("\t{");
			WriteAllClipNames(fileWriter);
			fileWriter.WriteLine("\t}");
			fileWriter.WriteLine("}");

			fileWriter.Close();
			fileStream.Close();

			CompilationPipeline.RequestScriptCompilation();
		}

		private void WriteAllClipNames(StreamWriter streamWriter)
		{
			AudioManager audioManager = (AudioManager)target;

			List<AudioClip> audioClipsInManager = audioManager.CopyOfAudioClips;

			foreach(var clip in audioClipsInManager)
			{
				streamWriter.WriteLine($"\t\t{clip.name},");
			}
			streamWriter.WriteLine();
			streamWriter.WriteLine("\t\tNone");
		}
	}
}
