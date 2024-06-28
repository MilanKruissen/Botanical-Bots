using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    private Image healthBar;
    private MiniBoss miniBoss;

    private float currentHealth;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        miniBoss = FindObjectOfType<MiniBoss>();
    }

    private void Update()
    {
        currentHealth = miniBoss.currentHealth;
        healthBar.fillAmount = currentHealth / miniBoss.maxHealth;
    }
}
