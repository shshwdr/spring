using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : HelperPlant
{
    public List<HelperPlantType> upgradeList;
    public List<int> slotCount = new List<int>() { 2, 4, 6 };
    int currentLevel = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        PlantsManager.Instance.maintree = this;
        PlantsManager.Instance.unlockedSlot = slotCount[currentLevel];
        type = upgradeList[currentLevel];
        base.Start();
    }

    public void Upgrade()
    {
        if (!upgradable())
        {
            return;
        }
        currentLevel += 1;
        type = upgradeList[currentLevel];
        PlantsManager.Instance.unlockedSlot = slotCount[currentLevel];
        PlantsManager.Instance.ReduceCostForType(type);
        PlantsManager.Instance.UpdateRate();

        HUD.Instance.ShowPlantDetail(gameObject);
    }

    public bool isAtMaxLevel()
    {
        return currentLevel == upgradeList.Count - 1;
    }

    public bool upgradable()
    {
        return PlantsManager.Instance.IsPlantable(nextLevelType(),true);
    }

    protected override void OnMouseDown()
    {
        if (isAtMaxLevel())
        {
            return;
        }
        Upgrade();
    }

    public HelperPlantType nextLevelType()
    {
        return upgradeList[currentLevel + 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
