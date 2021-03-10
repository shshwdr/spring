using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestManager : Singleton<PestManager>
{

    public List<Pest> currentPests = new List<Pest>();
    // Start is called before the first frame update

    public List<Transform> pestsList()
    {
        List<Transform> res = new List<Transform>();
        foreach (var plantValue in currentPests)
        {
            res.Add(plantValue.transform);
        }
        return res;
    }
    void Start()
    {
        
    }

    public void AddPest(Pest p)
    {
        currentPests.Add(p);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
