using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    // 
    private void Awake()
    {
        /*
         * if Instance isn't referencing anything and it isn't this scipt specifically, destroy itself
         * if Instance isn't referencing this script, reference this script
         * if Instance isn't referencing anything, reference this script
         */
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
    }
   

    
}
