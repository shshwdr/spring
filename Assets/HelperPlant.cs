using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPlant : HPObject
{
    public HelperPlantType type;
    [HideInInspector]
    public int slot;

    // Start is called before the first frame update
    protected override void Start()
    {
        PlantsManager.Instance.AddPlant(this);
        base.Start();
    }

    private void OnMouseEnter()
    {
        HUD.Instance.ShowPlantDetail(gameObject);
    }

    private void OnMouseExit()
    {
        HUD.Instance.HidePlantDetail();
    }
    protected virtual void OnMouseDown()
    {
        PlantsManager.Instance.Remove(gameObject);

        HUD.Instance.HidePlantDetail();
    }

    // Update is called once per frame
    protected override void Update()
    {
    }
}
