using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : Singleton<BeeManager>
{
    public GameObject bee;
    public Transform beeSpawnPositionParent;
    List<Transform> beeSpawnPositions = new List<Transform>();
    public float beeGenerateTime = 5;
    float currentBeeGenerateTime = 0;
    [HideInInspector]
    public int currentBeeCount = 0;
    public int maxBeeCount = 5;

    public Transform getClosestSpawnTransform(Vector3 pos)
    {
        float dis = 1000;
        Transform res = null;
        foreach (Transform p in beeSpawnPositionParent)
        {
            if((pos - p.position).magnitude < dis)
            {
                dis = (pos - p.position).magnitude;
                res = p;
            }
        }
        return res;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform p in beeSpawnPositionParent)
        {
            beeSpawnPositions.Add(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentBeeGenerateTime += Time.deltaTime;
        if(currentBeeGenerateTime>=beeGenerateTime && currentBeeCount< maxBeeCount)
        {
            currentBeeGenerateTime = 0;
            currentBeeCount++;
            Instantiate(bee, Utils.RandomTransform(beeSpawnPositionParent).position,Quaternion.identity);
        }
    }
}
