﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CllickToCollect : MonoBehaviour
{
    bool isClicked = false;
    public DropboxType dropboxType;
    public Dictionary<PlantProperty, int> resource;
    public HelperPlantType unlockPlant;
    public float speed = 1f;
    public float amplitude = 1f;
    public bool needClick;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateImage()
    {

        PlantProperty maxP = PlantProperty.s;
        int maxV = 0;
        foreach (var pair in resource)
        {
            if (pair.Value > maxV)
            {
                maxV = pair.Value;
                maxP = pair.Key;
            }
        }
        GetComponent<SpriteRenderer>().sprite = HUD.Instance.propertyImage[(int)(maxP)];
    }

    public void OnOneTap()
    {
        collect();
    }

    private void OnMouseOver()
    {
        
        if (!needClick )
        {
            collect();


        }
    }

    void collect()
    {
        if (!isClicked)
        {
            isClicked = true;
        }
        if (dropboxType == DropboxType.unlock)
        {
            PlantsManager.Instance.UnlockPlant(unlockPlant);
            BirdManager.Instance.needToUnlock[unlockPlant] = false;
            TutorialManager.Instance.firstSeeSomething("unlock");
            //CollectionManager.Instance.AddCoins(transform.position, resource);
        }
        else
        {
            CollectionManager.Instance.AddCoins(transform.position, resource);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        var verticalMove = Mathf.Sin(Time.realtimeSinceStartup * speed) * amplitude * Vector3.up * Time.deltaTime;
        transform.position +=  verticalMove;
    }
}
