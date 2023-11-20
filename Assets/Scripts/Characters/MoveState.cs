using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IUnitStateMachine
{
    private Vector3 destination;

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
    }

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
            unit.ChangeState(unit.AttackState);
            return;
        }
        
        // Continue moving towards the destination
        unit.MoveTowardsDestination(destination);
    }

    public void Exit(Unit unit)
    {
        // Cleanup or reset logic for Move state
    }
}
