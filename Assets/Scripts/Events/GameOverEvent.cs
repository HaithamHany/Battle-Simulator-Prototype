using System.Collections.Generic;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Notifies UI with the winner team
    /// </summary>
    public class GameOverEvent : UnityEvent<TeamDataConfig>
    {
        public static GameOverEvent Instance = new GameOverEvent();
    }
}