using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAutoGeneration : MonoBehaviour
{
    public float harvestTime = 10;
    float currentHarvestTimer;
    public Transform spawnPositionParent;

    List<Transform> transforms;

    Dictionary<PlantProperty, int> autoResrouce = new Dictionary<PlantProperty, int>
    {
        {PlantProperty.water,20 },

    };
    void Harvest()
    {

        var t = Utils.RandomTransform(spawnPositionParent);
        var go = Instantiate(PlantsManager.Instance.ClickToCollect, t.position, Quaternion.identity);
        var box = go.GetComponent<CllickToCollect>();
        box.dropboxType = DropboxType.resource;


        box.resource = autoResrouce;
        box.UpdateImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHarvestTimer > harvestTime)
        {
            currentHarvestTimer = 0;
            Harvest();
        }
        currentHarvestTimer += Time.deltaTime;
    }
}
