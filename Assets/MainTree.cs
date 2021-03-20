using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : HelperPlant
{
    public int fruitNumberToFinish;
    int fruitNumberFinished;
    public List<HelperPlantType> upgradeList;
    public HelperPlantType flowerPlantType;
    public List<int> slotCount = new List<int>() { 2, 4, 6 };
    int currentLevel = 0;
    public GameObject treeFlowerPrefab;
    public Transform flowerPositionParent;
    List<Transform> flowerGeneratedPositions;
    List<bool> isFlowerPositionUsed;
    int totalFlowerNumber;
    int currentFlowerNumber;

    public HelperPlantType finishType { get { return upgradeList[upgradeList.Count-1]; } }
    // Start is called before the first frame update
    protected override void Start()
    {
        PlantsManager.Instance.maintree = this;
        PlantsManager.Instance.unlockedSlot = slotCount[currentLevel];
        type = upgradeList[currentLevel];
        base.Start();
        isDragging = false;
        flowerGeneratedPositions = new List<Transform>();
        isFlowerPositionUsed = new List<bool>();
        foreach (Transform t in flowerPositionParent)
        {
            flowerGeneratedPositions.Add(t);
            isFlowerPositionUsed.Add(false);
        }
        totalFlowerNumber = flowerGeneratedPositions.Count;

    }
    public override void die()
    {
        isAlive = false;
        HUD.Instance.Gameover();
        
    }
    public void SpawnFlower()
    {
        currentFlowerNumber++;
        for(int i = 0;i< flowerGeneratedPositions.Count; i++)
        {
            if (!isFlowerPositionUsed[i])
            {
                var go = Instantiate(treeFlowerPrefab, flowerGeneratedPositions[i].position, flowerGeneratedPositions[i].rotation,transform);
                go.GetComponent<TreeFlower>().tree = this;

                CollectionManager.Instance.RemoveCoins(go.transform.position, PlantsManager.Instance.helperPlantCost[flowerPlantType]);
                isFlowerPositionUsed[i] = true;
                break;
            }
        }
    }

    public bool isFinished()
    {
        return fruitNumberFinished >= fruitNumberToFinish;
    }

    public void createFruit()
    {
        fruitNumberFinished++;
        if (fruitNumberFinished >= fruitNumberToFinish)
        {
            HUD.Instance.showGardenButton();
            TutorialManager.Instance.finishTree(type);
        }

    }

    public void Upgrade()
    {
        if (!upgradable())
        {
            return;
        }
        currentLevel += 1;
        PlantsManager.Instance.increaseShadowSize();
        type = upgradeList[currentLevel];
        BirdManager.Instance.startTreePlant(type);
        PlantsManager.Instance.startTreePlant(type);
        //PlantsManager.Instance.unlockedSlot = slotCount[currentLevel];
        CollectionManager.Instance.RemoveCoins(transform.position, PlantsManager.Instance.helperPlantCost[type]);
        HUD.Instance.ShowPlantDetail(gameObject);
        GetComponent<Animator>().SetTrigger("grow");
    }

    public void PurchaseFlower()
    {
        if (canPurchaseFlower())
        {

            SpawnFlower();

            HUD.Instance.ShowPlantDetail(gameObject);
        }

    }


    public bool isAtMaxLevel()
    {
        return currentLevel == upgradeList.Count - 1;
    }

    public bool upgradable()
    {
        return PlantsManager.Instance.IsPlantable(nextLevelType(),true);
    }

    public bool canPurchaseFlower()
    {
        return PlantsManager.Instance.IsPlantable(flowerPlantType, true);
    }

    public bool isAtMaxFlower()
    {
        return currentFlowerNumber >= totalFlowerNumber;
    }
    void OnMouseDown()
    {
        if (isAtMaxLevel())
        {

            if (!isAtMaxFlower())
            {
                PurchaseFlower();
            }
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
