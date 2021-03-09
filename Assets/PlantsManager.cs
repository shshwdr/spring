using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PlantProperty { s, p, n, water, bee, pest };
public enum HelperPlantType { red, yellow, blue, purple, appleTree1, appleTree2, appleTree3 };
public class PlantsManager : Singleton<PlantsManager>
{
    public bool ignoreResourcePlant = true;
    public MainTree maintree;
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantCost;
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantKeepCost;
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantProd;

    public Dictionary<PlantProperty, int> currentResource;

    public Dictionary<PlantProperty, string> resourceName;

    public Dictionary<HelperPlantType, string> plantName;

    public Dictionary<PlantProperty, int> currentResourceRate;

    public Dictionary<PlantProperty, int> baseResourceRate;

    public Transform plantsSlotParent;
    List<PlantSlot> plantSlots;

    public int unlockedSlot = 2;


    public Dictionary<GameObject, bool> isPlantUnlocked;
    public List<GameObject> helperPlantList;

    Dictionary<int, HelperPlant> plantedPlant = new Dictionary<int, HelperPlant>();
    float currentTime = 0;

    void initValues()
    {
        isPlantUnlocked = new Dictionary<GameObject, bool>();
        currentResourceRate = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s,0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 0 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },
    };
        baseResourceRate = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s, 0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 10 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },
    };
        plantName = new Dictionary<HelperPlantType, string>() {
        { HelperPlantType.red, "Red Flower" },
        { HelperPlantType.yellow, "Yellow Flower" },
        { HelperPlantType.blue, "Blue Flower" },
        { HelperPlantType.purple, "Purple Flower" },
        { HelperPlantType.appleTree1, "Apple Tree - child" },
        { HelperPlantType.appleTree2, "Apple Tree - middle" },
        { HelperPlantType.appleTree3, "Apple Tree - flower" },

    };
        resourceName = new Dictionary<PlantProperty, string>() {
        { PlantProperty.p, "calcium" },
        { PlantProperty.s, "Potassium"  },
        { PlantProperty.n, "Nitrogen" },
        { PlantProperty.water, "Water" },
        { PlantProperty.bee, "Bee Attrack" },
        { PlantProperty.pest, "Pest Attrack" },

    };
        currentResource = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s, 0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 100 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },

    };
        helperPlantProd = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() {
            { PlantProperty.p, 1 },
        }},
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.s, 1 } } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 1 } } },
        {HelperPlantType.purple,new Dictionary<PlantProperty, int>() { { PlantProperty.bee, 1 }, { PlantProperty.s, 1 } } },
        {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() {  } },
        {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() { } },
        {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { } },
    }; 
        helperPlantKeepCost = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
        {HelperPlantType.purple,new Dictionary<PlantProperty, int>() {  { PlantProperty.n, 1 } } },
        {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 1 } } },
        {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 2 }, { PlantProperty.n, 2 } } },
        {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 3 } , { PlantProperty.n, 3 } , { PlantProperty.s, 2 } } },
    }; helperPlantCost = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
        {HelperPlantType.purple,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n, 5 }, { PlantProperty.p, 5 } } },
        {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 } } },
        {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n,10}, { PlantProperty.p, 10 } } },
        {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.s, 20 }, { PlantProperty.p, 50 } } },
    };
    }
    // Start is called before the first frame update
    private void Awake()
    {
        initValues();
        plantSlots = new List<PlantSlot>();
        for (int i = 0; i < plantsSlotParent.childCount; i++)
        {
            plantSlots.Add(plantsSlotParent.GetChild(i).GetComponent<PlantSlot>());
        }
    }
    void Start()
    {
        UpdateRate();
    }

    public int firstAvailableSlot()
    {
        for (int i = 0; i < unlockedSlot; i++)
        {
            var slot = plantSlots[i];
            if (slot.isAvailable)
            {
                return i;
            }
        }
        return -1;
    }

    public bool hasSlot()
    {
        return firstAvailableSlot() != -1;
    }
    public bool IsResourceAvailable(PlantProperty property, int value)
    {
        return currentResource[property] >= value;
    }

    public bool IsResourceRateAvailable(PlantProperty property, int value)
    {
        return currentResourceRate[property] >= value;
    }
    public bool IsPlantable(HelperPlantType type, bool ignoreSlot = false)
    {
        if (!ignoreResourcePlant)
        {
            var prodDictionary = helperPlantCost[type];
            foreach (var pair in prodDictionary)
            {
                if (currentResource[pair.Key] < pair.Value)
                {
                    return false;
                }
            }
        }
        return ignoreSlot || hasSlot();
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
        var slotId = firstAvailableSlot();
        if (slotId != -1)
        {
            var slot = plantSlots[slotId];
            ReduceCostForType(plantPrefab.GetComponent<HelperPlant>().type);
            GameObject spawnInstance = Instantiate(plantPrefab, slot.transform);
            slot.isAvailable = false;
            plantedPlant[slotId] = spawnInstance.GetComponent<HelperPlant>();
            spawnInstance.GetComponent<HelperPlant>().slot = slotId;
        }
    }

    public void ReduceCostForType(HelperPlantType type)
    {
        ReduceResource(currentResource, helperPlantCost[type]);
    }

    public void Remove(GameObject plantPrefab)
    {
        int slotId = plantPrefab.GetComponent<HelperPlant>().slot;
        plantedPlant[slotId] = null;
        Destroy(plantPrefab);

        plantSlots[slotId].isAvailable = true;
        UpdateRate();
    }

    public void AddPlant(HelperPlant newPlant)
    {
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
            if (plant && plant.isAlive)
            {
                UpdateOneTypeRate(plant.type);
            }
        }
        if (maintree)
        {

            UpdateOneTypeRate(maintree.type);
        }
    }

    void UpdateOneTypeRate(HelperPlantType type)
    {
        var prodDictionary = helperPlantProd[type];
        foreach (var pairI in prodDictionary)
        {
            currentResourceRate[pairI.Key] += pairI.Value;
        }

        var keepCostDictionary = helperPlantKeepCost[type];
        foreach (var pairI in keepCostDictionary)
        {
            currentResourceRate[pairI.Key] -= pairI.Value;
        }
    }
}
