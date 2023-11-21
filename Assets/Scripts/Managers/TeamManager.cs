using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    
    private List<Unit> units = new List<Unit>();
    private TeamDataConfig teamData; // Set this via the GameManager or Inspector
    
    public TeamDataConfig TeamData=> teamData;
    
    private const int GRID_SIZE = 3;
    private const int GRID_SPACING = 2;

    // Call this method from the GameManager to initialize each team
    public void Init(TeamDataConfig teamData)
    {
        this.teamData = teamData;
        SpawnUnits();
    }
    
    // Spawn units based on the provided TeamData
    private void SpawnUnits()
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
        
        // Initialize the unit with the provided UnitData
        unit.Init(teamData.TeamColor, data);

        return unit;
    }

    

    // Implement additional methods as needed for team-specific behavior, such as:
    // - Handling commands from the GameManager
    // - Coordinating unit movements and actions
    // - Responding to changes in game state
    // - Managing resources or special abilities unique to the team
}