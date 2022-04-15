﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantsButton : MonoBehaviour
{
    [HideInInspector]
    public GameObject spawnPlantPrefab;

    public TMP_Text name;
    public Image image;
    [HideInInspector]
    public HelperPlant helperPlant;
    HUD hud;
    bool previousPlantableState = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(GameObject plant,HUD h)
    {
        spawnPlantPrefab = plant;
        helperPlant = plant.GetComponent<HelperPlant>();
        name.text = PlantsManager.Instance.plantName[helperPlant.type];
        image.sprite = plant.GetComponent<HelperPlant>().iconSprite;
        image.color = plant.GetComponent<SpriteRenderer>().color;
        hud = h;

    }

    private void OnMouseDown()
    {
        SpawnPlant();
       // PlantsManager.Instance.shadowCollider.gameObject.SetActive(true);
    }
    public void SpawnPlant()
    {
        //try to purchase
        if (PlantsManager.Instance.IsPlantable(helperPlant.type))
        {
            //PlantsManager.Instance.Purchase(spawnPlantPrefab);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //PlantsManager.Instance.shadowCollider.gameObject.SetActive(true);
            GameObject spawnInstance = Instantiate(spawnPlantPrefab, mousePosition,Quaternion.identity,PlantsManager.Instance.allInTreeGame);
        }

    }

    public void PointerEnter()
    {
        hud.ShowPlantDetail(gameObject);
    }
    public void PointerExit()
    {
        hud.HidePlantDetail();
    }
    // Update is called once per frame
    void Update()
    {
        var currentPlantableState = PlantsManager.Instance.IsPlantable(helperPlant.type);
        if (currentPlantableState)
        {
            GetComponent<Button>().interactable = true;
            if (!previousPlantableState)
            {

                TutorialManager.Instance.canPurchasePlant(helperPlant.type);
            }
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
        previousPlantableState = currentPlantableState;
    }
}
