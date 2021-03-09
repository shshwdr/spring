using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : Singleton<HUD>
{
    public GameObject statsContent;
    public GameObject oneStatPrefab;
    public GameObject plantsContent;
    public GameObject plantButtonPrefab;
    public GameObject plantDetailPanel;
    public TMP_Text speedText;
    int currentSpeedId = 1;
    List<float> speedList = new List<float>() { 0.5f, 1, 2, 4 };
    bool isPaused = false;
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

    public void changeSpeed()
    {
        currentSpeedId += 1;
        if (currentSpeedId >= speedList.Count)
        {
            currentSpeedId = 0;
        }
        resumeSpeed();

    }

    public void resumeSpeed()
    {

        var speed = speedList[currentSpeedId];
        Time.timeScale = speed;
        speedText.text = speed + "x speed";
    }

    public void togglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {

            Time.timeScale = 0;
            speedText.text = 0 + "x speed";
        }
        else
        {
            resumeSpeed();
        }
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
