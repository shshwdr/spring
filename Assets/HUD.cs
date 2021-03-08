using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject statsContent;
    public GameObject oneStatPrefab;
    PlantsManager plantManager;
    // Start is called before the first frame update
    void Start()
    {
        plantManager = PlantsManager.Instance;
        foreach (var pair in plantManager.currentResource)
        {
            GameObject oneStatInstance = Instantiate(oneStatPrefab);
            OneStatHud oneStatHud = oneStatInstance.GetComponent<OneStatHud>();
            oneStatHud.init(plantManager.resourceName[pair.Key], pair.Value, plantManager.currentResourceRate[pair.Key]);
            oneStatInstance.transform.parent = statsContent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
