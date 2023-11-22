using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private TeamSelectionButton selectionButtonPrefab;
   [SerializeField] private Transform selectionButtonsParent;
   [SerializeField] private GameObject uiCanvas;
   [SerializeField] private ResultScreen resultScreen;
   [SerializeField] private GameObject mainMenuScreen;
   
   private void Awake()
   {
      EnemiesInitializedEvent.Instance.AddListener(OnEnemiesInitialized);
      GameOverEvent.Instance.AddListener(OnGameEnded);
   }

   private void OnGameEnded(TeamManager winnerTeam)
   {
      resultScreen.Init(winnerTeam.TeamData);
      uiCanvas.gameObject.SetActive(true);
      resultScreen.gameObject.SetActive(true);
      mainMenuScreen.SetActive(false);
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
      uiCanvas.SetActive(false);
   }

   private void OnDestroy()
   {
      EnemiesInitializedEvent.Instance.RemoveListener(OnEnemiesInitialized);
   }

   public void GoToMainMenu()
   {
      resultScreen.gameObject.SetActive(false);
      mainMenuScreen.gameObject.SetActive(true);
      //TODO: Restart game
   }
}
