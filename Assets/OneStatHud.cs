using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneStatHud : MonoBehaviour
{
    public TMP_Text name;
    public Image image;

    public TMP_Text value;
    public TMP_Text rate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    string minusResourcePrefix = "<color=#B90018>";

    string AddResourcePrefix = "<color=#037610>";
    string InsufficientResourceSurfix = "</color>";
    public void init(string n, Sprite t, float v, float r)
    {
        name.text = n;
        image.sprite = t;
        value.text = v.ToString();
        rate.text = "";
        if (r > 0)
        {
            rate.text += AddResourcePrefix;
        }else if (r < 0)
        {
            rate.text += minusResourcePrefix;
        }
        rate.text += (r>=0?"+": "") + r.ToString();
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
