using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    void Awake()
    {
        // read item rankings from file
        TextAsset jsonRanking = Resources.Load<TextAsset>("Item Rankings");
        ItemRankingCollection itemRankings = JsonUtility.FromJson<ItemRankingCollection>(jsonRanking.text);


        Debug.Log(itemRankings);
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize agents
    }

    // Update is called once per frame
    void Update()
    {

    }
}
