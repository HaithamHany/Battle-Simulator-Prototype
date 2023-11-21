using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerConfig gameConfig; // List of team configurations
    private List<TeamManager> teamManagers = new List<TeamManager>();
    private static GameManager instance;
    private List<TeamManager> playerTeams = new List<TeamManager>();
    private List<TeamManager> enemyTeams = new List<TeamManager>();

    public static GameManager Instance => instance;

    private const int TEAM_SPACING = 30;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        StartGameEvent.Instance.AddListener(OnGameStarted);
    }
    
    private void OnGameStarted()
    {
        InitializeTeams();
        InitializeEnemyUnits();
    }
    
    private void InitializeEnemyUnits()
    {
        foreach (var playerTeam in playerTeams)
        {
            //TODO: REMOVE INDEX WITH TEAM INDEX to CHOOSE FROM
            // Call InitializeEnemyUnits for each player team to set its enemies
            playerTeam.SetEnemyTeam(enemyTeams[0].Units);
        }

        foreach (var enemyTeam in enemyTeams)
        {
            // Call InitializeEnemyUnits for each enemy team to set its enemies
            enemyTeam.SetEnemyTeam(playerTeams[0].Units);
        }
    }

    private void InitializeTeams()
    {
        float middleX = (gameConfig.TeamDataConfigs.Count - 1) * TEAM_SPACING / 2f;
        
        for (int i = 0; i <gameConfig.TeamDataConfigs.Count; i++)
        {
            var teamData = gameConfig.TeamDataConfigs[i];
            TeamManager newTeamManager = InstantiateTeamManager(teamData);
            teamManagers.Add(newTeamManager);
            
            float xPos = i * TEAM_SPACING - middleX;
            Vector3 teamPosition = new Vector3(xPos, 0f, 0f);
            newTeamManager.transform.position = teamPosition;
            
            newTeamManager.Init(teamData);
            
            // Categorize teams based on their ETeamType
            if (teamData.TeamType == ETeamType.PlayerTeam)
            {
                playerTeams.Add(newTeamManager);
                continue;
            }
            
            enemyTeams.Add(newTeamManager);
        }
    }

    private TeamManager InstantiateTeamManager(TeamDataConfig data)
    {
        GameObject teamManagerObject = new GameObject("TeamManager_" + data.TeamName);
        TeamManager newTeamManager = teamManagerObject.AddComponent<TeamManager>();

        return newTeamManager;
    }

    public void OnDestroy()
    {
        StartGameEvent.Instance.RemoveListener(OnGameStarted);
    }
}