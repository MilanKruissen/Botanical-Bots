using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayloadHealthbar : MonoBehaviour
{
    private Image healthBar;
    private Payload payload;

    private float currentHealth;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        payload = FindObjectOfType<Payload>();
    }

    private void Update()
    {
        currentHealth = payload.currentHealth;
        healthBar.fillAmount = currentHealth / payload.payloadHealth;
    }
}
