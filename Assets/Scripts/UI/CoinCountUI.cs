using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCountUI : MonoBehaviour
{
    [SerializeField]
    private CollectableManager CM;

    
    public TextMeshProUGUI ctext;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ctext.text = "Coin Count: " + CM.coin_count.ToString();
    }
}
