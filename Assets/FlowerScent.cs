using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScent : MonoBehaviour
{
    TrailRenderer trail;
    public TreeFlower treeFlower;

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        var num = trail.positionCount;
        if (num > 1)
        {

            Vector3[] TrailRecorded = new Vector3[num];
            trail.GetPositions(TrailRecorded);

            LayerMask mask = LayerMask.GetMask("Bee");
            RaycastHit2D hit = Physics2D.Linecast(TrailRecorded[0], TrailRecorded[num - 1], mask);
            if (hit && treeFlower)
            {
                hit.collider.GetComponent<Bee>().setTarget(treeFlower.transform);
            }
        }
    }
}
