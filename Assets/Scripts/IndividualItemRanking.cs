using System.Collections.Generic;

[System.Serializable]
public class IndividualItemRanking
{
    public int TeamID;
    public int ParticipantID;
    public int GroupSize;
    public List<ItemRanking> Rankings;
}
