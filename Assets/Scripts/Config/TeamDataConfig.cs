using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTeamData", menuName = "BattleSimulator/TeamDataConfig", order = 2)]
public class TeamDataConfig : ScriptableObject
{
    
    [SerializeField] private List<UnitData> unitConfigs;
    [SerializeField] private Color teamColor;
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private string teamName;
    [SerializeField] private ETeamType teamType;

    public List<UnitData> UnitConfigs => unitConfigs;
    public Color TeamColor => teamColor;
    public Unit UnitPrefab => unitPrefab;
    public string TeamName => teamName;
    public ETeamType TeamType => teamType;
}

[System.Serializable]
public class UnitData
{
    
    [SerializeField] private float hp;
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float movementSpeed;


    public float Hp => hp;

    public float Attack => attack;

    public float AttackSpeed => attackSpeed;

    public float AttackRange => attackRange;

    public float MovementSpeed => movementSpeed;
}

public enum ETeamType
{
    None = 0,
    PlayerTeam = 1,
    EnemyTeam = 2,
}