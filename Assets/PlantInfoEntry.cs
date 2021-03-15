using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantInfoEntry : MonoBehaviour
{
    public TMP_Text title;
    public GameObject oneState;
    public Transform statePanel;
    public List<OneStatHud> stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(string t, Sprite image, int value, Color c)
    {
        title.text = t;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
