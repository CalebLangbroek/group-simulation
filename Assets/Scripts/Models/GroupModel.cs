using System.Collections.Generic;

public class GroupModel
{
    private int _groupID;
    private int _agentCount = 0;
    private List<IndividualItemRanking> _agentItemRankings = null;

    public GroupModel(int _groupID, int agentCount, List<IndividualItemRanking> agentItemRankings)
    {
        GroupID = _groupID;
        AgentCount = agentCount;
        AgentItemRankings = agentItemRankings;
    }

    public int GroupID
    {
        get => _groupID;
        private set
        {
            _groupID = value;
        }
    }

    public int AgentCount
    {
        get => _agentCount;
        private set
        {
            _agentCount = value;
        }
    }

    public List<IndividualItemRanking> AgentItemRankings
    {
        get => _agentItemRankings;
        private set
        {
            if (value.Count != AgentCount)
            {
                throw new System.Exception();
            }

            _agentItemRankings = value;
        }
    }

}
