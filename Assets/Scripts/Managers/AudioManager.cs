using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource sfxobject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void playSFX(AudioClip audioclip, Transform spawnTransform, float volume)
    {
        //spawn in gameObject
        AudioSource audioSource = Instantiate(sfxobject, spawnTransform.position, Quaternion.identity);
       
        //assign the audioclip
        audioSource.clip = audioclip;
       
        // assign volume
        audioSource.volume = volume;
        
        //play sound
        audioSource.Play();
        
        //get length of sound fx clip
        float cliplength = audioSource.clip.length;

        //Destroy the clip its done playing 

        Destroy(audioSource.gameObject, cliplength);
       

        
    }

    
}
