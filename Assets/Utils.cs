using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static Transform RandomTransform(Transform tranformParent)
    {
        List<Transform> trans = new List<Transform>();
        foreach(Transform c in tranformParent)
        {
            trans.Add(c);
        }
        return RandomTransform(trans);
    }

    public static Transform RandomTransform(List<Transform> transforms)
    {
        int randomValue = Random.Range(0, transforms.Count);
        return transforms[randomValue];
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
