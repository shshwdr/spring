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

    private void OnMouseDown()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
