using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DropboxType { unlock, resource}
public class Dropbox : MonoBehaviour
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
        if(dropboxType == DropboxType.unlock)
        {
            PlantsManager.Instance.UnlockPlant(unlockPlant);
        }
        else
        {
            PlantsManager.Instance.AddResource(resource);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
