using System.Collections.Generic;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Notifies UI when enemy teams are initialized. Sends teams data to UI
    /// </summary>
    public class EnemiesInitializedEvent : UnityEvent<List<TeamManager>>
    {
        public static EnemiesInitializedEvent Instance = new EnemiesInitializedEvent();
    
    }
}