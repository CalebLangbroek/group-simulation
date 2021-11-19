using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    [Header("Layout Settings")]
    [SerializeField]
    private int _rows = 3;

    [SerializeField]
    private int _floorSize = 10;

    [SerializeField]
    private int _spacingSize = 5;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _groupPrefab;

    void Awake()
    {
        // read individuals' item rankings from file
        TextAsset json = Resources.Load<TextAsset>(GroupSimulationSettings.Instance.IndividualItemRankingsFileName);
        List<IndividualItemRanking> individualItemRankings = JsonUtility.FromJson<IndividualItemRankingCollection>(json.text).IndividualRankings;
        individualItemRankings.Sort((a, b) => a.TeamID - b.TeamID);

        // initialize agents
        int agentIndex = 0;
        for (int i = 0; i < GroupSimulationSettings.Instance.GroupCount; i++)
        {
            Vector3 floorLocation = new Vector3(i % _rows * (_floorSize + _spacingSize), 0, Mathf.Floor(i / _rows) * (_floorSize + _spacingSize));
            GameObject groupInstance = Instantiate(_groupPrefab, floorLocation, new Quaternion());
            GroupModel groupModel = new GroupModel(individualItemRankings[agentIndex].TeamID, individualItemRankings.GetRange(agentIndex, individualItemRankings[agentIndex].GroupSize));
            groupInstance.GetComponent<GroupController>().Initialize(groupModel);
            agentIndex += individualItemRankings[agentIndex].GroupSize;
        }
    }
}
