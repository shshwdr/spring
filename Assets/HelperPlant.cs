using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPlant : HPObject
{
    public HelperPlantType type;
    [HideInInspector]
    public int slot;
    public Collider2D plantCollider;
    public GameObject collictToCollect;

    protected bool isDragging = true;

    public float harvestTime = 10;
    float currentHarvestTimer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void OnMouseEnter()
    {
        if (!isDragging)
        {

            HUD.Instance.ShowPlantDetail(gameObject);
        }
    }

    private void Plant()
    {
        isDragging = false;
        if (canPlant())
        {
            PlantsManager.Instance.Purchase(gameObject);
            PlantsManager.Instance.AddPlant(this);
        }
        else
        {
            Destroy(gameObject);
        }

        PlantsManager.Instance.shadowCollider.gameObject.SetActive(false);
    }

    private void OnMouseExit()
    {
        if (!isDragging)
        {
            HUD.Instance.HidePlantDetail();
        }
    }
    protected virtual void RemovePlant()
    {
        
        PlantsManager.Instance.Remove(gameObject);
        die();
        HUD.Instance.HidePlantDetail();
    }

    public override void die()
    {
        var summons = GetComponent<SummonPlant>();
        if (summons)
        {
            summons.clean();
        }
        base.die();

    }
    bool canPlant()
    {
        return PlantsManager.Instance.IsPlantable(type, plantCollider);
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (isDragging)
        {
            if (!canPlant())
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
            if (Input.GetMouseButtonUp(0) && isDragging)
                Plant();

        }
        else
        {
            if (currentHarvestTimer > harvestTime)
            {
                currentHarvestTimer = 0;
                Harvest();
            }
            currentHarvestTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemovePlant();
        }
    }

    void Harvest()
    {
        var go = Instantiate(collictToCollect, transform.position, Quaternion.identity);
        var box = go.GetComponent<CllickToCollect>();
        box.dropboxType = DropboxType.resource;


        box.resource = PlantsManager.Instance.helperPlantProd[type];
    }

    public void MoveToGarden()
    {
        Destroy(this);
    }
}
