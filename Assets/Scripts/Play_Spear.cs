using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Spear : MonoBehaviour
 
{
    private Animator s_animator;
    
    void Start()
    {
        s_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(s_animator != null)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                s_animator.SetTrigger("Thrust_Trigger");
                
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                s_animator.SetTrigger("Thrust_Off");
            }
        }
    }
}
