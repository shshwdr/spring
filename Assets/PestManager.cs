using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestManager : Singleton<PestManager>
{
    [HideInInspector]
    public List<Pest> currentPests = new List<Pest>();
    public Transform pestSpawnParent;

    public GameObject pestPrefab;
    // Start is called before the first frame update

    public List<Transform> pestsList()
    {
        List<Transform> res = new List<Transform>();
        foreach (var plantValue in currentPests)
        {
            res.Add(plantValue.transform);
        }
        return res;
    }
    void Start()
    {
        //StartCoroutine(spawnMultiWavePest(pestPrefab, 8, 2,1f));
    }

    IEnumerator spawnMultiWavePest(GameObject pest, float waveInterval, int summonAmount, float interval = 1f)
    {
        while(true)
        {
            yield return new WaitForSeconds(waveInterval);
            StartCoroutine(spawnMultiPest(pest, summonAmount, interval));
        }
    }

    IEnumerator spawnMultiPest(GameObject pest, int summonAmount, float interval = 1f)
    {
        for(int i = 0; i < summonAmount; i++)
        {
            spawnOnePest(pest);
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitForSeconds(interval);
    }

    public void spawnOnePest(GameObject pest)
    {

        var trans = Utils.RandomTransform(pestSpawnParent);
        Instantiate(pest, trans);
    }

    public void AddPest(Pest p)
    {
        currentPests.Add(p);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
