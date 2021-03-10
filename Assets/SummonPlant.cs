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
        int i = 0;
        foreach(Transform po in summonPositionParent)
        {
            i++;
            Instantiate(summonObj, po);
            if (i >= summonAmount)
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
