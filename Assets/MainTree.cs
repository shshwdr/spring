﻿using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : HelperPlant
{
    public List<AudioClip> growSound;
    public AudioClip flowerSound;
    public AudioClip fruitSound;

    public Sprite background;

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

        base.Start();
        //audiosource.PlayOneShot(growSound[0]);
        PlantsManager.Instance.maintree = this;
        PlantsManager.Instance.unlockedSlot = slotCount[currentLevel];
        type = upgradeList[currentLevel];
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

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        //LeanTouch.OnFingerUp += HandleFingerUp;
    }
    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        //LeanTouch.OnFingerUp -= HandleFingerUp;
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if(finger.TapCount == 2)
        {
            OnMouseDown();
        }
    }
    public override void die()
    {
        isAlive = false;
        HUD.Instance.Gameover();
        
    }
    public void SpawnFlower()
    {
        audiosource.PlayOneShot(flowerSound);
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
        
        audiosource.PlayOneShot(fruitSound);
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
        
        audiosource.PlayOneShot(growSound[currentLevel]);
        PlantsManager.Instance.increaseShadowSize();
        type = upgradeList[currentLevel];
        BirdManager.Instance.startTreePlant(type);
        PlantsManager.Instance.startTreePlant(type);
        //PlantsManager.Instance.unlockedSlot = slotCount[currentLevel];
        CollectionManager.Instance.RemoveCoins(transform.position, PlantsManager.Instance.helperPlantCost[type]);
        HUD.Instance.ShowPlantDetail(gameObject);
        GetComponent<Animator>().SetTrigger("grow");

        GameObject cameraPrevious = GameObject.Find("camera" + currentLevel);
        if (cameraPrevious && cameraPrevious.active)
        {
            cameraPrevious.SetActive(false);
        }
        else
        {
            Debug.LogError("previous camra is wrong");
        }


        //GameObject cameraCurrent = GameObject.Find("camera" + (currentLevel+1));
        //if (cameraCurrent && !cameraCurrent.active)
        //{
        //    cameraCurrent.SetActive(true);
        //}
        //else
        //{
        //    Debug.LogError("current camra is wrong");
        //}


        GameObject growCollider = GameObject.Find("growCollider" + currentLevel);
        if (growCollider)
        {
            growCollider.SetActive(false);
        }
        else
        {
            Debug.LogError("growCollider" + currentLevel+ " does not existed");
        }

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
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
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
