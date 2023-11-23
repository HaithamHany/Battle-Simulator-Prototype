using System.Collections.Generic;
using UnityEngine.Events;

namespace Events
{
    public class TeamLostEvent : UnityEvent<TeamManager>
    {
        public static TeamLostEvent Instance = new TeamLostEvent();
    }
}