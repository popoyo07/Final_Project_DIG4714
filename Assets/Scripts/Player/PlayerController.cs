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
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;

        // this rotates the model to some way cause camera moves but player never rotates, but may need to hard code what is forward cause gets finnicky when moving right and left or moving camera around
        if (movement != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
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
