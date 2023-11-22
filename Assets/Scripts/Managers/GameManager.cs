using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerConfig gameConfig; // List of team configurations
    private List<TeamManager> teamManagers = new List<TeamManager>();
    private List<TeamManager> playerTeams = new List<TeamManager>();
    private List<TeamManager> enemyTeams = new List<TeamManager>();
    private static GameManager instance;
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
        SelectEnemyTeamEvent.Instance.AddListener(OnTeamSelected);
    }
    
    private void Start()
    {
        Setup();
        InitializeTeams();
    }
    
    private void OnTeamSelected(int teamIndex)
    {
        var newTeamData = enemyTeams[teamIndex].TeamData;
        enemyTeams.FirstOrDefault()?.UpdateTeam(newTeamData);
    }

    private void OnGameStarted()
    {
        var team = enemyTeams.FirstOrDefault()?.Units;
        InitializeEnemyUnits(team);
    }
    
    private void InitializeEnemyUnits(List<Unit> currentSelectedEnemyUnits)
    {
        foreach (var playerTeam in playerTeams)
        {
            //TODO: REMOVE INDEX WITH TEAM INDEX to CHOOSE FROM
            // Call InitializeEnemyUnits for each player team to set its enemies
            playerTeam.SetEnemyTeam(currentSelectedEnemyUnits);
        }

        foreach (var enemyTeam in enemyTeams)
        {
            // Call InitializeEnemyUnits for each enemy team to set its enemies
            //The player will always be the first or default
            enemyTeam.SetEnemyTeam(playerTeams.FirstOrDefault()?.Units);
        }
    }

    private void InitializeTeams()
    {
        var defaultPlayerTeam = playerTeams.FirstOrDefault();
        var defaultEnemyTeam = enemyTeams.FirstOrDefault();
        
        if (defaultPlayerTeam != null)
        {
            defaultPlayerTeam.SpawnUnits();
        }

        if (defaultEnemyTeam != null)
        {
            defaultEnemyTeam.SpawnUnits();
        }
    }

    private void Setup()
    {
        float middleX = TEAM_SPACING / 2f;
        
        for (int i = 0; i <gameConfig.TeamDataConfigs.Count; i++)
        {
            var teamData = gameConfig.TeamDataConfigs[i];
            TeamManager newTeamManager = InstantiateTeamManager(teamData);
            teamManagers.Add(newTeamManager);
            
            float xPos = i * TEAM_SPACING - middleX;
            Vector3 teamPosition = new Vector3(xPos, 1f, 0f);
            newTeamManager.transform.position = teamPosition;
            
            newTeamManager.Init(teamData);
            
            // Categorize teams based on their Team type
            if (teamData.TeamType == ETeamType.PlayerTeam)
            {
                playerTeams.Add(newTeamManager);
                continue;
            }
            
            enemyTeams.Add(newTeamManager);
        }
        
        EnemiesInitializedEvent.Instance.Invoke(enemyTeams);
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
        SelectEnemyTeamEvent.Instance.RemoveListener(OnTeamSelected);
    }
}