using System;
using System.Collections.Generic;

public class AgentModel
{
    private int _groupID;
    private int _agentID;
    private List<ItemRanking> _itemRankings;
    private Action<int, ItemRanking> _onProposeAction;
    private Action<int, ItemRanking> _onAcceptAction;
    private Action<int, ItemRanking> _onRejectAction;

    public AgentModel(int groupID, int agentID, List<ItemRanking> itemRankings, Action<int, ItemRanking> onProposeAction, Action<int, ItemRanking> onAcceptAction, Action<int, ItemRanking> onRejectAction)
    {
        GroupID = groupID;
        AgentID = agentID;
        ItemRankings = itemRankings;
        OnProposeAction = onProposeAction;
        OnAcceptAction = onAcceptAction;
        OnRejectAction = onRejectAction;
    }

    public int GroupID
    {
        get => _groupID;
        private set
        {
            _groupID = value;
        }
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

    public Action<int, ItemRanking> OnProposeAction
    {
        get => _onProposeAction;
        private set
        {
            _onProposeAction = value;
        }
    }

    public Action<int, ItemRanking> OnAcceptAction
    {
        get => _onAcceptAction;
        private set
        {
            _onAcceptAction = value;
        }
    }

    public Action<int, ItemRanking> OnRejectAction
    {
        get => _onRejectAction;
        private set
        {
            _onRejectAction = value;
        }
    }
}
