using System.Collections.Generic;

public class AgentModel
{
    private int _teamID;
    private int _agentID;
    private int _groupSize;
    private List<ItemRanking> _itemRankings;

    public AgentModel(int agentID, List<ItemRanking> itemRankings)
    {
        AgentID = agentID;
        ItemRankings = itemRankings;
    }

    public int AgentID
    {
        get => _agentID;
        private set
        {
            _agentID = value;
        }
    }

    public List<ItemRanking> ItemRankings
    {
        get => _itemRankings;
        private set
        {
            _itemRankings = value;
        }
    }

}
