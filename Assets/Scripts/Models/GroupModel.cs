using System.Collections.Generic;

public class GroupModel
{
    private int _groupID;
    private List<IndividualItemRanking> _agentItemRankings;

    public GroupModel(int _groupID, List<IndividualItemRanking> agentItemRankings)
    {
        GroupID = _groupID;
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

    public List<IndividualItemRanking> AgentItemRankings
    {
        get => _agentItemRankings;
        private set
        {
            _agentItemRankings = value;
        }
    }

}
