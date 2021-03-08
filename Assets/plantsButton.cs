using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantsButton : MonoBehaviour
{
    public GameObject spawnPlantPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnPlant()
    {
        GameObject spawnInstance = Instantiate(spawnPlantPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
