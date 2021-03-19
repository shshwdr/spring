using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlantsManager.Instance.ignoreResourcePlant = true;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PestManager.Instance.lotsPest = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            BeeManager.Instance.lotsPest = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlantsManager.Instance.unlockAllFlowers = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TutorialManager.Instance.skipTutorial = true;
        }
    }
}
