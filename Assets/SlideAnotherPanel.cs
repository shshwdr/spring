using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideAnotherPanel : MonoBehaviour
{
    public UIView target;
    Button button;
    bool isPressed;
    public GameObject showImage;
    public GameObject hideImage;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            if (isPressed)
            {
                hideImage.SetActive(false);
                showImage.SetActive(true);
                target.Hide();
            }
            else
            {
                target.Show();
                hideImage.SetActive(true);
                showImage.SetActive(false);
            }
            isPressed = !isPressed;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
