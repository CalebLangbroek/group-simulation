using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSimulationSettings : SingletonScriptableObject<GroupSimulationSettings>
{
    [Header("Data Settings")]
    public string IndividualItemRankingsFileName = "Individuals Item Rankings";
    public string ExpertItemRankingsFileName = "Expert Item Rankings";
    public List<ItemRanking> ExpertItemRankings = new List<ItemRanking>();

    [Header("Group Settings")]
    public int GroupCount = 10;

    [Header("Simulation Settings")]
    public int MaxSimulationSteps = 2500;
    public int MaxReward = 100;

    // read expert item rankings from file
    public void ParseExpertItemRankings()
    {
        TextAsset json = Resources.Load<TextAsset>(ExpertItemRankingsFileName);
        ExpertItemRankings = JsonUtility.FromJson<ItemRankingCollection>(json.text).Rankings;
    }
}
