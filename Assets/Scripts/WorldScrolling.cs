using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector3Int currentTilePosition;
    [SerializeField] Vector3Int playerTilePosition;
    [SerializeField] float tileSize = 25f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.z = (int)(playerTransform.position.z / tileSize);
    }

    public void Add(GameObject tileGameObject, Vector3Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.z] = tileGameObject;
    }
}
