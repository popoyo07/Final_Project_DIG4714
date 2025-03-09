using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    // 
    private void Awake()
    {
        /*
         * if Instance isn't referencing anything and it isn't this scipt specifically, destroy itself
         * if Instance isn't referencing this script, reference this script
         * if Instance isn't referencing anything, reference this script
         */
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    
}
