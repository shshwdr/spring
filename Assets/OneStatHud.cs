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

    string minusResourcePrefix = "<color=#FF0000>";

    string AddResourcePrefix = "<color=#00FF00>";
    string InsufficientResourceSurfix = "</color>";
    public void init(string n, float v, float r)
    {
        name.text = n;
        value.text = v.ToString();
        rate.text = "";
        if (r > 0)
        {
            rate.text += AddResourcePrefix;
        }else if (r < 0)
        {
            rate.text += minusResourcePrefix;
        }
        rate.text = (r>=0?"+": "") + r.ToString();
        if (r != 0)
        {
            rate.text += InsufficientResourceSurfix;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
