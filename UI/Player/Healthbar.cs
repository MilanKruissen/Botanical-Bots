using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image healthBar;
    private PlayerManager playerManager;

    private float currentHealth;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        currentHealth = playerManager.currentHealth;
        healthBar.fillAmount = currentHealth / playerManager.maxHealth;
    }
}
