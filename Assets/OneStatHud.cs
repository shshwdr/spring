using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OneStatHud : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text value;
    public TMP_Text rate;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void init(string n, float v, float r)
    {
        name.text = n;
        value.text = v.ToString();
        rate.text = (r>=0?"+":"-") + r.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
