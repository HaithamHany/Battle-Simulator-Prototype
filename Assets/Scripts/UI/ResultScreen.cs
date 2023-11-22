using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI teamNameTxt;
    [SerializeField] private Image teamFlag;

    public void Init(TeamDataConfig teamData)
    {
        if(teamData == null) return;
        
        teamNameTxt.text = $"{teamData.TeamName} WON!";
        teamFlag.color = teamData.TeamColor;
    }
}
