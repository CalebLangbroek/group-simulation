using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSimulationSettings : SingletonScriptableObject<GroupSimulationSettings>
{
    [Header("Ranking Data")]
    public string IndividualItemRankingsFileName = "Individuals Item Rankings";
    public string ExpertItemRankingsFileName = "Expert Item Rankings";
    public List<ItemRanking> ExpertItemRankings = null;

    [Header("Group Settings")]
    public int GroupCount = 10;

    [Header("Simulation Settings")]
    public int MaxSimulationSteps = 2500;

    void Awake()
    {
        // read expert item rankings from file
        TextAsset json = Resources.Load<TextAsset>(ExpertItemRankingsFileName);
        ExpertItemRankings = JsonUtility.FromJson<ItemRankingCollection>(json.text).Rankings;
    }
}
