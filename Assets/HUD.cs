using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject statsContent;
    public GameObject oneStatPrefab;
    PlantsManager plantManager;
    Dictionary<PlantProperty, OneStatHud> hudByProperty;
    // Start is called before the first frame update
    void Start()
    {
        plantManager = PlantsManager.Instance;
        hudByProperty = new Dictionary<PlantProperty, OneStatHud>();
        foreach (var pair in plantManager.currentResource)
        {
            GameObject oneStatInstance = Instantiate(oneStatPrefab);
            OneStatHud oneStatHud = oneStatInstance.GetComponent<OneStatHud>();
            hudByProperty[pair.Key] = oneStatHud;
            oneStatInstance.transform.parent = statsContent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in plantManager.currentResource)
        {

            OneStatHud oneStatHud = hudByProperty[pair.Key];
            oneStatHud.init(plantManager.resourceName[pair.Key], pair.Value, plantManager.currentResourceRate[pair.Key]);
        }
    }
}
