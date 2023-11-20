using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IUnitStateMachine
{
    public void Enter(Unit unit)
    {
        // Initialization for Attack state
    }

    public void Execute(Unit unit)
    {
        // Logic for attacking the target
    }

    public void Exit(Unit unit)
    {
        // Cleanup or reset logic for Attack state
    }
}
