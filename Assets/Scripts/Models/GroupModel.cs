using System.Collections.Generic;

public class GroupModel
{
    private int _agentCount = 0;

    private List<List<ItemRanking>> _agentItemRankings = null;

    public GroupModel(int agentCount, List<List<ItemRanking>> agentItemRankings)
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

    public List<List<ItemRanking>> AgentItemRankings
    {
        get => _agentItemRankings;
        private set
        {
            if(value.Count != AgentCount) {
                throw new System.Exception();
            }

            _agentItemRankings = value;
        }
    }

}
