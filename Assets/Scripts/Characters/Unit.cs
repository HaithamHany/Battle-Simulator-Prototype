using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private bool isAlive;
    //TODO: MAKE SURE TO USE THE CONFIGURATION FILES AFTER TESTING
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private bool disable;
    private float lastAttackTime = 0f;
    private MoveState moveState;
    private AttackState attackState;
    private IUnitStateMachine currentState;
    
    
    //TODO: MOVE THIS TO MANAGER
    [SerializeField] private List<Unit> enemyUnits = new List<Unit>();
    private Unit targetUnit;
    public Unit TargetUnit => targetUnit;
    public AttackState AttackState => attackState;
    public MoveState MoveState => moveState;

    void Start()
    {
        if(disable) return;
        moveState = new MoveState();
        attackState = new AttackState();

        ChangeState(moveState);
    }

    void Update()
    {
        if(disable) return;
        currentState?.Execute(this);
    }

    public void ChangeState(IUnitStateMachine newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
    
    public bool IsAlive()
    {
        // Implement the logic to check if the unit is alive (e.g., based on HP)
        return isAlive; // Modify this based on your game's logic
    }

    // Check if the target unit is within attack range
    public bool IsInRange(Unit target)
    {
        if (target != null) return false;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= attackRange;
    }
    
    public void FindAndSetNewTarget()
    {
        targetUnit = FindRandomEnemy();
        if (targetUnit != null)
        {
            moveState.SetDestination(targetUnit.transform.position);
        }
    }
    
    // Move towards a specified destination
    public void MoveTowardsDestination(Vector3 destination)
    {
        // Calculate the direction to the destination
        Vector3 direction = (destination - transform.position).normalized;

        // Calculate the velocity based on movementSpeed
        Vector3 velocity = direction * movementSpeed;

        // Apply the velocity as a force to the Rigidbody
        rigidbody.velocity = velocity;
    }
    
    // Attack the target unit
    public void Attack(Unit target)
    {
        // Implement attack logic here, including cooldown management
    }
    
    // Find a random enemy unit (you need to implement this)
    private Unit FindRandomEnemy()
    {
        // Implement logic to find a random enemy unit
        // For example, you can use a list of all units and select a random enemy unit from it

        if (enemyUnits.Count > 0)
        {
            int randomIndex = Random.Range(0, enemyUnits.Count);
            return enemyUnits[randomIndex];
        }

        return null; // No valid enemy targets found
    }
    
}

