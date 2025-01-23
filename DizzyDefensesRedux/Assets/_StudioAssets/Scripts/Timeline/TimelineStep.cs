using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RubberDucks.DizzyDefenses.Timelines
{
    public class TimelineStep : MonoBehaviour
    {
        // Event list ordering is maintained strictly so that the event order goes from lowest to highest at all times
        private LinkedList<ITimelineEvent> Events = new();

        private IEnumerator ActiveProcessSequence;

        public void RegisterEvent(ITimelineEvent evt)
        {
            Events.AddLast(evt);
            Events.OrderBy(x => x.EventOrder);
        }

        public void UnregisterEvent(ITimelineEvent evt)
        {
            Events.Remove(evt);
        }

        public void ProcessRevert()
        {
            ProcessEvents(true);
        }

        // TODO: Have some kind of cheat in case this is needed if the game hangs
        public void StopProcessSequence()
        {
            if (ActiveProcessSequence != null)
            {
                StopCoroutine(ActiveProcessSequence);
                ActiveProcessSequence = null;
            }
        }

        public void ProcessExecution()
        {
            ProcessEvents(false);
        }

        private void ProcessEvents(bool execute)
        {
            var eventsToProcess = new List<ITimelineEvent>(Events);

            // If reverting, reverse the order of events so we undo them in the reverse of their original execution order
            if (!execute)
            {
                eventsToProcess.Reverse();
            }

            IEnumerator DoExecution()
            {
                while (eventsToProcess.Count > 0)
                {
                    // Find out what the next event index is from the first element (list is always sorted)
                    // Grab all of the elements that are part of that group so we can process them all together
                    int nextEventGroupIndex = eventsToProcess.First().EventOrder;
                    var eventsInGroup = eventsToProcess.FindAll(x => x.EventOrder == nextEventGroupIndex);
                    void OnEventProcessed(ITimelineEvent evt) => eventsInGroup.Remove(evt);

                    // Remove the whole group from the remaining list so we can process the group separately
                    eventsToProcess.RemoveAll(x => x.EventOrder == nextEventGroupIndex);

                    // Process all of the events in the group simulataneously
                    // They all start at the same time but may have different durations so need to wait until the list is cleared
                    foreach (var evt in eventsInGroup)
                    {
                        if (execute)
                        {
                            evt.Execute(OnEventProcessed);
                        }
                        else
                        {
                            evt.Revert(OnEventProcessed);
                        }
                    }

                    yield return new WaitUntil(() => eventsInGroup.Count == 0);
                }
            }

            ActiveProcessSequence = DoExecution();
            StartCoroutine(ActiveProcessSequence);
        }
    }
}