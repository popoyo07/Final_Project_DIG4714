using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private CollectableManager CM;

    public GameObject player;

    public JsonSaveExample saveData;

    [Header("SFX")]
    public AudioClip SFXcoin;
    AudioSource audioSource;

    private void Awake()
    {
        saveData = GameObject.FindWithTag("GameManager").GetComponent<JsonSaveExample>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            if (this.gameObject.CompareTag("coin"))
            {
                audioSource.clip = SFXcoin;
                audioSource.Play();
                CM.AddCount();
                saveData.lastCoins++;
                saveData.SaveData(); // save the game data because a coin was added to the count 
            }



            StartCoroutine(DestroyAfterSound());
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(this.gameObject);
    }
}
