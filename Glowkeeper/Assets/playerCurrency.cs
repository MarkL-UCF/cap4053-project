using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerCurrency : MonoBehaviour
{
    public int coins;
    public Image coinIcon;
    public TextMeshProUGUI currencyDisplay;

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        currencyDisplay = GameObject.Find("CurrencyDisplay").GetComponent<TextMeshProUGUI>();
        coinIcon = GameObject.Find("CoinIcon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currencyDisplay.text = "x" + coins;

    }

    public void receiveCoins(int amount)
    {
        coins += amount;
    }
    public bool checkCoins(int amount)
    {
        return coins >= amount;
    }
    public void spendCoins(int amount)
    {
        coins -= amount;
    }
}
