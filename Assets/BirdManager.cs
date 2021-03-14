using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : Singleton<BirdManager>
{
    public GameObject bird;

    public float birdShowtimeMin = 5f;
    public float birdShowtimeMax = 10f;
    float birdShowTimer = -1;
    float birdShowCurrentTime;
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
            Instantiate(bird);
        }
    }

    public void updateDropbox(Dropbox box)
    {
        box.dropboxType = DropboxType.unlock;
        box.unlockPlant = HelperPlantType.yellow;
    }
}
