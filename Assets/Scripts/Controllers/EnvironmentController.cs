using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    [SerializeField]
    private int _rows = 3;

    [SerializeField]
    private int _groupCount = 10;

    [SerializeField]
    private int _agentCount = 3;

    [SerializeField]
    private GameObject _groupPrefab;

    private List<ItemRanking> _expertItemRankings = null;

    void Awake()
    {
        // read expert item rankings from file
        TextAsset json = Resources.Load<TextAsset>("Expert Item Rankings");
        this._expertItemRankings = JsonUtility.FromJson<ItemRankingCollection>(json.text).Rankings;

        // read individuals' item rankings from file
        json = Resources.Load<TextAsset>("Individuals Item Rankings");
        List<IndividualItemRanking> individualItemRankings = JsonUtility.FromJson<IndividualItemRankingCollection>(json.text).IndividualRankings;

        // initialize agents
        for(int i = 0; i < _groupCount; i++) {
            Vector3 floorLocation = new Vector3(i % _rows * 15, 0, Mathf.Floor(i / _rows) * 15);
            GameObject groupInstance = Instantiate(_groupPrefab, floorLocation, new Quaternion());
            GroupModel groupModel = new GroupModel(_agentCount, individualItemRankings.GetRange(i * _groupCount, _agentCount));
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
