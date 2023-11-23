
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Invoked When resetting the game
    /// </summary>
    public class ResetGameEvent : UnityEvent
    {
        public static ResetGameEvent Instance = new ResetGameEvent();
    }
}

