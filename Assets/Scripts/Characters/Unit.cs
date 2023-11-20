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
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float hp = 100;
    [SerializeField] private bool disable;

    //TODO: Clean this variable
    private float jumpForce = 7;

    private float lastAttackTime = 0f;
    private MoveState moveState;
    private AttackState attackState;
    private IUnitStateMachine currentState;
    
    
    //TODO: MOVE THIS TO MANAGER
    [SerializeField] private List<Unit> enemyUnits = new List<Unit>();
    [SerializeField] private Unit targetUnit; // remove serialization after testing is done
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
        return hp > 0; 
    }

    // Check if the target unit is within attack range
    public bool IsInRange(Unit target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= attackRange;
    }
    
    public void FindAndSetNewTarget()
    {
        targetUnit = FindRandomEnemy();
    }
    
    // Move towards a specified destination
    public void MoveTowardsDestination(Transform destination)
    {
        // Calculate the direction to the destination
        Vector3 direction = (destination.position - transform.position).normalized;

        // Calculate the velocity based on movementSpeed
        Vector3 velocity = direction * movementSpeed;

        // Apply the velocity as a force to the Rigidbody
        rigidbody.velocity = velocity;
        
        transform.LookAt(destination);
    }
    
    // Attack the target unit
   public void Attack(Unit target)
   {
       // Check if the unit can perform an attack based on cooldown and target validity
       if (Time.time - lastAttackTime <= attackCooldown || target == null || !target.IsAlive() || !IsInRange(target))
           return;

       // Start the attack coroutine
       StartCoroutine(AttackCoroutine(target));

       // Update the last attack time for cooldown management
       lastAttackTime = Time.time;
   }

   private IEnumerator AttackCoroutine(Unit target)
   {
       while (target != null && target.IsAlive() && IsInRange(target))
       {
           // Jump towards the current target
           JumpTowardsTarget(target.transform.position);

           // Wait for a moment to simulate the attack duration
           yield return new WaitForSeconds(0.5f); // Adjust the duration as needed

           // Jump back after a slight delay
           yield return new WaitForSeconds(0.2f); // Adjust the delay as needed
           JumpBack();
        
           // Optionally, you can wait for a cooldown period using WaitForSeconds
           yield return new WaitForSeconds(attackCooldown);
       }
   }

   private void JumpTowardsTarget(Vector3 targetPosition)
   {
       if (rigidbody != null)
       {
           // Calculate the direction towards the target
           Vector3 direction = (targetPosition - transform.position).normalized;

           // Apply an explosion force to jump towards the target
           rigidbody.AddForce(direction * jumpForce, ForceMode.Impulse);
       }
   }

   private void JumpBack()
   {
       if (rigidbody != null)
       {
           Vector3 directionBack = (targetUnit.transform.position - transform.position).normalized;

           // Apply an equal and opposite force to jump back
           rigidbody.AddForce(-directionBack * (jumpForce *2), ForceMode.Impulse);
       }
   }

    
    public void TakeDamage(float damage)
    {
        // Deduct HP based on the incoming damage
        hp -= damage;

        if (!IsAlive())
        {
            // Transition to DeathState when HP reaches zero
            Destroy(gameObject);
           // ChangeState(deathState);
        }
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

