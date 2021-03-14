using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HUD : Singleton<HUD>
{
    public GameObject statsContent;
    public GameObject oneStatPrefab;
    public GameObject plantsContent;
    public GameObject plantButtonPrefab;
    public GameObject plantDetailPanel;
    public TMP_Text speedText;
    public GameObject gardenButton;
    [Header("garden")]
    public GameObject levelInfoPanel;

    [Header("camera")]
    public Camera treeCamera;
    public Camera gardenCamera;
    public float cameraMoveTime = 0.5f;

    public GameObject treePanel;
    public GameObject gardenPanel;

    float previousSpeed;

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

    public void showGardenButton()
    {
        gardenButton.SetActive(true);
    }

    public void MoveToGargen()
    {
        // The shortcuts way
        //transform.DOMove(new Vector3(2, 2, 2), 1);
        // The generic way
        //DOTween.To(() => transform.position, x => transform.position = x, new Vector3(2, 2, 2), 1);

        if (PlantsManager.Instance.maintree.isFinished())
        {
            GardenManager.Instance.finishTree(PlantsManager.Instance.maintree.type);
        }

        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, gardenCamera.orthographicSize, cameraMoveTime).SetUpdate(true);
        DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, gardenCamera.transform.position, cameraMoveTime).SetUpdate(true);
        treePanel.SetActive(false);
        gardenPanel.SetActive(true);
        previousSpeed = Time.timeScale;
        Time.timeScale = 0;
    }

    public void showLevelInfo(HelperPlantType type)
    {
        levelInfoPanel.SetActive(true);

        levelInfoPanel.GetComponent<LevelInfo>().UpdateInfo(type);
    }

    public void hideLevelInfo()
    {
        levelInfoPanel.SetActive(false);

    }

    public void MoveToTree()
    {
        // The shortcuts way
﻿﻿﻿﻿﻿﻿﻿﻿//transform.DOMove(new Vector3(2, 2, 2), 1);
﻿﻿﻿﻿﻿﻿﻿﻿// The generic way
﻿﻿﻿﻿﻿﻿﻿﻿//DOTween.To(() => transform.position, x => transform.position = x, new Vector3(2, 2, 2), 1);

        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, treeCamera.orthographicSize, cameraMoveTime).SetUpdate(true);
        DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, treeCamera.transform.position, cameraMoveTime).SetUpdate(true);
        treePanel.SetActive(true);
        gardenPanel.SetActive(false);
        Time.timeScale = previousSpeed;

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
