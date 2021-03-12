using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFlower : MonoBehaviour
{
    public GameObject flowerScentPrefab;
    GameObject flowerScent;
    public GameObject fruitPrefab;
    bool isPollinated;
    bool isDragging;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        flowerScent = Instantiate(flowerScentPrefab, mousePosition, Quaternion.identity);
        flowerScent.GetComponent<FlowerScent>().treeFlower = this;
    }

    public void GetPollinate()
    {
        if (isPollinated)
        {
            return;
        }
        isPollinated = true;
        Instantiate(fruitPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        
        flowerScent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging && flowerScent && !isPollinated)
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            flowerScent.transform.position = mousePosition;

        }
    }
}
