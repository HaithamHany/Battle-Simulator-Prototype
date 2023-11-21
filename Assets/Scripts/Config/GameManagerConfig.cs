using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameManagerConfig", menuName = "BattleSimulator/GameManagerConfig", order = 2)]
public class GameManagerConfig : MonoBehaviour
{
    [SerializeField] private List<TeamDataConfig> teamConfigs = new List<TeamDataConfig>();

    public List<TeamDataConfig> TeamDataConfigs => teamConfigs;
}
