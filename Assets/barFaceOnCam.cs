using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barFaceOnCam : MonoBehaviour
{
    Vector3 bar;

    void Start()
    {
        bar = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(bar); 
    }
}
