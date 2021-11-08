using System;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using UnityEngine;

public class AgentController : Agent, IInitializable<AgentModel>
{
    private int _agentID;
    private List<ItemRanking> _itemRankings;
    private Action<int, ItemRanking> _onProposeAction;
    private Action<int, int> _onAcceptAction;
    private Action<int, int> _onRejectAction;

    public void Initialize(AgentModel data)
    {
        _agentID = data.AgentID;
        _itemRankings = data.ItemRankings;

        _onProposeAction = data.OnProposeAction;
        _onAcceptAction = data.OnAcceptAction;
        _onRejectAction = data.OnRejectAction;

        BehaviorParameters behaviorParameters = GetComponent<BehaviorParameters>();

        // all agents have the same team for now
        behaviorParameters.TeamId = 0;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        int itemID = actions.DiscreteActions[1];

        switch (action)
        {
            case 1:
                {
                    _onProposeAction.Invoke(_agentID, _itemRankings[itemID - 1]);
                    break;
                }
            case 2:
                {
                    _onAcceptAction.Invoke(_agentID, itemID);
                    break;
                }
            case 3:
                {
                    _onRejectAction.Invoke(_agentID, itemID);
                    break;
                }
            default:
                {
                    Debug.LogError("Invalid Agent Action");
                    break;
                }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }
}
