using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProps : MonoBehaviour
{
    public GameObject[] props; // List of prop prefabs that can spawn on this tile
    public int numProps; // Number of props to spawn
    public int offset; // Offset for the number of props
    public float tileScale; // Tile's scale value

    private void Start()
    {
        CreateProps();
    }

    void CreateProps()
    {
        int total = Random.Range(numProps - offset, numProps + offset);

        for (int i = 0; i < total; i++)
        {
            int index = Random.Range(0, props.Length);

            GameObject prop = Instantiate(props[index], transform, worldPositionStays: false); // Instantiates the prop as a child
            prop.transform.localPosition = new Vector3(Random.Range(-tileScale, tileScale), 0, Random.Range(-tileScale, tileScale));
        }
    }
}
