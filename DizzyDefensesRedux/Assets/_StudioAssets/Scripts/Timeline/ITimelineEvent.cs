using System;
using System.Collections;
using UnityEngine;

namespace RubberDucks.DizzyDefenses.Timelines
{
    public interface ITimelineEvent
    {
        /// <summary>
        /// This controls when the event will be executed
        /// All events with the same value here will be executed simultaneously
        /// TimelineManager.cs has some pre-defined values that can be used for most things
        /// In general, the player interactions should be first, the sun should be last, and the environment should be in the middle
        /// </summary>
        public int EventOrder { get; }

        /// <summary>
        /// Override this coroutine with the actual logic for the event (ex: rotating the tower to a new orientation over time)
        /// Should take some amount of time to play an animation as representation
        /// </summary>
        protected IEnumerator ProcessExecution();
        
        /// <summary>
        /// Called by the owning TimelineStep when it is executed. Generally should not be overriden otherwise
        /// </summary>
        public void Execute(Action<ITimelineEvent> onExecutionComplete)
        {
            TimelineManager.Instance.StartCoroutine(HandleExecution());
            IEnumerator HandleExecution()
            {
                yield return ProcessExecution();

                onExecutionComplete(this);
            }
        }

        /// <summary>
        /// Override this coroutine with the actual logic for the event reversion (ex: rotating the tower back to a previous orientation)
        /// Should take some amount of time to play an animation as representation
        /// </summary>
        protected IEnumerator ProcessRevert();

        /// <summary>
        /// Called by the owning TimelineStep when it is reverted. Generally should not be overriden otherwise
        /// </summary>
        public void Revert(Action<ITimelineEvent> onRevertComplete)
        {
            TimelineManager.Instance.StartCoroutine(HandleRevert());
            IEnumerator HandleRevert()
            {
                yield return ProcessRevert();

                onRevertComplete(this);
            }
        }
    }
}