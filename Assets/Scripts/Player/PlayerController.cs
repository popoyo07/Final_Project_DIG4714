using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //these numbers just feel least explosive in movement
    public float moveSpeed = 3f;
    public float health = 10;

    float horizontal;
    float vertical;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = Color.red; // Temp
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * moveSpeed, 0, vertical * moveSpeed);
        Quaternion lookDirection = Quaternion.LookRotation(rb.velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime);    

    }
}
