using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IUnitStateMachine
{
    public void Enter(Unit unit)
    {
        unit.Die(); 
    }

    public void Execute(Unit unit)
    {
       
    }

    public void Exit(Unit unit)
    {
     
    }
}
