using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : HelperPlant
{

    // Start is called before the first frame update
    protected override void Start()
    {
        PlantsManager.Instance.maintree = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
