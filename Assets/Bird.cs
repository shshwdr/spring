using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    protected AudioSource audiosource;
    public GameObject dropbox;
    public AudioClip dropSound;
    public AudioClip appearSound;
    public float speed = 1f;
    public float verticalSpeed = 1f;
    public float amplitude = 1f;
    public bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {

        audiosource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            audiosource.PlayOneShot(dropSound);
            isClicked = true;
            var go = Instantiate(dropbox, transform.position, Quaternion.identity);
            BirdManager.Instance.updateDropbox(go.GetComponent<Dropbox>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        var verticalMove = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed ) * amplitude * Vector3.up * Time.deltaTime;
        transform.position += Vector3.left * speed*Time.deltaTime + verticalMove;
    }

    public void Appear()
    {

        audiosource.PlayOneShot(appearSound);
    }
}
