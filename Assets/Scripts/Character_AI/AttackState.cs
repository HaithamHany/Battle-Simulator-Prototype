using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IUnitStateMachine
{
    private Unit target;

    public void SetTarget(Unit newTarget)
    {
        target = newTarget;
    }
    public void Enter(Unit unit)
    {
       // unit.Attack(target);
    }

    public void Execute(Unit unit)
    {
        // Check if the target is still alive and in attack range
        if (!unit.IsAttacking && target != null && target.IsAlive() && unit.IsInRange(target))
        {
            unit.Attack(target); // Continue the attack
        }
        else
        {
            // Target is dead or out of range, transition to another state (e.g., MoveState)
            unit.ChangeState(unit.MoveState);
        }
    }

    public void Exit(Unit unit)
    {
        // Cleanup or reset logic for Attack state
    }
}
