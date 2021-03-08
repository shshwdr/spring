using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantDetail : MonoBehaviour
{
    public TMP_Text stats;
    PlantsManager plantManager;
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
            //this is a button
            var helperPlant = plantButton.helperPlant;
            stats.text += getProduction(helperPlant);
        }
        else
        {
            //this is planted plant
        }
    }

    string getProduction(HelperPlant plant)
    {
        string res = ""; 
        var prodDictionary = plantManager.helperPlantProd[plant.type];
        foreach (var pair in prodDictionary)
        {
            res += plantManager.resourceName[pair.Key]+"\t"+ pair.Value.ToString();
            res += "\n";
        }
        return res;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
