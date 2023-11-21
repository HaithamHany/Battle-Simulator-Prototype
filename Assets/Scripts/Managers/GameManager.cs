using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerConfig gameConfig; // List of team configurations
    private List<TeamManager> teamManagers = new List<TeamManager>();
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
    }
    
    void Start()
    {
        InitializeTeams();
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