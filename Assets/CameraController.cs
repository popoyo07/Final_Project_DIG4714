using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float yOffset = 15;
    public float zOffset = -4;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, yOffset, zOffset);
    }
}
