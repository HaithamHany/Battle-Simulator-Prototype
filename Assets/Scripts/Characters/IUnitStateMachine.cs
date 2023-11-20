using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStateMachine 
{
    void Enter(Unit unit);
    void Execute(Unit unit);
    void Exit(Unit unit);
}
