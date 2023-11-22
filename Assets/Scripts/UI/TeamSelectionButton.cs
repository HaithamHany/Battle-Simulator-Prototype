using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionButton : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI buttonText;
   [SerializeField] private Button button;
   private int currentIndex;

   private void Awake()
   { 
      button.onClick.AddListener(OnButtonSelected);
   }

   public void Init(string buttonName, int currentIndex)
   {
      this.currentIndex = currentIndex;
      buttonText.text = buttonName;
   }

   private void OnButtonSelected()
   {
      SelectEnemyTeamEvent.Instance.Invoke(currentIndex);
   }

   public void OnDestroy()
   {
      button.onClick.RemoveListener(OnButtonSelected);
   }
}
