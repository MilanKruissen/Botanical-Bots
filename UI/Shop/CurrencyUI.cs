using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    private TextMeshProUGUI currencyAmount;

    private void Start()
    {
        currencyAmount = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        try
        {
            UpdateText(FindObjectOfType<PlayerManager>().playerCoins);
        }
        catch (System.Exception) { }
       
    }

    public void UpdateText(int totalCoins)
    {
        currencyAmount.text = totalCoins.ToString();
    }
}
