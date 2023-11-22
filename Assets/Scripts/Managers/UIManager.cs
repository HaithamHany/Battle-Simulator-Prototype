using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private TeamSelectionButton selectionButtonPrefab;
   [SerializeField] private Transform selectionButtonsParent;
   
   private void Awake()
   {
      EnemiesInitializedEvent.Instance.AddListener(OnEnemiesInitialized);
   }

   private void OnEnemiesInitialized(List<TeamManager> enemiesManagers)
   {
      CreateEnemiesList(enemiesManagers);

   }

   private void CreateEnemiesList(List<TeamManager> enemiesManagers)
   {
      for (int i = 0; i < enemiesManagers.Count; i++)
      {
         TeamManager manager = enemiesManagers[i];
         var teamSelectionButton = Instantiate(selectionButtonPrefab, selectionButtonsParent);
         teamSelectionButton.Init(manager.TeamData.TeamName, i);
      }
   }

   public void StartGame()
   {
      StartGameEvent.Instance.Invoke();
   }

   private void OnDestroy()
   {
      EnemiesInitializedEvent.Instance.RemoveListener(OnEnemiesInitialized);
   }
}
