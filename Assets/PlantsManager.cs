using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PlantProperty {s,p,n,water,bee,pest };
public enum HelperPlantType { red, yellow, blue};
public class PlantsManager : Singleton<PlantsManager>
{

    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantCost = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
    };
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantKeepCost = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
    };
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantProd = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() {
            { PlantProperty.p, 1 },
            {PlantProperty.water, 1 },
        }},
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.s, 1 } } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 1 } } },
    };

    public Dictionary<PlantProperty, int> currentResource = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s, 0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 100 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },

    };

    public Dictionary<PlantProperty, string> resourceName = new Dictionary<PlantProperty, string>() {
        { PlantProperty.p, "Phosphorous" },
        { PlantProperty.s, "Potassium"  },
        { PlantProperty.n, "Nitrogen" },
        { PlantProperty.water, "Water" },
        { PlantProperty.bee, "Bee Attrack" },
        { PlantProperty.pest, "Pest Attrack" },

    };

    public Dictionary<HelperPlantType, string> plantName = new Dictionary<HelperPlantType, string>() {
        { HelperPlantType.red, "Red" },
        { HelperPlantType.yellow, "Yellow" },
        { HelperPlantType.blue, "Blue" },

    };

    public Dictionary<PlantProperty, int> currentResourceRate = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s,0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 0 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },
    };

    public Dictionary<PlantProperty, int> baseResourceRate = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 1 },
        { PlantProperty.s, 1 },
        { PlantProperty.n, 1 },
        { PlantProperty.water, 5 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },
    };

    public Dictionary<GameObject, bool> isPlantUnlocked = new Dictionary<GameObject, bool>()
    {

    };
    public List<GameObject> helperPlantList;

    Dictionary<int, HelperPlant> plantedPlant = new Dictionary<int, HelperPlant>();
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateRate();
    }

    public bool IsPlantable(HelperPlant plant)
    {
        var prodDictionary = helperPlantCost[plant.type];
        foreach (var pair in prodDictionary)
        {
            if( currentResource[ pair.Key] < pair.Value)
            {
                return false;
            }
        }
        return true;
    }

    void ReduceResource(Dictionary<PlantProperty, int> origin, Dictionary<PlantProperty, int> reduce)
    {
        foreach (var pair in reduce)
        {
            origin[pair.Key] -= pair.Value;
        }
    }

    public void Purchase(GameObject plantPrefab)
    {
        ReduceResource(currentResource, helperPlantCost[plantPrefab.GetComponent<HelperPlant>().type]);
        GameObject spawnInstance = Instantiate(plantPrefab);
    }

    public void AddPlant(HelperPlant newPlant)
    {
        plantedPlant[0] = newPlant;
        UpdateRate();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 1)
        {
            currentTime -= 1;
            foreach (var pair in currentResourceRate)
            {
                currentResource[pair.Key] += pair.Value;
            }
        }
    }

    public void UpdateRate()
    {
        foreach (var key in currentResourceRate.Keys.ToArray<PlantProperty>())
        {
            currentResourceRate[key] = baseResourceRate[key];
        }
        foreach (var pair in plantedPlant)
        {
            var plant = pair.Value;
            if (plant.isAlive)
            {
                var prodDictionary = helperPlantProd[plant.type];
                foreach (var pairI in prodDictionary)
                {
                    currentResourceRate[pairI.Key] += pairI.Value;
                }

                var keepCostDictionary = helperPlantKeepCost[plant.type];
                foreach (var pairI in keepCostDictionary)
                {
                    currentResourceRate[pairI.Key] -= pairI.Value;
                }
            }
        }
    }
}
