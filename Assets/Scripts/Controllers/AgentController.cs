using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour, IInitializable<AgentModel>
{
    private int _agentID;
    private List<ItemRanking> _itemRankings;

    public void Initialize(AgentModel data)
    {
        _agentID = data.AgentID;
        _itemRankings = data.ItemRankings;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
