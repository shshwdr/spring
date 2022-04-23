using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var buttons = GetComponentsInChildren<CheatButton>();
        buttons[0].GetComponentInChildren<Text>().text = "Unlimit Resource";
        buttons[0].GetComponentInChildren<Button>().onClick.AddListener(delegate { CheatManager.Instance.unlimitedResource(); });


        buttons[1].GetComponentInChildren<Text>().text = "Show Bird";
        buttons[1].GetComponentInChildren<Button>().onClick.AddListener(delegate { BirdManager.Instance.showBird(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
