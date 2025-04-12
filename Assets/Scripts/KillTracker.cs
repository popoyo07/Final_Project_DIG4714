using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using System.Runtime.CompilerServices;


public class KillTracker : MonoBehaviour
{
    public static int killsCounted;

    public static List<GameObject> killlist = new List<GameObject>();

    private void Update()
    {
        killsCounted = killlist.Count;
        Debug.Log("Kills counted is: " + killsCounted);

        
    }

    

    /*
     * this script is modified by EnemyBehavior script, whenever an enemy's health drops to zero. Add the gameobject to the killlist.
     * save the count of the list to an integer called killsCounted every update
     * see EnemyBehavior script
     */





}





    

    
    


