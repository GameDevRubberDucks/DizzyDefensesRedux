using RubberDucks.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubberDucks.DizzyDefenses.Timelines
{
    public class TimelineManager : Singleton<TimelineManager>
    {
        /// <summary>
        /// The different types of event groups that exist
        /// Can manually assign a different ordering but generally will be using these
        /// 
        /// The player interactions always occurs FIRST (ie: rotating a tower)
        /// The environment interaction occur afterwards (ie: a tower goes underground)
        /// The sun movements always occur LAST. This is mostly here to show the passage of time regardless
        /// </summary>
        public const int EventOffset_PlayerInteraction = 0;
        public const int EventOffset_EnvironmentalInteraction = 5000;
        public const int EventOffset_Sun = 10000;
    }
}