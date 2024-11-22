namespace RubberDucks.Utilities
{
	namespace UnityLifeCycle
	{
		[System.Serializable]
		public enum InitialStages
		{
			Awake,
			Start,

			None
		}

		[System.Serializable]
		public enum EveryFrameStages
		{
			Update,
			LateUpdate,
			FixedUpdate,

			None
		}

		[System.Serializable]
		public enum ObjectActiveStateStages
		{
			OnEnable,
			OnDisable,
			OnDestroy,

			None
		}
	}
}