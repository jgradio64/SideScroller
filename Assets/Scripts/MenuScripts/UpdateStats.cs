using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Assets.Scripts;

public class UpdateStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KillsText;
    [SerializeField] private TextMeshProUGUI GoldText;

    // Update is called once per frame
    void Update()
    {
        KillsText.text = "Num Kills: " + PlayerStats.MobKills;
        GoldText.text = "Gold: " + PlayerStats.Gold;
    }
}
