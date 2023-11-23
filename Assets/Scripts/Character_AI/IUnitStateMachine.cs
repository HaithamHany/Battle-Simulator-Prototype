using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStateMachine 
{
    /// <summary>
    /// Called once when the state entered
    /// </summary>
    /// <param name="unit"> Instance of the unit</param>
    void Enter(Unit unit);
    
    /// <summary>
    /// Executed and called every frame for state logic
    /// </summary>
    /// <param name="unit">Unit instance</param>
    void Execute(Unit unit);
    
    /// <summary>
    /// Used for Cleanup or reset logic for a state
    /// </summary>
    /// <param name="unit"></param>
    void Exit(Unit unit);
}
