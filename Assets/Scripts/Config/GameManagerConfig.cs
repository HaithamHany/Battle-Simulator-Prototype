using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameManagerConfig", menuName = "BattleSimulator/GameManagerConfig", order = 1)]
public class GameManagerConfig : ScriptableObject
{
    [SerializeField] private List<TeamDataConfig> teamConfigs = new List<TeamDataConfig>();

    public List<TeamDataConfig> TeamDataConfigs => teamConfigs;
}
