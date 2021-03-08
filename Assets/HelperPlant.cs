using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPlant : MonoBehaviour
{
    public HelperPlantType type;
    [HideInInspector]
    public bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        PlantsManager.Instance.AddPlant(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
