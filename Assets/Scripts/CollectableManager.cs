using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable Manager")]
public class CollectableManager : ScriptableObject
{
    public int coin_count = 0;
    public int XP_amt = 0;
    private void OnEnable()
    {
        coin_count = 0;
        XP_amt = 0;
    }



    // Update is called once per frame
    public void AddCount()
    {
        
      coin_count++;
        
    }

    public void AddXP()
    {
       XP_amt++;
    }

}
