using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable Manager")]
public class CollectableManager : ScriptableObject
{
    public int coin_count = 0;
    private void OnEnable()
    {
        coin_count = 0;
    }



    // Update is called once per frame
    public void AddCount()
    {
        {
            coin_count++;
        }
    }

}
