using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantDetail : MonoBehaviour
{
    public TMP_Text stats;
    PlantsManager plantManager;
    public TMP_Text actionText;
    // Start is called before the first frame update
    void Start()
    {
        plantManager = PlantsManager.Instance;
        gameObject.SetActive(false);
    }
    public void updateValue(GameObject plant)
    {
        stats.text = "";
        var plantButton = plant.GetComponent<PlantsButton>();
        if (plantButton)
        {
            //this is a plant button
            var helperPlant = plantButton.helperPlant;
            stats.text += getName(helperPlant);
            stats.text += getOnetimeCost(helperPlant);
            stats.text += getDurationCost(helperPlant);
            stats.text += getProduction(helperPlant);
            if (!plantManager.hasSlot())
            {

                actionText.text = "Out of slots";
            }
            else
            {
                if (plantManager.IsPlantable(helperPlant.type))
                {

                    actionText.text = "Click to plant";
                }
                else
                {

                    actionText.text = "Insufficient Resource";
                }
            }
        }
        else
        {
            var maintree = plant.GetComponent<MainTree>();
            if (maintree)
            {
                stats.text += getName(maintree);
                stats.text += getDurationCost(maintree);
                stats.text += getProduction(maintree);
                stats.text += getUpgrade(maintree);
                if (maintree.isAtMaxLevel()) {
                    actionText.text = "\nIs At Max Level. Attract bees and wait for fruit.";
                }
                else
                {
                    if (maintree.upgradable())
                    {

                        actionText.text = "Click to upgrade";
                    }
                    else
                    {

                        actionText.text = "Can't Upgrade";
                    }
                }
            }
            else
            {
                //this is planted plant
                //stats.text += getOnetimeCost(helperPlant);
                var helperPlant = plant.GetComponent<HelperPlant>();
                stats.text += getName(helperPlant);
                stats.text += getDurationCost(helperPlant);
                stats.text += getProduction(helperPlant);
                actionText.text = "Click to remove";

            }
        }
    }

    string getProduction(HelperPlant plant)
    {
        var prodDictionary = plantManager.helperPlantProd[plant.type];
        if (prodDictionary.Count == 0)
        {
            return "";
        }
        string res = "\nProduce:\n"; 
        foreach (var pair in prodDictionary)
        {
            res += plantManager.resourceName[pair.Key]+"\t"+ pair.Value.ToString();
            res += "\n";
        }
        return res;
    }

    string InsufficientResourcePrefix = "<color=#FF0000>";
    string InsufficientResourceSurfix = "</color>";
    string getUpgrade(MainTree plant)
    {
        if (plant.isAtMaxLevel())
        {
            return "\n";
        }

        string res = "\nUpgrade Cost:\n";
        var prodDictionary = plantManager.helperPlantCost[plant.nextLevelType()];
        foreach (var pair in prodDictionary)
        {
            bool isResourceAvailable = plantManager.IsResourceAvailable(pair.Key, pair.Value);
            res += isResourceAvailable ? "" : InsufficientResourcePrefix;
            res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
            res += isResourceAvailable ? "" : InsufficientResourceSurfix;
            res += "\n";
        }
        res += "\nNext Level Keep Cost:\n";
        var keepCostDictionary = plantManager.helperPlantKeepCost[plant.nextLevelType()];
        foreach (var pair in keepCostDictionary)
        {
            
            res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
            res += "\n";
        }
        return res;
    }
    string getName(HelperPlant plant)
    {
        return plantManager.plantName[plant.type]+"\n";
    }
    string getOnetimeCost(HelperPlant plant)
    {
        string res = "\nOne Time Cost:\n";
        var prodDictionary = plantManager.helperPlantCost[plant.type];
        foreach (var pair in prodDictionary)
        {
            bool isResourceAvailable = plantManager.IsResourceAvailable(pair.Key, pair.Value);
            res += isResourceAvailable ? "" : InsufficientResourcePrefix;
            res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
            res += isResourceAvailable ? "" : InsufficientResourceSurfix;
            res += "\n";
        }
        return res;
    }

    string getDurationCost(HelperPlant plant)
    {
        string res = "\nDuration Cost\n";
        var prodDictionary = plantManager.helperPlantKeepCost[plant.type];
        foreach (var pair in prodDictionary)
        {
            res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
            res += "\n";
        }
        return res;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
