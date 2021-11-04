using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    [SerializeField]
    private int rows = 3;

    [SerializeField]
    private int groupCount = 10;

    [SerializeField]
    private int agentCount = 3;

    [SerializeField]
    private GameObject groupPrefab;

    private List<ItemRanking> expertItemRankings = null;

    void Awake()
    {
        // read item rankings from file
        TextAsset json = Resources.Load<TextAsset>("Item Rankings");
        ItemRankingCollection itemRankingCollection = JsonUtility.FromJson<ItemRankingCollection>(json.text);

        this.expertItemRankings = itemRankingCollection.rankings;

        // initialize agents
        for(int i = 0; i < groupCount; i++) {
            Vector3 floorLocation = new Vector3(i % this.rows * 15, 0, Mathf.Floor(i / this.rows) * 15);
            GameObject groupInstance = Instantiate(this.groupPrefab, floorLocation, new Quaternion());
            GroupModel groupModel = new GroupModel(0, new List<List<ItemRanking>>());
            groupInstance.GetComponent<GroupController>().Initialize(groupModel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
