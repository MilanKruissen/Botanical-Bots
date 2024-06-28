using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaPickup : MonoBehaviour
{
    private PlayerInventory player;

    [SerializeField] private GameObject model;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        player = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Katana();
            model.SetActive(false);

            Destroy(gameObject, 2);
        }
    }
}
