using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerManager.currentHealth != playerManager.maxHealth)
        {
            playerManager.currentHealth = playerManager.currentHealth + 50;
            Destroy(gameObject);
        }
    }
}
