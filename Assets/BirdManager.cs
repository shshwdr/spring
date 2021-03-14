using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : Singleton<BirdManager>
{
    public GameObject bird;

    public float birdShowtimeMin = 5f;
    public float birdShowtimeMax = 10f;

    public Transform spawnBirdHighest;
    public Transform spawnBirdLowest;
    float birdShowTimer = -1;
    float birdShowCurrentTime;

    Dictionary<HelperPlantType, HelperPlantType> treeToUnlockFlower = new Dictionary<HelperPlantType, HelperPlantType>()
    {
        {HelperPlantType.appleTree2, HelperPlantType.yellow }
    };

    Dictionary<HelperPlantType, bool> needToUnlock = new Dictionary<HelperPlantType, bool>();
    // Start is called before the first frame update
    void Start()
    {
        updateShowTimer();
    }
    void updateShowTimer()
    {
        birdShowTimer = Random.Range(birdShowtimeMin, birdShowtimeMax);
    }
    // Update is called once per frame
    void Update()
    {
        birdShowCurrentTime += Time.deltaTime;
        if (birdShowCurrentTime >= birdShowTimer)
        {
            updateShowTimer();
            Vector3 position = new Vector3(spawnBirdHighest.position.x, Random.Range(spawnBirdLowest.position.y, spawnBirdHighest.position.y), -1f);
            Instantiate(bird, position,Quaternion.identity);
            birdShowCurrentTime = 0;
        }
    }

    public void startTreePlant(HelperPlantType type)
    {
        if(treeToUnlockFlower.ContainsKey(type) && !needToUnlock.ContainsKey(type))
        {
            needToUnlock[type] = true;
        }
    }

    public void updateDropbox(Dropbox box)
    {
        foreach (var key in needToUnlock.Keys)
        {
            if (needToUnlock[key] == true)
            {
                needToUnlock[key] = false;
                box.dropboxType = DropboxType.unlock;
                box.unlockPlant = HelperPlantType.yellow;
                return;
            }
        }

        box.dropboxType = DropboxType.resource;

        PlantProperty[] dropProperties = new PlantProperty[]{
            PlantProperty.n, PlantProperty.s, PlantProperty.p
        };
        var typeRandom = Random.Range(0, 3);

        box.resource = new Dictionary<PlantProperty, int>() {
            {dropProperties[typeRandom], Random.Range(10, 20) },
        }; 
    }
}
