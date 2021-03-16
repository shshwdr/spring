using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenSlot : MonoBehaviour
{
    [HideInInspector]
    public HelperPlantType finishedTreeType;
    public GameObject tree;
    public Transform allInTreeNode;

    private void OnMouseEnter()
    {
        HUD.Instance.showLevelInfo(tree.GetComponent<MainTree>().type);
    }

    private void OnMouseExit()
    {
        HUD.Instance.hideLevelInfo();
    }

    private void OnMouseDown()
    {
        Popup.Instance.Init(Dialogues.StartTreeConfirm, () =>
        {

            GardenManager.Instance.StartTree(this);
            PlantsManager.Instance.ClearResource();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        finishedTreeType = tree.GetComponent<MainTree>().finishType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
