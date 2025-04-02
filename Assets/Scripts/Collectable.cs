using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private CollectableManager CM;

    public GameObject player;
    
   

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            if (this.gameObject.CompareTag("coin"))
            {
                CM.AddCount();
            }

            if (this.gameObject.CompareTag("XP"))
            {
                CM.AddXP();
            }

            Destroy(this.gameObject);
        }
    }
}
