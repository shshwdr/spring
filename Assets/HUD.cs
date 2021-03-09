using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    public GameObject statsContent;
    public GameObject oneStatPrefab;
    public GameObject plantsContent;
    public GameObject plantButtonPrefab;
    public GameObject plantDetailPanel;
    PlantsManager plantManager;
    Dictionary<PlantProperty, OneStatHud> hudByProperty;
    // Start is called before the first frame update
    void Start()
    {
        plantManager = PlantsManager.Instance;
        //init stats
        hudByProperty = new Dictionary<PlantProperty, OneStatHud>();
        foreach (var pair in plantManager.currentResource)
        {
            GameObject oneStatInstance = Instantiate(oneStatPrefab, statsContent.transform);
            OneStatHud oneStatHud = oneStatInstance.GetComponent<OneStatHud>();
            hudByProperty[pair.Key] = oneStatHud;
        }
        //init plant buttons
        foreach (var go in plantManager.helperPlantList)
        {
            if (!plantManager.isPlantUnlocked.ContainsKey(go) || plantManager.isPlantUnlocked[go])
            {
                GameObject buttonInstance = Instantiate(plantButtonPrefab,plantsContent.transform);
                PlantsButton plantButtonInstance = buttonInstance.GetComponent<PlantsButton>();
                plantButtonInstance.init(go,this);
                
            }
        }
    }

    public void ShowPlantDetail(GameObject plant)
    {
        plantDetailPanel.SetActive(true);
        plantDetailPanel.GetComponent<PlantDetail>().updateValue(plant);
    }

    public void HidePlantDetail()
    {
        plantDetailPanel.SetActive(false);
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
