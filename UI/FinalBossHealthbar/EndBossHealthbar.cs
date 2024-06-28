using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndBossHealthbar : MonoBehaviour
{
    private Image healthBar;
    private EndBoss endBoss;

    private float currentHealth;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        endBoss = FindObjectOfType<EndBoss>();
    }

    private void Update()
    {
        currentHealth = endBoss.currentHealth;
        healthBar.fillAmount = currentHealth / endBoss.maxHealth;
    }
}
