﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : Singleton<CheatManager>
{
    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void unlimitedResource()
    {

        PlantsManager.Instance.ignoreResourcePlant = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {

            if (Input.GetKeyDown(KeyCode.U))
            {
                unlimitedResource();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                PestManager.Instance.lotsPest = true;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                BeeManager.Instance.lotsPest = true;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                PlantsManager.Instance.unlockAllFlowers = true;
                HUD.Instance.UpdatePlantButtons();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                TutorialManager.Instance.skipTutorial = true;
            }
        }
    }
}
