using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPlant : MonoBehaviour
{
    public GameObject summonObj;
    public int summonAmount = 2;
    public Transform summonPositionParent;
    public List<GameObject> summoned = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < summonAmount; i++)
        {
            var trans = Utils.RandomTransform(summonPositionParent);
            var go = Instantiate(summonObj, trans);
            summoned.Add(go); 
        }
    }
    public void clean()
    {
        foreach(var go in summoned)
        {
            go.GetComponent<HPObject>().die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
