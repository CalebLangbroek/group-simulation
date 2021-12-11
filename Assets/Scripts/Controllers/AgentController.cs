using System;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using UnityEngine;

public class AgentController : Agent, IInitializable<AgentModel>
{
    [SerializeField]
    private List<Material> _materials;

    private int _agentID;
    private List<ItemRanking> _itemRankings = new List<ItemRanking>();
    private List<float> _proposedItems = new List<float>();
    private List<float> _acceptedItems = new List<float>();
    private Action<int, ItemRanking> _onProposeAction;
    private Action<int, ItemRanking> _onAcceptAction;
    private Action<int, ItemRanking> _onRejectAction;

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

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(_agentID, transform.localPosition.y, _agentID);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        int itemID = actions.DiscreteActions[1];
        int ranking = actions.DiscreteActions[2];

        ItemRanking itemRanking = new ItemRanking();
        itemRanking.Name = _itemRankings[itemID].Name;
        itemRanking.Ranking = ranking;

        switch (action)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    _onProposeAction.Invoke(_agentID, _itemRankings[itemID]);
                    break;
                }
            case 2:
                {
                    _onAcceptAction.Invoke(_agentID, _itemRankings[itemID]);
                    break;
                }
            case 3:
                {
                    _onRejectAction.Invoke(_agentID, _itemRankings[itemID]);
                    break;
                }
            default:
                {
                    Debug.LogError("Invalid Agent Action: " + action);
                    break;
                }
        }

        GetComponent<MeshRenderer>().material = _materials[action];
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_proposedItems);
        sensor.AddObservation(_acceptedItems);
    }

    public void UpdateObservations(List<float> proposedItems, List<float> acceptedItems)
    {
        _proposedItems = proposedItems;
        _acceptedItems = acceptedItems;
    }
}
