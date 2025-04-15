using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerCurrency : MonoBehaviour
{
    public int coins;
    public TextMeshProUGUI currencyDisplay;

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        currencyDisplay = GameObject.Find("CurrencyDisplay").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        currencyDisplay.text = "x" + coins;

    }

    public void ReceiveCoins(int amount)
    {
        coins += amount;
    }
    public bool CheckCoins(int amount)
    {
        return coins >= amount;
    }
    public void SpendCoins(int amount)
    {
        coins -= amount;
    }
}
