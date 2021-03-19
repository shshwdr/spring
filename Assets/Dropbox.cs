using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DropboxType { unlock, resource}
public class Dropbox : CllickToCollect
{
    
    
    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
