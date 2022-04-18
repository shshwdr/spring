﻿using System.Collections;
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
    public TMP_Text pauseText;
    public GameObject gardenButton;
    public List<Sprite> propertyImage;
    public bool isInGarden = false;
    public List<Transform> propertyResourceTransform = new List<Transform>(6);
    public bool isGameover;
    public GameObject gameoverPanel;
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
    public Dictionary<PlantProperty, OneStatHud> hudByProperty;
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
        UpdatePlantButtons();
    }

    public void UpdatePlantButtons()
    {
        foreach (Transform go in plantsContent.transform)
        {
            Destroy(go.gameObject);
        }
            foreach (var go in plantManager.helperPlantList)
        {
            if ((!plantManager.isPlantUnlocked.ContainsKey(go.GetComponent<HelperPlant>().type) || plantManager.isPlantUnlocked[go.GetComponent<HelperPlant>().type]) || plantManager.unlockAllFlowers)
            {
                foreach (var plantType in plantManager.levelToPlants[plantManager.mainTreePrefab.GetComponent<MainTree>().upgradeList[0]])
                {
                    if(plantType == go.GetComponent<HelperPlant>().type)
                    {

                        GameObject buttonInstance = Instantiate(plantButtonPrefab, plantsContent.transform);
                        PlantsButton plantButtonInstance = buttonInstance.GetComponent<PlantsButton>();
                        plantButtonInstance.init(go, this);
                    }
                }

            }
        }
    }

    public void ShowPlantDetail(GameObject plant)
    {
        DOTween.To(() => plantDetailPanel.GetComponent<CanvasGroup>().alpha, x => plantDetailPanel.GetComponent<CanvasGroup>().alpha = x, 1, detailFadeInTime);

        
        //plantDetailPanel.SetActive(true);
        plantDetailPanel.GetComponent<PlantDetail>().updateValue(plant);
    }

    public void changeSpeed()
    {
        currentSpeedId += 1;
        if (currentSpeedId >= speedList.Count)
        {
            currentSpeedId = 1;
        }
        resumeSpeed();

    }

    public void resumeSpeed()
    {
        if (isInGarden)
        {
            return;
        }
        var speed = speedList[currentSpeedId];
        Time.timeScale = speed;
        isPaused = false;
        speedText.text = speed + "x speed";
        pauseText.text = "Pause";
    }

    public void togglePause()
    {
        if (isInGarden)
        {
            return;
        }
        if (isGameover)
        {
            return;
        }
        isPaused = !isPaused;
        if (isPaused)
        {

            Time.timeScale = 0;
            speedText.text = 0 + "x speed";
            pauseText.text = "Play";
        }
        else
        {
            resumeSpeed();
            pauseText.text = "Pause";
        }
    }

    public void showGardenButton()
    {
        gardenButton.SetActive(true);
    }

    public void MoveToGargen()
    {
        
        if (PlantsManager.Instance.maintree.isFinished() || GardenManager.Instance.alwaysUpdateTree)
        {
            GardenManager.Instance.finishTree(PlantsManager.Instance.maintree.upgradeList[0]);
        }

        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, gardenCamera.orthographicSize, cameraMoveTime).SetUpdate(true);
        DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, gardenCamera.transform.position, cameraMoveTime).SetUpdate(true);
        treePanel.SetActive(false);
        gardenPanel.SetActive(true);
        previousSpeed = Time.timeScale;
        Time.timeScale = 0;
        isInGarden = true;
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

    public void clearLevel()
    {
        foreach (Transform tt in PlantsManager.Instance.allInTreeGame)
        {

            Destroy(tt.gameObject);

        }
        PlantsManager.Instance.ClearResource();
        BirdManager.Instance.ResetBird();
        PestManager.Instance.Clear();
        BeeManager.Instance.Clear();
        ResourceAutoGeneration.Instance.Clear();
        Instantiate(PlantsManager.Instance.mainTreePrefab, PlantsManager.Instance.allInTreeGame);
        gameoverPanel.SetActive(false);

        isGameover = false;

        resumeSpeed();
    }

    public void Gameover()
    {
        gameoverPanel.SetActive(true);
        togglePause();
        isGameover = true;
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

        isInGarden = false;
        resumeSpeed();
        UpdatePlantButtons();
    }
    float detailFadeInTime = 0.5f;
    public void HidePlantDetail()
    {

        DOTween.To(() => plantDetailPanel.GetComponent<CanvasGroup>().alpha, x => plantDetailPanel.GetComponent<CanvasGroup>().alpha = x, 0, detailFadeInTime);
        //plantDetailPanel.SetActive(false);
    }
        // Update is called once per frame
        void Update()
    {
        foreach (var pair in plantManager.currentResource)
        {

            OneStatHud oneStatHud = hudByProperty[pair.Key];
            if(!plantManager.isResourceUnlocked.ContainsKey(pair.Key))
            {
                oneStatHud.gameObject.SetActive(false);
                oneStatHud.transform.position = statsContent.transform.position;
            }
            else
            {

                oneStatHud.gameObject.SetActive(true);
            }
            oneStatHud.init(plantManager.resourceName[pair.Key], propertyImage[(int)pair.Key], pair.Value);
        }
    }
}
