using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperInsect : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        HelperInsectManager.Instance.Add(this);
        base.Start();
    }



    public override void die()
    {
        HelperInsectManager.Instance.helperInsects.Remove(this);
        base.die();

    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
