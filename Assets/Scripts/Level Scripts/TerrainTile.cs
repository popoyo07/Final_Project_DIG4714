using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector3Int tilePosition;

    private void Start()
    {
        //gets the component in parent(worldScrolling) and add gameobject and tile position
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
    }
}
