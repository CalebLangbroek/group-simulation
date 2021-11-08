using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GroupController : MonoBehaviour, IInitializable<GroupModel>
{

    [SerializeField]
    private GameObject _agentPrefab;

    private int _agentCount = 0;

    private SimpleMultiAgentGroup _agentGroup;

    private List<Agent> _agents;

    public void Initialize(GroupModel data)
    {
        _agentCount = data.AgentCount;

        _agentGroup = new SimpleMultiAgentGroup();

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
    void Update()
    {

    }

    private void OnAgentPropose(int agentID, ItemRanking itemRanking)
    {
        // reward agent for proposing
        _agents[agentID].AddReward(1.0f);

        // add to list of proposals
    }

    private void OnAgentAccept(int agentID, int itemID)
    {
        // reward agent for accepting

    }

    private void OnAgentReject(int agentID, int itemID)
    {
        // penalize agent for rejecting



    }
}
