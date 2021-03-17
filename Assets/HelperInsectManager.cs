using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperInsectManager : Singleton<HelperInsectManager>
{
    public List<HelperInsect> helperInsects = new List<HelperInsect>();

    //public List<Bee> beeList = new List<Bee>();
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public List<Transform> helperInsectsList()
    {
        List<Transform> res = new List<Transform>();
        foreach (var plantValue in helperInsects)
        {
            res.Add(plantValue.transform);
        }
        return res;
    }

    public List<Transform> helperInsectsNoTargetList()
    {
        List<Transform> res = new List<Transform>();
        foreach (var helperInsect in helperInsects)
        {
            if (helperInsect && !helperInsect.target)
            {
                res.Add(helperInsect.transform);
            }
        }
        return res;
    }

    public void Add(HelperInsect p)
    {
        helperInsects.Add(p);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
