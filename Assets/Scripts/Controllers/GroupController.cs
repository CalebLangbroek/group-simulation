using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour, IInitializable<GroupModel>
{

    [SerializeField]
    private GameObject _agentPrefab;

    private int _agentCount = 0;

    public void Initialize(GroupModel data)
    {
        _agentCount = data.AgentCount;


    }

    void Start()
    {
        for (int j = 0; j < _agentCount; j++)
        {
            GameObject agentInstance = Instantiate(_agentPrefab, Vector3.zero, new Quaternion());
            agentInstance.transform.parent = this.transform;
            agentInstance.transform.localPosition = new Vector3(j % 3 * 2, 0.6f, Mathf.Floor(j / 3) * 2);

            AgentController agentController = agentInstance.GetComponent<AgentController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
