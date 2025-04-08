using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector3Int currentTilePosition = new Vector3Int(0, 1, 0);
    [SerializeField] Vector3Int playerTilePosition;
    Vector3Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 25f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Start()
    {
        UpdateTilesOnScreen();
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.z = (int)(playerTransform.position.z / tileSize);

        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.z = CalculatePositionOnAxis(onTileGridPlayerPosition.z, false);
            UpdateTilesOnScreen();
        }
    }
    private void UpdateTilesOnScreen()
    {
        for (int pov_x = -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth/2; pov_x++)
        {
            for(int pov_z = -(fieldOfVisionHeight/2); pov_z <= fieldOfVisionHeight/2; pov_z++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_z = CalculatePositionOnAxis(playerTilePosition.z + pov_z, true);

                Debug.Log("tileToUpdate_x " + tileToUpdate_x + ", tileToUpdate_z " + tileToUpdate_z);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_z];
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.z + pov_z);
            }
        }
    }
    private Vector3 CalculateTilePosition(int x, int z)
    {
        return new Vector3(x * tileSize, 0f, z * tileSize);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if(currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector3Int tilePosition)
    {
        terrainTiles[tilePosition.x + 1, tilePosition.z + 1] = tileGameObject;
    }
}
