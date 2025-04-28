using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject CharacterSelection;
    private CharacterUnlockUI characterUnlockUI;

    // 0 is santa 
    // 1 is Mrs Clause 
    // 2 is Rudolph
    public int pChoice;  // for now is only histing the variable    
    //public bool[] playerChoice;
    // 

    private void Start()
    {
        characterUnlockUI = CharacterSelection.GetComponent<CharacterUnlockUI>();
    }

    private void Update()
    {
        pChoice = CharacterUnlockUI.characterChosen;
        //Debug.LogWarning(pChoice);
    }


}
