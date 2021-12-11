using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GroupController : MonoBehaviour, IInitializable<GroupModel>
{

    [SerializeField]
    private GameObject _agentPrefab;

    private int _groupID = 0;

    private int _maxSteps = 0;

    private int _stepTimer = 0;

    private SimpleMultiAgentGroup _agentGroup;

    private Dictionary<int, AgentController> _agents = new Dictionary<int, AgentController>();

    private Dictionary<string, int> _proposedItems = new Dictionary<string, int>();

    private Dictionary<string, int> _acceptedItems = new Dictionary<string, int>();

    public void Initialize(GroupModel data)
    {
        _maxSteps = GroupSimulationSettings.Instance.MaxSimulationSteps;
        _agentGroup = new SimpleMultiAgentGroup();
        _groupID = data.GroupID;

        for (int i = 0; i < data.AgentItemRankings.Count; i++)
        {
            GameObject agentInstance = Instantiate(_agentPrefab, Vector3.zero, new Quaternion());
            agentInstance.transform.parent = this.transform;
            agentInstance.transform.localPosition = new Vector3(0, 0.6f, 0);
            AgentController agentController = agentInstance.GetComponent<AgentController>();
            agentController.Initialize(new AgentModel(data.GroupID, data.AgentItemRankings[i].ParticipantID, data.AgentItemRankings[i].Rankings, OnAgentPropose, OnAgentAccept, OnAgentReject));
            _agentGroup.RegisterAgent(agentController);
            _agents.Add(data.AgentItemRankings[i].ParticipantID, agentController);
        }

        ResetSimulation();
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
        _proposedItems = new Dictionary<string, int>();
        _acceptedItems = new Dictionary<string, int>();
        UpdateAgentsObservations();

        foreach (var agent in _agents)
        {
            _agentGroup.RegisterAgent(agent.Value);
        }
    }

    private void OnAgentPropose(int agentID, ItemRanking itemRanking)
    {
        // make sure we haven't already accepted or proposed this item or its ranking
        if (IsItemAcceptedOrProposed(itemRanking))
        {
            return;
        }

        // reward agent for proposing
        _agents[agentID].AddReward(GroupSimulationSettings.Instance.AgentProposeAward);

        // add to list of proposals
        _proposedItems.Add(itemRanking.Name, itemRanking.Ranking);

        UpdateAgentsObservations();
    }

    private void OnAgentAccept(int agentID, ItemRanking itemRanking)
    {
        // make sure the item ranking has been proposed and hasn't been accepted
        if (IsItemAcceptedOrNotProposed(itemRanking))
        {
            return;
        }

        // reward agent for accepting
        _agents[agentID].AddReward(GroupSimulationSettings.Instance.AgentAcceptAward);

        // move item from proposed to accepted
        _proposedItems.Remove(itemRanking.Name);
        _acceptedItems.Add(itemRanking.Name, itemRanking.Ranking);

        UpdateAgentsObservations();

        // reward the whole group based on how close the ranking was to the expert ranking
        float itemCount = GroupSimulationSettings.Instance.ExpertItemRankings.Count;
        ItemRanking expertRanking = GroupSimulationSettings.Instance.ExpertItemRankings.Find(x => x.Name.Equals(itemRanking.Name));
        _agentGroup.AddGroupReward((itemCount - (float)Mathf.Abs(expertRanking.Ranking - itemRanking.Ranking)) / itemCount * (float)GroupSimulationSettings.Instance.MaxReward / itemCount);

        // end training if all items are ranked
        if (_acceptedItems.Count == GroupSimulationSettings.Instance.ExpertItemRankings.Count)
        {
            _agentGroup.EndGroupEpisode();
            ResetSimulation();
        }
    }

    private void OnAgentReject(int agentID, ItemRanking itemRanking)
    {
        // make sure the item ranking has been proposed and hasn't been accepted
        if (IsItemAcceptedOrNotProposed(itemRanking))
        {
            return;
        }

        // penalize agent for rejecting
        _agents[agentID].AddReward(GroupSimulationSettings.Instance.AgentRejectAward);

        _proposedItems.Remove(itemRanking.Name);

        UpdateAgentsObservations();
    }

    private bool IsItemAcceptedOrProposed(ItemRanking itemRanking)
    {
        return _proposedItems.ContainsKey(itemRanking.Name) || _acceptedItems.ContainsKey(itemRanking.Name) || _proposedItems.ContainsValue(itemRanking.Ranking) || _acceptedItems.ContainsValue(itemRanking.Ranking);
    }

    private bool IsItemAcceptedOrNotProposed(ItemRanking itemRanking)
    {
        return !_proposedItems.ContainsKey(itemRanking.Name) || _acceptedItems.ContainsKey(itemRanking.Name);
    }

    private void UpdateAgentsObservations()
    {
        foreach (var agent in _agents)
        {
            agent.Value.UpdateObservations(ConvertItemDictToFloatList(_proposedItems), ConvertItemDictToFloatList(_acceptedItems));
        }
    }

    private List<float> ConvertItemDictToFloatList(Dictionary<string, int> items)
    {
        List<float> itemsFloatList = new List<float>(GroupSimulationSettings.Instance.ExpertItemRankings.Count);
        foreach (var expertRanking in GroupSimulationSettings.Instance.ExpertItemRankings)
        {
            if (items.ContainsKey(expertRanking.Name))
            {
                itemsFloatList.Add(1);
            }
            else
            {
                itemsFloatList.Add(0);
            }
        }
        return itemsFloatList;
    }
}
