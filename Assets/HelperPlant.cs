using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPlant : MonoBehaviour
{
    public HelperPlantType type;
    [HideInInspector]
    public bool isAlive = true;
    public float hp;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isAlive = true;
        PlantsManager.Instance.AddPlant(this);
    }

    private void OnMouseEnter()
    {
        HUD.Instance.ShowPlantDetail(gameObject);
    }

    private void OnMouseExit()
    {
        HUD.Instance.HidePlantDetail();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
