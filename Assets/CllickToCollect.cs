using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CllickToCollect : MonoBehaviour
{
    bool isClicked = false;
    public float speed = 1f;
    public DropboxType dropboxType;
    public Dictionary<PlantProperty, int> resource;
    public HelperPlantType unlockPlant;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
        }
        if (dropboxType == DropboxType.unlock)
        {
            PlantsManager.Instance.UnlockPlant(unlockPlant);
        }
        else
        {
            //PlantsManager.Instance.AddResource(resource);
        }
        CollectionManager.Instance.AddCoins(transform.position, PlantProperty.bee, 10);
        //Destroy(gameObject);
    }
}
