using System.Collections.Generic;

public class GroupModel
{
    private int _agentCount = 0;

    private List<IndividualItemRanking> _agentItemRankings = null;

    public GroupModel(int agentCount, List<IndividualItemRanking> agentItemRankings)
    {
        AgentCount = agentCount;
        AgentItemRankings = agentItemRankings;
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
