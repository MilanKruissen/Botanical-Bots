using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBox : MonoBehaviour
{
    private PlayerManager playerManager;
    private AudioSource audioSource;

    [SerializeField] private GameObject modelOne;
    [SerializeField] private GameObject modelTwo;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager.ArmorOneActivated();

            audioSource.Play();
            modelOne.SetActive(false);
            modelTwo.SetActive(false);

            Destroy(gameObject, 1);
        }
    }
}
