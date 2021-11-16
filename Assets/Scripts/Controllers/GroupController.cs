using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GroupController : MonoBehaviour, IInitializable<GroupModel>
{

    [SerializeField]
    private GameObject _agentPrefab;

    [SerializeField]
    private int _maxSteps = 2500;

    private int _stepTimer = 0;

    private int _agentCount = 0;

    private SimpleMultiAgentGroup _agentGroup;

    private List<Agent> _agents;

    private List<ItemRanking> _proposedItems = new List<ItemRanking>();

    private List<ItemRanking> _acceptedItems = new List<ItemRanking>();

    public void Initialize(GroupModel data)
    {
        _agentCount = data.AgentCount;

        _agentGroup = new SimpleMultiAgentGroup();
        _agents = new List<Agent>();

        for (int i = 0; i < _agentCount; i++)
        {
            GameObject agentInstance = Instantiate(_agentPrefab, Vector3.zero, new Quaternion());
            agentInstance.transform.parent = this.transform;
            agentInstance.transform.localPosition = new Vector3(i % 3 * 2, 0.6f, Mathf.Floor(i / 3) * 2);
            AgentController agentController = agentInstance.GetComponent<AgentController>();
            agentController.Initialize(new AgentModel(data.GroupID, i, data.AgentItemRankings[i].Rankings, OnAgentPropose, OnAgentAccept, OnAgentReject));
            _agentGroup.RegisterAgent(agentController);
            _agents.Add(agentController);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _stepTimer++;

        if (_stepTimer > _maxSteps && _maxSteps != 0)
        {
            _agentGroup.GroupEpisodeInterrupted();
            ResetSimulation();
        }
    }

    private void ResetSimulation()
    {
        _stepTimer = 0;
        _proposedItems = new List<ItemRanking>();
        _acceptedItems = new List<ItemRanking>();

        foreach (var agentController in _agents)
        {
            _agentGroup.RegisterAgent(agentController);
        }
    }

    private void OnAgentPropose(int agentID, ItemRanking itemRanking)
    {
        // reward agent for proposing
        _agents[agentID].AddReward(1.0f);

        // add to list of proposals
        _proposedItems.Add(itemRanking);
    }

    private void OnAgentAccept(int agentID, int itemID)
    {
        // reward agent for accepting
        _agents[agentID].AddReward(1.0f);

        // reward the whole group based 
        _agentGroup.AddGroupReward(10.0f);

        // move item from proposed to accepted
        if (_proposedItems.Count != 0)
        {
            _acceptedItems.Add(_proposedItems[_proposedItems.Count - 1]);
            _proposedItems.RemoveAt(_proposedItems.Count - 1);
        }

        // end training if all items are ranked
        if (_acceptedItems.Count == 15)
        {
            _agentGroup.EndGroupEpisode();
            ResetSimulation();
        }
    }

    private void OnAgentReject(int agentID, int itemID)
    {
        // penalize agent for rejecting
        _agents[agentID].AddReward(-1.0f);
    }
}
