using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPlant : MonoBehaviour
{
    public GameObject summonObj;
    public int summonAmount = 2;
    public Transform summonPositionParent;
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < summonAmount; i++)
        {
            var trans = Utils.RandomTransform(summonPositionParent);
            Instantiate(summonObj, trans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
