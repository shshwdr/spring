using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pest : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!target)
        {
            target = getClosestTransform(PlantsManager.Instance.plantsList());
        }
        base.Update();
    }
}
