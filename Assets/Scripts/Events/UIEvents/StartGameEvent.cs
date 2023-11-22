
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Invoked when a user Starts a game
    /// </summary>
    public class StartGameEvent : UnityEvent
    {
        public static StartGameEvent Instance = new StartGameEvent();
    }
}

