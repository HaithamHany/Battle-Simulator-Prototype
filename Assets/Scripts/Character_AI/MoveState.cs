using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IUnitStateMachine
{
    public void Enter(Unit unit)
    {
        unit.FindAndSetNewTarget();
    }

    public void Execute(Unit unit)
    {
        // Check if the current target is no longer valid
        if (unit.TargetUnit == null || !unit.TargetUnit.IsAlive())
        {
            unit.FindAndSetNewTarget();
            return;
        }
        
        if (unit.IsInRange(unit.TargetUnit))
        {
            unit.AttackState.SetTarget(unit.TargetUnit);
            unit.ChangeState(unit.AttackState);
            return;
        }
        
        // Continue moving towards the destination
        unit.MoveTowardsDestination(unit.TargetUnit.transform);
    }

    public void Exit(Unit unit)
    {
        // Cleanup or reset
    }
}
