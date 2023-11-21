using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IUnitStateMachine
{
    public void Enter(Unit unit)
    {
        unit.Die(); // Continue the attack
    }

    public void Execute(Unit unit)
    {
       
    }

    public void Exit(Unit unit)
    {
        // Cleanup or reset logic for Attack state
    }
}
