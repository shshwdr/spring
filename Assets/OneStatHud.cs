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
    public void init(string n, Sprite t, float v)
    {
        name.text = n;
        image.sprite = t;
        value.text = v.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
