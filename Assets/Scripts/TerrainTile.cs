using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector3Int tilePosition;

    private void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
    }
}
