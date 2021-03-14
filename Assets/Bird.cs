using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject dropbox;
    public float speed = 1f;
    bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            var go = Instantiate(dropbox, transform.position, Quaternion.identity);
            BirdManager.Instance.updateDropbox(go.GetComponent<Dropbox>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed*Time.deltaTime;
    }
}
