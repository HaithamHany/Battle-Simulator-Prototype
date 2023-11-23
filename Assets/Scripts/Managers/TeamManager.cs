using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    
    private List<Unit> units = new List<Unit>();
    private TeamDataConfig teamData; //For selection
    //After spawning different team
    private TeamDataConfig updatedData; 
    private int deadMembers;
    
    public TeamDataConfig TeamData=> teamData;
    public TeamDataConfig UpdatedData=> updatedData;
    public List<Unit> Units => units;
    
    private const int GRID_SIZE = 3;
    private const int GRID_SPACING = 2;


    private void Awake()
    {
        UnitDiedEvent.Instance.AddListener(OnUnitDied);
    }

    private void OnUnitDied(Unit deadUnit)
    {
        if (deadUnit.UnitManager != this) return;
        
        deadMembers++;

        if (AllMembersDead())
        {
            TeamLostEvent.Instance.Invoke(this);
        }
    }

    public bool AllMembersDead()
    {
        return deadMembers >= units.Count;
    }

    // initialize each team
    public void Init(TeamDataConfig teamData)
    {
        this.teamData = teamData;
        
        //Initially same as original team data
        updatedData = teamData;
    }
    
    // Spawn units based on the provided TeamData
    public void SpawnUnits()
    {
        int unitCount = Mathf.Min(GRID_SIZE * GRID_SIZE, teamData.UnitConfigs.Count);

        for (int i = 0; i < unitCount; i++)
        {
            var unitData = teamData.UnitConfigs[i];
            int row = i / GRID_SIZE;
            int col = i % GRID_SIZE;
            Vector3 spawnPosition = CalculateSpawnPosition(row, col); // Calculate the spawn position

            var newUnit = InstantiateUnit(unitData, spawnPosition);
            units.Add(newUnit);
        }
    }

    private void OrganizeTeam(TeamDataConfig newTeamData)
    {
        int unitCount = newTeamData.UnitConfigs.Count;
        for (int i = 0; i < unitCount; i++)
        {
            int row = i / GRID_SIZE;
            int col = i % GRID_SIZE;
            Vector3 spawnPosition = CalculateSpawnPosition(row, col);
            units[i].transform.position = spawnPosition;
        }
    }

    private Vector3 CalculateSpawnPosition(int row, int col)
    {
        // Calculate the spawn position based on the row and column
        // Adjust the position as needed to fit your game's grid layout
        float xOffset = col * GRID_SPACING; //  this value based on  grid spacing
        float zOffset = row * GRID_SPACING; //  this value based on  grid spacing
        return transform.position + new Vector3(xOffset, 0f, zOffset);
    }

    private Unit InstantiateUnit(UnitData data, Vector3 spawnPos)
    {
        var unit = Instantiate(teamData.UnitPrefab, spawnPos, Quaternion.identity);
        unit.name = $"{teamData.TeamName} unit";
        // Initialize the unit with the provided UnitData
        unit.Init(teamData.TeamColor, data, this);

        return unit;
    }

    public void StartGame(List<Unit> enemyUnits)
    {
        foreach (var unit in units)
        {
            unit.Fight(enemyUnits);
        }
    }

    /// <summary>
    /// Updating team and utilizing object pooling
    /// </summary>
    /// <param name="teamData"></param>
    public void ResetTeam(TeamDataConfig teamData)
    {
        //Updating  the data
        updatedData = teamData;
        deadMembers = 0;
        //Disable all
        foreach (var unit in Units)
        {
            unit.Reset();
            unit.gameObject.SetActive(false);
        }

        for (int i = 0; i < teamData.UnitConfigs.Count; i++)
        {
            var unitData = teamData.UnitConfigs[i];

            //Recycle the same object before deciding to instantiate a new one
            if (i < Units.Count && Units[i] != null)
            {
                var unit = Units[i];
                unit.Init(teamData.TeamColor, unitData, this);
                unit.gameObject.SetActive(true);
                unit.name = $"{teamData.TeamName} unit";
                continue;
            }
            
            //Otherwise instantiate a new one in case team members size is bigger than the initial pool of objects
            var newUnit = InstantiateUnit(unitData, Vector3.zero);
            newUnit.Init(teamData.TeamColor, unitData, this);
            units.Add(newUnit);
        }
        
        OrganizeTeam(teamData);
    }
    

    private void OnDestroy()
    {
        UnitDiedEvent.Instance.RemoveListener(OnUnitDied);
    }
}