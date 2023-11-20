using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTeamData", menuName = "BattleSimulator/TeamData", order = 2)]
public class TeamData : ScriptableObject
{
    public List<UnitData> unitConfigs;
}

[System.Serializable]
public class UnitData
{
    [SerializeField] private float HP;
    [SerializeField] private float Attack;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float AttackRange;
    [SerializeField] private float MovementSpeed;
    
}