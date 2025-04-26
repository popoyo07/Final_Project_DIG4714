using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCountUI : MonoBehaviour
{
    [SerializeField]
    private CollectableManager CM;
    private JsonSaveExample gameSaveData;

    
    public TextMeshProUGUI ctext;

    private void Start()
    {
        gameSaveData = GameObject.FindWithTag("GameManager").GetComponent<JsonSaveExample>();
    }
    // Update is called once per frame
    void Update()
    {
       // ctext.text = "Coin Count: " + CM.coin_count.ToString();
        ctext.text = "Coin Count: " + gameSaveData.totalCoins.ToString();

    }
}
