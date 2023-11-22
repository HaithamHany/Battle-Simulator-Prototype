using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    
    private List<Unit> units = new List<Unit>();
    private TeamDataConfig teamData; // Set this via the GameManager or Inspector
    
    public TeamDataConfig TeamData=> teamData;
    public List<Unit> Units => units;
    
    private const int GRID_SIZE = 3;
    private const int GRID_SPACING = 2;

    // Call this method from the GameManager to initialize each team
    public void Init(TeamDataConfig teamData)
    {
        this.teamData = teamData;
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
        unit.Init(teamData.TeamColor, data);

        return unit;
    }

    public void SetEnemyTeam(List<Unit> enemyUnits)
    {
        foreach (var unit in units)
        {
            unit.SetEnemyTeam(enemyUnits);
        }
    }

    /// <summary>
    /// Updating team and utilizing object pooling
    /// </summary>
    /// <param name="teamData"></param>
    public void UpdateTeam(TeamDataConfig teamData)
    {
        //Disable all
        foreach (var unit in Units)
        {
            unit.gameObject.SetActive(false);
        }

        for (int i = 0; i < teamData.UnitConfigs.Count; i++)
        {
            var unitData = teamData.UnitConfigs[i];

            //Recycle the same object before deciding to instantiate a new one
            if (i < Units.Count && Units[i] != null)
            {
                Units[i].Init(teamData.TeamColor, unitData);
                Units[i].gameObject.SetActive(true);
                continue;
            }
            
            //Otherwise instantiate a new one in case team members size is bigger than the initial pool of objects
            var newUnit = InstantiateUnit(unitData, Vector3.zero);
            newUnit.Init(teamData.TeamColor, unitData);
            units.Add(newUnit);
        }
        
        OrganizeTeam(teamData);
    }
}