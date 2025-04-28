using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //these numbers just feel least explosive in movement
    public float moveSpeed = 3f;
    public float rotationSpeed = 45f;
    public float health;

    private Rigidbody rb;
    [SerializeField] Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (gameObject.name == "Santa Claus")
        {
            animator = GetComponent<Animator>();
        }
        else
        {
             animator = GetComponentInChildren<Animator>();
            Debug.Log("Getting the child");
        }
    }

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;

    
        if (movement != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            //Debug.Log("The bool was updated and is moving should be true = " + animator.GetBool("isMoving"));
            rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.LookRotation(movement);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        /*
                if (horizontalInput != 0)
                {
                    Quaternion targetRotation = Quaternion.Euler(0, horizontalInput * rotationSpeed * Time.fixedDeltaTime, 0);
                    rb.MoveRotation(rb.rotation * targetRotation);

                }
        */
    }
}
