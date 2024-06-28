using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    private PlayerInventory player;
    private AudioSource audioSource;

    [SerializeField] private GameObject model;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            player.Pistol();
            audioSource.Play();
            model.SetActive(false);

            Destroy(gameObject, 2);
        }
    }
}
