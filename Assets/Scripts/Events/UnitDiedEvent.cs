using System.Collections.Generic;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Notifies UI when enemy teams are initialized. Sends teams data to UI
    /// </summary>
    public class UnitDiedEvent : UnityEvent<Unit>
    {
        public static UnitDiedEvent Instance = new UnitDiedEvent();
    }
}