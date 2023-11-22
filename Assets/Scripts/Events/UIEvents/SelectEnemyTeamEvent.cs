
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Invoked when selecting a team
    /// </summary>
    public class SelectEnemyTeamEvent : UnityEvent<int>
    {
        public static SelectEnemyTeamEvent Instance = new SelectEnemyTeamEvent();
    }
}

