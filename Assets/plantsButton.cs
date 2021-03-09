using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void init(GameObject plant,HUD h)
    {
        spawnPlantPrefab = plant;
        helperPlant = plant.GetComponent<HelperPlant>();
        name.text = PlantsManager.Instance.plantName[helperPlant.type];
        image.sprite = plant.GetComponent<SpriteRenderer>().sprite;
        image.color = plant.GetComponent<SpriteRenderer>().color;
        hud = h;
    }
    public void SpawnPlant()
    {
        //try to purchase
        if (PlantsManager.Instance.IsPlantable(helperPlant.type))
        {
            PlantsManager.Instance.Purchase(spawnPlantPrefab);
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
        if (PlantsManager.Instance.IsPlantable(helperPlant.type))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
