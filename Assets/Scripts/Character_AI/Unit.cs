using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Renderer renderer;
    
    private float attackRange;
    private float movementSpeed;
    private float attackDamage;
    private float hp;
    private float attackSpeed;
    private float attackCooldown ;
    private Unit targetUnit;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;
    private bool isCoolingDown = false;
    private bool isReadyToAttack = false;
    
    private MoveState moveState;
    private AttackState attackState;
    private DeathState deathState;
    private IUnitStateMachine currentState;
    
    //remove for testing
    [SerializeField] private List<Unit> enemyUnits = new List<Unit>();
    
    public Unit TargetUnit => targetUnit;
    public AttackState AttackState => attackState;
    public MoveState MoveState => moveState;
    public bool IsAttacking => isAttacking;
    
    private const float JUMP_BACK_FORCE = 7;
    private const float ATTACK_COOLDOWN_MIN = 0.2f;
    private const float ATTACK_COOLDOWN_MAX = 2.8f;


    public void Init(Color teamColor, UnitData unitConfig)
    {
        hp = unitConfig.Hp;
        attackDamage = unitConfig.Attack;
        attackSpeed = unitConfig.AttackSpeed;
        attackRange = unitConfig.AttackRange;
        movementSpeed = unitConfig.MovementSpeed;

        hpText.text = $"{hp}";
        renderer.material.color = teamColor;
    }

    public void SetEnemyTeam(List<Unit> enemyUnits)
    {
        this.enemyUnits = enemyUnits;
    }

    void Start()
    {
        moveState = new MoveState();
        attackState = new AttackState();
        deathState = new DeathState();

        ChangeState(moveState);
    }

    void Update()
    {
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
       attackCooldown = Random.Range(ATTACK_COOLDOWN_MIN, ATTACK_COOLDOWN_MAX);
       // If the unit is ready to attack and the cooldown has passed, attack the target
       if (isReadyToAttack && Time.time >= lastAttackTime + attackCooldown && CanAttack(targetUnit))
       {
           JumpTowardsTarget(targetUnit.transform.position);
           lastAttackTime = Time.time;
           isReadyToAttack = false; // Not ready to attack again until cooldown
       }
        
       // If the unit is not ready to attack but the cooldown has passed, it means it's time to jump back
       if (!isReadyToAttack && Time.time >= lastAttackTime + attackCooldown)
       {
           JumpBack();
           isReadyToAttack = true; // Ready to attack again after jumping back
           lastAttackTime = Time.time;
       }
   }
   
   private bool CanAttack(Unit target)
   {
       return target != null && target.IsAlive() && IsInRange(target);
   }

   private void JumpTowardsTarget(Vector3 targetPosition)
   {
       if (rigidbody != null)
       {
           Vector3 direction = (targetPosition - transform.position).normalized;
           rigidbody.AddForce(direction * attackSpeed, ForceMode.Impulse);
       }
   }

   private void JumpBack()
   {
       if (rigidbody != null && targetUnit != null)
       {
           Vector3 directionBack = (targetUnit.transform.position - transform.position).normalized;
           rigidbody.AddForce(-directionBack * JUMP_BACK_FORCE, ForceMode.Impulse);
       }
   }
   
    public void TakeDamage(float damage)
    {
        // Deduct HP based on the incoming damage
        hp -= damage;
        hpText.text = $"{hp}";
        if (!IsAlive())
        {
            ChangeState(deathState);
        }
    }

    public void Die()
    {
        rigidbody.AddForce(Vector3.up * 150, ForceMode.Impulse);
        Destroy(gameObject, 3);
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

    private void OnCollisionEnter(Collision other)
    {
        //TODO: Diffrintiate between attacker and non attacker
        if (other != null && targetUnit != null && other.gameObject == targetUnit.gameObject)
        {
            targetUnit.TakeDamage(attackDamage);
        }
    }

    private enum UnitAttacState
    {
        Idle,
        Attacking,
        CoolingDown
    }
    
}

