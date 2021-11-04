using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour, IInitializable<GroupModel>
{

    [SerializeField]
    private GameObject agentPrefab;

    private int agentCount = 0;

    public void Initialize(GroupModel data)
    {
        this.agentCount = data.AgentCount;
        

    }

    void Start()
    {
        for (int j = 0; j < agentCount; j++)
        {
            GameObject agentInstance = Instantiate(agentPrefab, Vector3.zero, new Quaternion());
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
