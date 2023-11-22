using System.Collections.Generic;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Notifies UI when enemy teams are initialized. Sends teams data to UI
    /// </summary>
    public class TeamLostEvent : UnityEvent<TeamManager>
    {
        public static TeamLostEvent Instance = new TeamLostEvent();
    }
}