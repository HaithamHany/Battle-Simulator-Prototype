using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerConfig gameConfig; // List of team configurations
    private List<TeamManager> teamManagers = new List<TeamManager>();

    void Start()
    {
        InitializeTeams();
    }

    private void InitializeTeams()
    {
        foreach (var teamData in gameConfig.TeamDataConfigs)
        {
            TeamManager newTeamManager = InstantiateTeamManager(teamData);
            teamManagers.Add(newTeamManager);
            newTeamManager.Init(teamData);
        }
    }

    private TeamManager InstantiateTeamManager(TeamDataConfig data)
    {
        GameObject teamManagerObject = new GameObject("TeamManager_" + data.TeamName);
        TeamManager newTeamManager = teamManagerObject.AddComponent<TeamManager>();

        return newTeamManager;
    }
    
    // Additional methods for game-wide functionality
}