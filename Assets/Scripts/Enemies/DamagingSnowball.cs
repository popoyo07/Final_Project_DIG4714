using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamagingSnowball : MonoBehaviour
{
   [SerializeField] private PlayerController player;
    private UIBars uiBars;
    [SerializeField] private float snowball_dmg;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        uiBars = player.GetComponent<UIBars>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.health = player.health - snowball_dmg + snowball_dmg;
            uiBars.LoseHealthBar(player.health);
            Destroy(this.gameObject);
        }
    }
}
