using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPlant : HPObject
{
    public HelperPlantType type;
    [HideInInspector]
    public int slot;
    public Collider2D plantCollider;

    bool isDragging = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        PlantsManager.Instance.AddPlant(this);
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseExit()
    {
        if (!isDragging)
        {
            HUD.Instance.HidePlantDetail();
        }
    }
    protected virtual void OnMouseDown()
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
    }
}
